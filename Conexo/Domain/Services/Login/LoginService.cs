using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.Dependencies.Mapper;
using Common.Preferences;
using Global.Constants;
using Domain.Models;
using Domain.Models.Request;
using Domain.Models.Response;
using Infraestructure.DTO.Request;
using Infraestructure.DTO.Response;
using Infraestructure.Entities;
using Infraestructure.Repositories.Data;
using Infraestructure.Repositories.WS;
using FluentValidation;
using Common.Dependencies.Resources;

namespace Domain.Services.Login
{
    public class LoginService : ILoginService
    {

        private LoginValidator _loginValidator;


        private IWSLoginRepository _LoginRepository;
        private IMapper _mapper;
        private IUserPreferences _userPreferences;
        private IUserDataRepository _userDataRepository;


        public UserModel GetUser()
        {
            var currentUser = _userDataRepository.GetUser();
            if (currentUser != null)
            {
                return _mapper.Map<UserModel>(currentUser);
            }
            else
            {
                return null;
            }
        }

        public LoginService(IWSLoginRepository LoginRepository, IMapperDependency mapperDependency, IUserPreferences userPreferences, IUserDataRepository userDataRepository,IResourcesDependency resourceDependency)
        {
            _LoginRepository = LoginRepository;
            _mapper = mapperDependency.GetMapper();
            _userPreferences = userPreferences;
            _userDataRepository = userDataRepository;
            _loginValidator = new LoginValidator(resourceDependency);
        }



        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel)
        {

            //Validamos el objeto
            await _loginValidator.ValidateAndThrowAsync(loginRequestModel);



            LoginRequestContract loginRequestContract = new LoginRequestContract
            {
                usuario = loginRequestModel.UserName,
                contrasenia = loginRequestModel.Password
            };



            LoginResponseContract loginResponseContract = await _LoginRepository.LoginAsync(loginRequestContract);



            //Ahora lo insertamos en la Base de Datos
            UserModel userModel = new UserModel
            {
                UserName = loginRequestModel.UserName,
                Password = loginRequestModel.Password,
                IsRemember = loginRequestModel.IsRemember,
                LastAccess = DateTime.Now
            };

            UserEntity userEntity = _mapper.Map<UserEntity>(userModel);
            _userDataRepository.DeleteAll();
            _userDataRepository.Insert(userEntity);



            // Ahora metemos el token en el userPreferences
            _userPreferences.StoreStringValue(GlobalConfig.TOKEN_KEY, loginResponseContract.token);


            return _mapper.Map<LoginResponseModel>(loginResponseContract);
        }

        public async Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel changePasswordRequestModel)
        {
            ChangePasswordRequestContract changePasswordRequestContract = _mapper.Map<ChangePasswordRequestContract>(changePasswordRequestModel);
            ChangePasswordResponseContract changePasswordResponseContract = await _LoginRepository.ChangePassword(changePasswordRequestContract);

            return _mapper.Map<ChangePasswordResponseModel>(changePasswordResponseContract);
        }

        public async Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            ForgotPasswordRequestContract forgotPasswordRequestContract = _mapper.Map<ForgotPasswordRequestContract>(forgotPasswordRequestModel);
            ForgotPasswordResponseContract forgotPasswordResponseContract = await _LoginRepository.ForgotPassword(forgotPasswordRequestContract);

            return _mapper.Map<ForgotPasswordResponseModel>(forgotPasswordResponseContract);
        }

    }
}
