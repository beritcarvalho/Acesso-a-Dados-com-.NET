using System;
using Microsoft.Data.SqlClient;

const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";
var connection = new SqlConnection(connectionString);

connection.Open();
connection.Close();
Console.WriteLine("Aberto e Fechado");

using (var connection2 = new SqlConnection(connectionString))
{
    Console.WriteLine("Conectado");
    connection2.Open();

    using (var command = new SqlCommand())
    {
        command.Connection = connection2;
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "SELECT [Id], [Name] FROM [Student]";

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"ID = {reader.GetGuid(0)} - Nome = {reader.GetString(1)}");
        }
    }
}