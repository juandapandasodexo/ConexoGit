using System;
using System.Collections.Generic;
using Common.Preferences;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.Services
{

    public interface ICUCService{
        
        void SetCurrentCUC(string userName,CucModel model);
        CucModel GetCurrentCUC(string userName);
        void SetCurrentCUCList(string userName, List<CucModel> models);
        List<CucModel> GetCurrentCUCList(string userName);
    }
}
