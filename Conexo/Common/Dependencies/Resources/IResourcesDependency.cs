using System;
namespace Common.Dependencies.Resources
{
    public interface IResourcesDependency
    {
        string ResolveString(string key);
    }
}
