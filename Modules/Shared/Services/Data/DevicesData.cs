using System;
using Drrobo.Modules.Shared.Models;

namespace Drrobo.Modules.Shared.Services.Data
{
	public class DevicesData : BaseData<DevicesModel>
    {
        public override List<DevicesModel> GetAll()
            => new List<DevicesModel>(_dataBase.Table<DevicesModel>());

        public override DevicesModel GetById(int id)
            => _dataBase.Table<DevicesModel>().FirstOrDefault(entity => entity.Id == id);

        public override int Save(DevicesModel entity)
            => _dataBase.Insert(entity);

        public override int Update(DevicesModel entity)
            => _dataBase.Update(entity);

        public override int Delete(DevicesModel entity)
            => _dataBase.Delete(entity);

        public override int DeleteAll()
            => _dataBase.DeleteAll<DevicesModel>();

        public override void Dispose()
            => _dataBase.Dispose();
    }
}