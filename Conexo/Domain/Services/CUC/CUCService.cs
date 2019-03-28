using System;
using System.Collections.Generic;
using Common.Preferences;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.Services.CUC
{
    public class CUCService : ICUCService
    {

        private IUserPreferences _userPreferences;



        private string CUC_KEY(string userName)
        {
            return string.Format("CUC_KEY" + userName);
        }

        private string CUC_LIST_KEY(string userName)
        {
            return string.Format("CUC_LIST_KEY" + userName);
        }

        public CUCService(IUserPreferences userPreferences)
        {
            _userPreferences = userPreferences;
        }

        public void SetCurrentCUC(string userName, CucModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            _userPreferences.StoreStringValue(CUC_KEY(userName), json);

        }

        public CucModel GetCurrentCUC(string userName)
        {
            string jsonCUC = _userPreferences.GetStoredStringValue(CUC_KEY(userName));
            CucModel model = new CucModel();
            if (jsonCUC != null)
            {
                model = JsonConvert.DeserializeObject<CucModel>(jsonCUC);
            }
            return model;
        }




        public void SetCurrentCUCList(string userName, List<CucModel> models)
        {
            string json = JsonConvert.SerializeObject(models);
            _userPreferences.StoreStringValue(CUC_LIST_KEY(userName), json);

        }

        public List<CucModel> GetCurrentCUCList(string userName)
        {
            string jsonCUC = _userPreferences.GetStoredStringValue(CUC_LIST_KEY(userName));
            List<CucModel> models = new List<CucModel>();
            if (jsonCUC != null)
            {
                models = JsonConvert.DeserializeObject<List<CucModel>>(jsonCUC);
            }
            return models;
        }


    }
}
