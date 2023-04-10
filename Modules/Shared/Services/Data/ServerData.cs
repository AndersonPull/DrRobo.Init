using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Shared.Services.Data
{
    public class ServerData : BaseData<ServerModel>
    {
        public override List<ServerModel> GetAll()
            => new List<ServerModel>(_dataBase.Table<ServerModel>());

        public override ServerModel GetById(int id)
            => _dataBase.Table<ServerModel>().FirstOrDefault(entity => entity.Id == id);

        public override int Save(ServerModel entity)
            => _dataBase.Insert(entity);

        public override int Update(ServerModel entity)
            => _dataBase.Update(entity);

        public override int Delete(ServerModel entity)
            => _dataBase.Delete(entity);

        public override int DeleteAll()
            => _dataBase.DeleteAll<ServerModel>();

        public override void Dispose()
            => _dataBase.Dispose();
    }
}