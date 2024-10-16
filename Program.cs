using Microsoft.Data.SqlClient;
using System.Data;

class Program
{
    public static void Main()
    {
        // Example usage of InsertData and GetData
        InsertData("John Doe", "25");
        GetData();
    }

    public static void InsertData(string value1, string value2)
    {
        // Connection string to connect to the database
        string connString = @"Data Source=PR3DATA;Initial Catalog=GitongaShopDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        // Create a new SqlConnection object
        using (SqlConnection sqlConnection = new SqlConnection(connString))
        {
            try
            {
                // Open the connection
                sqlConnection.Open();

                // Define the SQL command with parameters
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Users (Names, age) VALUES (@Value1, @Value2)", sqlConnection);

                // Add parameters to prevent SQL injection
                sqlCommand.Parameters.AddWithValue("@Value1", value1);
                sqlCommand.Parameters.AddWithValue("@Value2", value2);

                // Execute the command and get the number of rows affected
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                // Inform the user about the operation success
                Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
            }
            catch (SqlException ex)
            {
                // Handle any SQL-related errors
                Console.WriteLine("Error Occurred: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }

    public static void GetData()
    {
        string connString = @"Data Source=PR3DATA;Initial Catalog=GitongaShopDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        using (SqlConnection sqlConnection = new SqlConnection(connString))
        {
            try
            {
                // Open the connection
                sqlConnection.Open();

                // Define the SQL command to fetch data
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM Users", sqlConnection);

                // Execute the command and get the data using SqlDataReader
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    // Loop through all the rows
                    while (sqlDataReader.Read())
                    {
                        // Example: Assuming Users table has columns 'Names' and 'age'
                        string name = sqlDataReader["Names"].ToString();
                        string age = sqlDataReader["age"].ToString();

                        Console.WriteLine($"Name: {name}, Age: {age}");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error Occurred: " + ex.Message);
            }
            finally
            {
                // Close the connection
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
