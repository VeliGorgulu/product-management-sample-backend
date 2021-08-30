using ProductManagementSample.Core.Entities.Concrete;
using ProductManagementSample.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Concrete.AdoNet
{
    public class AdoNetUserDal : IUserDal
    {
        private SqlConnection _connection;
        public AdoNetUserDal()
        {
            _connection = new ConnectionToSql().Connect();
        }

        public void Add(User entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddUser";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", entity.PasswordSalt);
            command.Parameters.AddWithValue("@Status", entity.Status);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void Update(User entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateUser";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            command.Parameters.AddWithValue("@PasswordSalt", entity.PasswordSalt);
            command.Parameters.AddWithValue("@Status", entity.Status);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Delete(User entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteUser";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@Id", entity.Id);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public User Get(string filter)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Get";
            command.Parameters.AddWithValue("@tableName", "Users");
            command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();

            User user = null;

            while (reader.Read())
            {
                user = new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["FirstName"].ToString(),
                    Email = reader["Email"].ToString(),
                    PasswordHash = reader["PasswordHash"] as byte[],
                    PasswordSalt = reader["PasswordSalt"] as byte[],
                    Status = Convert.ToBoolean(reader["Status"])
                };
            }

            reader.Close();
            _connection.Close();

            return user;
        }

        public List<User> GetAll(string filter = null)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAll";
            command.Parameters.AddWithValue("@tableName", "Users");

            if (filter == null)
                command.Parameters.AddWithValue("@query", DBNull.Value);
            else
                command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();

            List<User> users = new ();

            while (reader.Read())
            {
                User user = new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["FirstName"].ToString(),
                    Email = reader["Email"].ToString(),
                    PasswordHash = reader["PasswordHash"] as byte[],
                    PasswordSalt = reader["PasswordSalt"] as byte[],
                    Status = Convert.ToBoolean(reader["Status"])
                };
                users.Add(user);
            }

            reader.Close();
            _connection.Close();

            return users;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetClaims";
            command.Parameters.AddWithValue("@userId", user.Id);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();


            List<OperationClaim> operationClaims = new List<OperationClaim>();

            while (reader.Read())
            {
                OperationClaim operationClaim = new OperationClaim
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                };
                operationClaims.Add(operationClaim);
            }

            reader.Close();
            _connection.Close();

            return operationClaims;
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
