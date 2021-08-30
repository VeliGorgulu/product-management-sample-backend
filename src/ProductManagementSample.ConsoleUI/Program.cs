using ProductManagementSample.Business.Abstract;
using ProductManagementSample.Business.Concrete;
using ProductManagementSample.DataAccess.Abstract;
using ProductManagementSample.DataAccess.Concrete.AdoNet;
using ProductManagementSample.Entities.Concrete;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProductManagementSample.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["ProductManagementContext"].ConnectionString;
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductManagementContext"].ConnectionString);
            //IProductService _productService = new ProductManager(new AdoNetProductDal());

            //var products = _productService.GetAllProductDetailsByCategoryId(1).Data;
            //var array = new string[] { "sasd", "sadsd" };
            //Console.WriteLine(array.Length);
            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.ProductName + " == " + product.CategoryName);
            //}

            //var obj1 = new byte[] { 1, 2, 3 };
            //var obj2 = new byte[] { 1, 2, 3 };
            //var result = obj1.Equals(obj2.GetValue())
         
            //Console.WriteLine(result.ToString());
            //CategoryManager categoryManager = new CategoryManager(new AdoNetCategoryDal(connection));

            //var categories = categoryManager.GetByCategoryName("Beverages");

            //foreach (var category in categories.Data)
            //{
            //    Console.WriteLine(category.CategoryName);
            //}

            //AdoNetProductDal adoNetProductDal = new AdoNetProductDal(connection);

            ////var product = adoNetProductDal.GetProductDetails("p.Id = 1");
            ////Console.WriteLine(product.ProductName + " === " + product.CategoryName);
            ////var products = adoNetProductDal.Get("Price > 10");
            ////Console.WriteLine(products.ProductName + " === " + products.Price);
            //var products = adoNetProductDal.GetAllProductDetails("p.Price > 20");

            ////var products = adoNetProductDal.GetAllProductDetails("Price > 20");
            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.ProductName + " === " + product.CategoryName + " === " + product.Price);
            //}

            //AdoNetCategoryDal adoNetCategoryDal = new AdoNetCategoryDal(connection);
            //adoNetCategoryDal.Delete(new Category() { Id = 2002});
            //var category = adoNetCategoryDal.Get("Id = 2");
            //Console.WriteLine(category.CategoryName);
            //foreach (var category in categories)
            //{
            //    Console.WriteLine(category.CategoryName);
            //}
            //AdoNetCategoryDal adoNetCategoryDal = new AdoNetCategoryDal(new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ProductManagementDb;integrated security=true"));
            //adoNetCategoryDal.Delete(new Category() { Id = 1002 });
            //AdoNetProductDal adoNetProductDal = new AdoNetProductDal(new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ProductManagementDb;integrated security=true"));
            //Product product = new Product()
            //{
            //    Id = 1002,
            //};
            //adoNetProductDal.Delete(product);
            //var products = adoNetProductDal.GetAll("Select * from Products where Id<5");
            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.ProductName);
            //}
            //Console.WriteLine(product.ProductName);
            //var filter = "Select * from Products where Id=2";
            //var category = efCategoryDal.Get(filter);
            //Console.WriteLine(category.CategoryName);
            ////foreach (var category in categories)
            ////{
            ////    Console.WriteLine(category.CategoryName);
            ////}
            ////Console.WriteLine("Hello World!");
        }
    }
}
