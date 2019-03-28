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

namespace Domain.Services
{
    public interface ILoginService
    {
        UserModel GetUser();

        Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
        Task<ChangePasswordResponseModel> ChangePassword(ChangePasswordRequestModel changePasswordRequestModel);
        Task<ForgotPasswordResponseModel> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel);
    }


}
