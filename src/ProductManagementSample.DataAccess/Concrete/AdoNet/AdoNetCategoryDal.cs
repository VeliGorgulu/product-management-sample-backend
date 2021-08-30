using ProductManagementSample.DataAccess.Abstract;
using ProductManagementSample.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Concrete.AdoNet
{
    public class AdoNetCategoryDal : ICategoryDal
    {
        private SqlConnection _connection;
        public AdoNetCategoryDal()
        {
            _connection = new ConnectionToSql().Connect();
        }

        public void Add(Category entity)
        {
            
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddCategory";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
            
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void Update(Category entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateCategory";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
            command.Parameters.AddWithValue("@Id", entity.Id);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Delete(Category entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteCategory";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@Id", entity.Id);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public Category Get(string filter)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Get";
            command.Parameters.AddWithValue("@tableName", "Categories");
            command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();

            Category category = null;

            while (reader.Read())
            {
                category = new Category
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CategoryName = reader["CategoryName"].ToString(),
                };
            }

            reader.Close();
            _connection.Close();

            return category;
        }

        public List<Category> GetAll(string filter = null)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAll";
            command.Parameters.AddWithValue("@tableName", "Categories");

            if (filter == null)
                command.Parameters.AddWithValue("@query", DBNull.Value);
            else
                command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();

            List<Category> categories = new List<Category>();

            while (reader.Read())
            {
                Category category = new Category
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CategoryName = reader["CategoryName"].ToString(),
                };
                categories.Add(category);
            }

            reader.Close();
            _connection.Close();

            return categories;
        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}
