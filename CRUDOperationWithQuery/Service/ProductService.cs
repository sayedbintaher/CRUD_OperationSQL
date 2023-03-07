using CRUDOperationWithQuery.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CRUDOperationWithQuery.Service
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void AddProduct(Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("spAddProduct", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Name", product.Name);
                sqlCmd.Parameters.AddWithValue("@Code", product.Code);
                sqlCmd.Parameters.AddWithValue("@Type", product.Type);
                sqlCmd.Parameters.AddWithValue("@Price", product.Price);
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
  
        public ICollection<Product> GetAllProducts()
        {
            List<Product> listOfProducts = new List<Product>();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("spGetAllProducts", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = sqlCmd.ExecuteReader();
                while (rdr.Read())
                {
                    listOfProducts.Add(new Product
                    {
                        ID = Convert.ToInt32(rdr["ID"]),
                        Name = rdr["Name"].ToString(),
                        Code = rdr["Code"].ToString(),
                        Type = rdr["Type"].ToString(),
                        Price = Convert.ToInt32(rdr["Price"])
                    });
                }
            }
            return listOfProducts;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                string sqlQuery = "SELECT * FROM Product WHERE Id= " + id;
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConnection);
                SqlDataReader rdr = sqlCmd.ExecuteReader();
                while (rdr.Read())
                {
                    product.ID = Convert.ToInt32(rdr["ID"]);
                    product.Name = rdr["Name"].ToString();
                    product.Code = rdr["Code"].ToString();
                    product.Type = rdr["Type"].ToString();
                    product.Price = Convert.ToInt32(rdr["Price"]);
                }
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("spUpdateProduct", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ID", product.ID);
                sqlCmd.Parameters.AddWithValue("@Name", product.Name);
                sqlCmd.Parameters.AddWithValue("@Code", product.Code);
                sqlCmd.Parameters.AddWithValue("@Type", product.Type);
                sqlCmd.Parameters.AddWithValue("@Price", product.Price);
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("spDeleteProduct", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("ID", id);
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
