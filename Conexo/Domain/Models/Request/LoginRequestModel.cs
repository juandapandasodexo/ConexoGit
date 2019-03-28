namespace Domain.Models.Request
{
    public class LoginRequestModel
	{
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }


        public LoginRequestModel(){
            
        }


        public LoginRequestModel(string userName,string password,bool isRemeber){
            UserName = userName;
            Password = password;
            IsRemember = isRemeber;
        }
	}
}
