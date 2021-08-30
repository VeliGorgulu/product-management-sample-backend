using ProductManagementSample.DataAccess.Abstract;
using ProductManagementSample.Entities.Concrete;
using ProductManagementSample.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Concrete.AdoNet
{
    public class AdoNetProductDal : IProductDal
    {
        private SqlConnection _connection;
        public AdoNetProductDal()
        {
            _connection = new ConnectionToSql().Connect();
        }

        public void Add(Product entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddProduct";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            command.Parameters.AddWithValue("@ProductName", entity.ProductName);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Stock", entity.Stock);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Update(Product entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateProduct";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            command.Parameters.AddWithValue("@ProductName", entity.ProductName);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Stock", entity.Stock);

            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Delete(Product entity)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteProduct";
            command.Connection = _connection;

            command.Parameters.AddWithValue("@Id", entity.Id);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        public Product Get(string filter)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Get";
            command.Parameters.AddWithValue("@tableName", "Products");
            command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();


            Product product = null;

            while (reader.Read())
            {
                product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    ProductName = reader["ProductName"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                };
            }

            reader.Close();
            _connection.Close();

            return product;
        }

        public List<Product> GetAll(string filter = null)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAll";
            command.Parameters.AddWithValue("@tableName", "Products");

            if (filter == null)
                command.Parameters.AddWithValue("@query", DBNull.Value);
            else
                command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;


            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    ProductName = reader["ProductName"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                };
                products.Add(product);
            }

            reader.Close();
            _connection.Close();

            return products;
        }

        

        public ProductDetailDto GetProductDetails(string filter)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetProductDetails";

            command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;

            SqlDataReader reader = command.ExecuteReader();


            ProductDetailDto productDetail = null;

            while (reader.Read())
            {
                productDetail = new ProductDetailDto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                };
            }

            reader.Close();
            _connection.Close();

            return productDetail;
        }

        public List<ProductDetailDto> GetAllProductDetails(string filter = null)
        {

            ConnectionControl();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAllProductDetails";

            if (filter == null)
                command.Parameters.AddWithValue("@query", DBNull.Value);
            else
                command.Parameters.AddWithValue("@query", filter);

            command.Connection = _connection;


            SqlDataReader reader = command.ExecuteReader();

            ConnectionControl();

            List<ProductDetailDto> productDetails = new List<ProductDetailDto>();

            while (reader.Read())
            {
                ProductDetailDto product = new ProductDetailDto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    ProductName = reader["ProductName"].ToString(),
                    CategoryName = reader["CategoryName"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                };
                productDetails.Add(product);
            }

            reader.Close();
            _connection.Close();

            return productDetails;
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
