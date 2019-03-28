using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Dependencies.Mapper;
using Common.Preferences;
using Domain.Models.Request;
using Domain.Models.Response;
using Infraestructure.DTO.Request;
using Infraestructure.DTO.Response;
using Infraestructure.Entities;
using Infraestructure.Repositories.Data;
using Infraestructure.Repositories.WS;
using System.Linq;
using Domain.Models;

namespace Domain.Services
{
    public interface IValidBonusService
    {
        Task<ValidateBonusResponseModel> ValidBonus(ValidateBonusRequetsModel validateBonusRequetsModel);
        Task<ValidateBonusResponseModel> RemoveBonus(ValidateBonusRequetsModel validateBonusRequetsModel);
        Task<MassiveBonusResponseModel> RemoveAllBonus(string userName,CucModel cucModel);


        List<ValidateBonusResponseModel> GetLocalBonus(string username);
        ValidateBonusResponseModel GetLocalBonusByCodigoBono(string username, string codigoBono);
        void DeleteLocalBonus(string username);
    }


}
