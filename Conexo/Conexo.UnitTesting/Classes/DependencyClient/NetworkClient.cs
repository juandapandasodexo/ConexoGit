using System;
using Common.Dependencies.Networking;

namespace Conexo.UnitTesting.Classes.DependencyClient
{
    public class NetworkClient : INetworkDependency
    {
        public NetworkClient()
        {
        }

        public bool IsConnected()
        {
            return true;
        }
    }
}
