using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.Dependencies.Networking;
using Common.Exceptions;
using Common.Preferences;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infraestructure.Networking
{
    public class ApiClient : IDisposable
	{
		private INetworkDependency _networkDependency;
		private HttpClient _client;
		protected IUserPreferences UserPreferences { get; private set; }

		public string BaseUrl {
			get;
			set;
		}

		public ApiClient (string baseUrl,INetworkDependency networkDependency, IUserPreferences userPreferences)
		{
			this.BaseUrl = baseUrl;
			this.UserPreferences = userPreferences;
			_client = CreateClient ();
			_networkDependency = networkDependency;
		}

		public ApiClient(IUserPreferences userPreferences) 
		{
			this.UserPreferences = userPreferences;
		}

		public async Task<T> POSTAsync<T> (string relativeUrl, object postData, bool useToken = false) 
		{
			if (_networkDependency.IsConnected () == false) {
				throw new NoInternetException (@"Comprueba tu conexión a internet");
			}

			//Organizamos la petición
			object dataToParse = postData ?? string.Empty;
			string jsonString = JsonConvert.SerializeObject (dataToParse).ToString ();
			StringContent postDataContent = new StringContent (jsonString, Encoding.UTF8, "application/json");

			//Realizamos la petición
			HttpResponseMessage response;

			T result = default(T);

			//Obtenemos el token si la peticion a realizar contiene token
			if (useToken)
			{
                var token = GetToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}

			try{
				
				response = await _client.PostAsync (relativeUrl, postDataContent).ConfigureAwait (false);
				result = await CastResponseAsync<T>(response);

			}
			catch (NoAuthException ex)
			{
				throw new NoAuthException(ex.Message);
			}
			catch (TaskCanceledException)
			{
				throw new TimeOutException();
			}
			catch (Exception ex)
			{
				throw new WSErrorException(ex.Message);
			}

            return result;
		}

		public async Task<T> GetAsync<T>(string apiUrl, bool useToken = false)
		{

			if (_networkDependency.IsConnected() == false)
			{
				throw new NoInternetException();
			}

			HttpResponseMessage response = default(HttpResponseMessage);
			//var result = default(T);
			T result = default(T);

			if (useToken)
			{
                var token = GetToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}

			try
			{
				response = await _client.GetAsync(apiUrl);
				result = await CastResponseAsync<T>(response);
			}
			catch (NoAuthException ex)
			{
				throw new NoAuthException(ex.Message);
			}
			catch (Exception ex)
			{
				throw new WSErrorException(ex.Message);
			}

            return result;

			/*switch (result.Header.Status)
			{
				case 200:
					return result.Data;

				case 401:
					throw new NoAuthException(result.Header.Messages[0]);

				default:
					throw new WSErrorException(result.Header.Messages[0]);
			}*/

		}


		private async Task<T> CastResponseAsync<T> (HttpResponseMessage response)
		{

			T result = default(T);

			//Revisamos si hay problemas en la respuesta y no es 200 en el HTTP
			if (response.IsSuccessStatusCode == false) {

                if(response.StatusCode==HttpStatusCode.BadRequest || response.StatusCode==HttpStatusCode.NotFound){
                    var jsonError = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    dynamic d = JObject.Parse(jsonError);
                    string exMessage = d.Descripcion;
                    exMessage = exMessage ?? d.descripcion;
                    throw new AppException(exMessage);

                }

                if(response.StatusCode == HttpStatusCode.InternalServerError){
                    throw new InternalServerException("Ha ocurrido un error en la respuesta del servidor");
                }

				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					throw new NoAuthException();
				}
                else { 
					throw new WSErrorException();
				}

			}

			var jsonString = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);

            result = await Task.Run(() => JsonConvert.DeserializeObject<T>(jsonString)).ConfigureAwait(false);
            //var hresult = JsonConvert.DeserializeObject<LoginResponseModel>(jsonString);



			return result;
		}


		private HttpClient CreateClient ()
		{

			var handler = new ModernHttpClient.NativeMessageHandler () {
				UseProxy = true,
				Proxy = System.Net.HttpWebRequest.DefaultWebProxy
			};


			Uri uri = new Uri (BaseUrl);
			var httpClient = new HttpClient (handler) { 
				BaseAddress = uri,
			};

			httpClient.Timeout = TimeSpan.FromMinutes (1);
			httpClient.DefaultRequestHeaders.Accept.Clear ();

			return httpClient;
		}

		#region Metodos Publicos

		public string GetRefreshToken()
		{
			string token = UserPreferences.GetToken();
			return token;
		}

		public string GetToken()
		{
            //TODO: Organizar el token
			string token = UserPreferences.GetToken();
            //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJ1c2VyIiwidmFyIjoiODMwMDUyNzYyIiwidXN1YXJpbyI6IlBydWViYXNEb2N0dXMiLCJ2aWdlbmNpYSI6IjM2MDAwMDAwIiwidGllbXBvRmluIjoiMDEvMDgvMjAxOCAyMzo0ODo1NiIsIm5iZiI6MTUzMzEzMTMzNiwiZXhwIjoxNTMzMjE3NzM2LCJpYXQiOjE1MzMxMzEzMzZ9.89ioPXVidseFFGfWNeCUNOaaHdUSL68pU92JmtLcNEU";
			return token;
		}

		#endregion


		#region IDisposable implementation
		public void Dispose ()
		{
			if (_client != null)
			{
				_client.Dispose();	
			}
		}
		#endregion
	}
}

