using Common.Dependencies.SQL;
using Infraestructure.Entities;
using Infraestructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructure.Repositories.Data
{
    public interface IBonusDataRepository : ICRUDRepository<BonusEntity>
    {
        BonusEntity GetByCode(string codigoBono);
        void DeleteAllByCodes(List<string> codigosBonos);
    }

    public class BonusDataRepository : BaseCRUDRepository<BonusEntity>, IBonusDataRepository
    {

        public BonusDataRepository(ISQLiteConnectionDependency sqliteConnection) : base(sqliteConnection)
        {
            
        }


        public BonusEntity GetByCode(string codigoBono)
        {
            return GetAll().Where(pp => pp.codigoBono == codigoBono).FirstOrDefault();
        }


        public void DeleteAllByCodes(List<string> codigosBonos){

            foreach (var item in codigosBonos)
            {
                var bono = GetByPrimaryKey(item);
                DataContext.Delete(bono);
            }

        }

    }

}
