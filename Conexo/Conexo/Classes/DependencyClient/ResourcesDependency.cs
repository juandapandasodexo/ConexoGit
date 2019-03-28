using System;
using Common.Dependencies.Resources;

namespace Conexo.Classes.DependencyClient
{
    public class ResourcesDependency : IResourcesDependency
    {

        public string ResolveString(string key)
        {
            return Xamarin.Forms.Application.Current.Resources[key].ToString();
        }
    }
}
