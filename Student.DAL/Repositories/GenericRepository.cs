using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Students.Models; 
using Students.DAL.Interfaces;
using Students.DAL;
using Utility.Configuration;
using Dapper;

namespace Students.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /*private static readonly IDbConnection _dbConnection;
        private static readonly string connectionString;\
*/
        /* public GenericRepository()
         {
             connectionString = AppSettings.SQL_Get_ConnectionString();
             _dbConnection = new SqlConnection(connectionString);
             BaseRepository.SetDialect(BaseRepository.Dialect.SQLServer);
         }*/

        public IEnumerable<T> GetAll() 
        {
            return (IEnumerable<T>)BaseRepository.GetAll<T>();
        }


        public T FindById(int id)
        {
            return BaseRepository.Get<T>(id);
        }

        public void Add(T entity)
        {
            BaseRepository.Insert(entity);
        }

        public void Delete(T entity)
        {
            BaseRepository.Delete(entity);
        }

        public void Update(T entity)
        {
            BaseRepository.Update(entity);
        }

        public void DeleteAll()
        {
            BaseRepository.DeleteList<T>();
        }

        public User FindByUsername(string username)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Username", username);
            string query = "SELECT * FROM Users WHERE Username = @Username";
            return BaseRepository.CustomQuery<User>(query, param).Result;
        }
    }
}
