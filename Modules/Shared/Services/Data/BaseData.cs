﻿using SQLite;

namespace Drrobo.Modules.Shared.Services.Data
{
    public abstract class BaseData<T>
    {
        protected SQLiteConnection _dataBase;
        private string _name = "Robo.db3";

        public BaseData()
        {
            string _dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _name);
            _dataBase = new SQLiteConnection(_dataBasePath);
            _dataBase.CreateTable<T>();
        }

        public abstract List<T> GetAll();

        public abstract T GetById(int id);

        public abstract int Save(T entity);

        public abstract int Update(T entity);

        public abstract int Delete(T entity);

        public abstract int DeleteAll();

        public abstract void Dispose();
    }
}

