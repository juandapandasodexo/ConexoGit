using System;
using Common.Dependencies.Resources;

namespace Conexo.UnitTesting.Classes.DependencyClient
{
    public class ResourceClient : IResourcesDependency
    {
        public ResourceClient()
        {
        }

        public string ResolveString(string key)
        {
            return key;
        }
    }
}
