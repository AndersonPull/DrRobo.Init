using Drrobo.Modules.Dashboard.Models;
using Drrobo.Modules.Shared.Services.Data;

namespace Drrobo.Modules.Dashboard.Data
{
	public class LanguageData : BaseData<LanguageModel>
	{
        public override List<LanguageModel> GetAll()
            => new List<LanguageModel>(_dataBase.Table<LanguageModel>());

        public override LanguageModel GetById(int id)
            => _dataBase.Table<LanguageModel>().FirstOrDefault(entity => entity.Id == id);

        public override int Save(LanguageModel entity)
            => _dataBase.Insert(entity);

        public override int Update(LanguageModel entity)
            => _dataBase.Update(entity);

        public override int Delete(LanguageModel entity)
            => _dataBase.Delete(entity);

        public override int DeleteAll()
            => _dataBase.DeleteAll<LanguageModel>();

        public override void Dispose()
            => _dataBase.Dispose();
    }
}