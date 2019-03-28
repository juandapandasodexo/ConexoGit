using System;
using AutoMapper;
using Common.Dependencies.Mapper;

namespace Conexo.Droid.DependencyClient
{
	public class MapperClient : IMapperDependency
	{
		public MapperClient()
		{
		}

		public IMapper GetMapper()
		{
			return LocalApp.LocalConfig.Mapper;
		}
	}
}
