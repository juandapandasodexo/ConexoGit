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

namespace Domain.Services.Bonus
{
    public class ValidBonusService : IValidBonusService
    {
        private IWSValidBonoRepository _wSValidBonoRepository;
        private IMapper _mapper;
        private IUserPreferences _userPreferences;
        private IBonusDataRepository _bonusDataRepository;

        public ValidBonusService(IWSValidBonoRepository wSValidBonoRepository, IMapperDependency mapperDependency, IUserPreferences userPreferences, IBonusDataRepository bonusDataRepository)
        {
            _wSValidBonoRepository = wSValidBonoRepository;
            _mapper = mapperDependency.GetMapper();
            _userPreferences = userPreferences;
            _bonusDataRepository = bonusDataRepository;
        }

        public List<ValidateBonusResponseModel> GetLocalBonus(string username)
        {
            var localBonus = _bonusDataRepository.GetAll().Where(pp => pp.UserName == username).ToList();
            var responseModel = _mapper.Map<List<ValidateBonusResponseModel>>(localBonus);
            return responseModel;
        }

        public async Task<ValidateBonusResponseModel> ValidBonus(ValidateBonusRequetsModel validateBonusRequetsModel)
        {
            ValidateBonusRequetsContract bonusRequest = _mapper.Map<ValidateBonusRequetsContract>(validateBonusRequetsModel);
            ValidateBonusResponseContract bonusResponse = await _wSValidBonoRepository.ValidBonus(bonusRequest);
            bonusResponse.codigoBono = bonusRequest.codigoBono;

            var bono = _bonusDataRepository.GetByCode(bonusRequest.codigoBono);
            if (bono == null)
            {
                BonusEntity entity = _mapper.Map<BonusEntity>(bonusResponse);
                entity.UserName = validateBonusRequetsModel.UserName;
                _bonusDataRepository.Insert(entity);
            }


            return _mapper.Map<ValidateBonusResponseModel>(bonusResponse);
        }

        public async Task<ValidateBonusResponseModel> RemoveBonus(ValidateBonusRequetsModel validateBonusRequetsModel)
        {
            ValidateBonusRequetsContract bonusRequest = _mapper.Map<ValidateBonusRequetsContract>(validateBonusRequetsModel);

            ValidateBonusResponseContract bonusResponse = await _wSValidBonoRepository.RemoveBonus(bonusRequest);

            var bonoEntity = _bonusDataRepository.GetByCode(bonusRequest.codigoBono);
            if (bonoEntity != null)
            {
                _bonusDataRepository.Delete(bonoEntity);
            }

            return _mapper.Map<ValidateBonusResponseModel>(bonusResponse);
        }

        public void DeleteLocalBonus(string username)
        {

            var bonos = GetLocalBonus(username);
            var codigos = bonos.Select(pp => pp.codigoBono).ToList();
            _bonusDataRepository.DeleteAllByCodes(codigos);
        }

        public ValidateBonusResponseModel GetLocalBonusByCodigoBono(string username, string codigoBono)
        {

            return GetLocalBonus(username).Where(pp => pp.codigoBono == codigoBono).FirstOrDefault();
        }

        public async Task<MassiveBonusResponseModel> RemoveAllBonus(string userName, CucModel cucModel)
        {
            var allBonus = GetLocalBonus(userName);
            MassiveBonusRequestContract requestContract = new MassiveBonusRequestContract()
            {
                codigosBonos = allBonus.Select(pp => pp.codigoBono).ToList(),
                cuc = cucModel.idPuntoVenta,
                idEmisorBono = userName
            };

            var response = await _wSValidBonoRepository.MassiveRemoveBonus(requestContract);

            DeleteLocalBonus(userName);

            return _mapper.Map<MassiveBonusResponseModel>(response);
        }
    }
}
