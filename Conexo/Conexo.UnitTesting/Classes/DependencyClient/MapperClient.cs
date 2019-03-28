using System;
using AutoMapper;
using Common.Dependencies.Mapper;
using Configuration;

namespace Conexo.UnitTesting.Classes.DependencyClient
{
    public class MapperClient : IMapperDependency
    {


        private static IMapper _mapper;


        public MapperClient()
        {
            if(_mapper==null){
                Init();
            }
        }

        private void Init()
        {
            _mapper = AutomapperConfig.CreateMapper();
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }
    }
}
