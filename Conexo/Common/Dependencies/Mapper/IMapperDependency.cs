using System;
using AutoMapper;

namespace Common.Dependencies.Mapper
{
	public interface IMapperDependency
	{
		IMapper GetMapper();
	}
}

