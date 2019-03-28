using AutoMapper;
using Infraestructure.Entities;
using Domain.Models;
using Infraestructure.DTO.Request;
using Infraestructure.DTO.Response;
using Domain.Models.Request;
using Domain.Models.Response;
using Infraestructure.DTO;

namespace Configuration
{
    public static class AutomapperConfig
	{
		
		public static IMapper CreateMapper(){
			var config = new MapperConfiguration(cfg => {
				cfg.AddProfile<EntityModelProfile>();
				cfg.AddProfile<ContractModelProfile>();
			});
			return config.CreateMapper ();
		}



		private class EntityModelProfile : Profile{

            public EntityModelProfile()
            {
                CreateMap<UserEntity, UserModel>().ReverseMap();
                CreateMap<BonusEntity, BonusModel>().ReverseMap();

                CreateMap<BonusEntity, ValidateBonusResponseContract>().ForSourceMember(x => x.UserName, opt => opt.Ignore());
                CreateMap<ValidateBonusResponseContract, BonusEntity>().ForMember(x => x.UserName, opt => opt.Ignore());
                CreateMap<BonusEntity, ValidateBonusResponseModel>().ReverseMap();
			}
		}

		private class ContractModelProfile : Profile{

            public ContractModelProfile(){
            
				CreateMap<LoginRequestContract, LoginRequestModel>().ReverseMap();
                CreateMap<LoginResponseContract, LoginResponseModel>().ReverseMap();

                CreateMap<CucContract, CucModel>().ReverseMap();

                CreateMap<ValidateBonusResponseContract, ValidateBonusResponseModel>().ReverseMap();
                CreateMap<ValidateBonusRequetsContract, ValidateBonusRequetsModel>().ReverseMap();

                CreateMap<ChangePasswordRequestContract, ChangePasswordRequestModel>().ReverseMap();
                CreateMap<ChangePasswordResponseContract, ChangePasswordResponseModel>().ReverseMap();

                CreateMap<ForgotPasswordRequestContract, ForgotPasswordRequestModel>().ReverseMap();
                CreateMap<ForgotPasswordResponseContract, ForgotPasswordResponseModel>().ReverseMap();


                CreateMap<MassiveBonusRequestContract, MassiveBonusRequestModel>().ReverseMap();
                CreateMap<MassiveBonusResponseContract, MassiveBonusResponseModel>().ReverseMap();
            }



		}



	}
}

