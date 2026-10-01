using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Runtime.Serialization;

namespace etterem.Controllers
{
    [Route("vendeg")]
    [ApiController]
    public class VendegController : Controller
    {
        public string ConnectionString = "server=localhost;database=etterem;uid=root;password=";

        [HttpGet("nameemail")]
        public object GetNameEmail(int id)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();

            string sql = @"SELECT name, email FROM vendeg WHERE `id` = @id";
            var command = new MySqlCommand(sql, connector);
            command.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new { Name = reader["name"], Email = reader["email"] };
            }
            else
            {
                return new { message = "Nincs ilyen vendég" };
            }
        }



        [HttpGet("rendelescount")]
        public object GetOrdercount()
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();

            string sql = @"SELECT COUNT(*) FROM `rendeles`";
            var command = new MySqlCommand(sql, connector);

            long order = Convert.ToInt32(command.ExecuteScalar());

            connector.Close();

            return new { totalPosts = order };
        }

        [HttpGet("namerendeles")]
        public object GetNameorder(int id)
        {
            var connector = new MySqlConnection(ConnectionString);
            connector.Open();

            string sql = @"SELECT COUNT(*) FROM `rendeles` WHERE `vendegId` = @id";
            var command = new MySqlCommand(sql, connector);
            command.Parameters.AddWithValue("@id", id);

            long szamol = Convert.ToInt32(command.ExecuteScalar());

            connector.Close();

            return new { VendegId = id, Rendelesekszama = szamol };
        }



    }
}
