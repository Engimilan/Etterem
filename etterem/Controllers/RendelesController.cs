using etterem.models;
using etterem.models.DTOs;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;


namespace etterem.Controllers
{
    [Route("rendeles")]
    [ApiController]
    
    public class RendelesController : ControllerBase
    {
        public string ConnetionString = "server=localhost;database=etterem;uid=root;password=";
        [HttpGet("listall")]
        public object GetAllRendeles()
        {
            List<Rendeles> rendelesek = new List<Rendeles>();

            var connector = new MySqlConnection(ConnetionString);

            connector.Open();

            string sql = @"SELECT * FROM `rendeles`";
            var command = new MySqlCommand(sql, connector);
            var datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                var order = new Rendeles
                {
                    id = datareader.GetInt32(0),
                    dish = datareader.GetString(1),
                    description = datareader.GetString(2),
                    orderTime = datareader.GetDateTime(3),
                    updateTime = datareader.GetDateTime(4),
                    vendegId = datareader.GetInt32(5)
                };
                rendelesek.Add(order);
            }

            connector.Close();

            return new { message = "Sikeres", rendelesek };
        }

        [HttpGet("byId")]
        public object GetrendelesById(int id)
        {
            var connector = new MySqlConnection(ConnetionString);

            connector.Open();

            string sql = @"SELECT * FROM `rendeles` WHERE `id`= @id;";
            var command = new MySqlCommand(sql, connector);
            command.Parameters.AddWithValue("@id", id);
            var datareader = command.ExecuteReader();

            datareader.Read();

            var order = new Rendeles
            {
                id = datareader.GetInt32(0),
                dish = datareader.GetString(1),
                description = datareader.GetString(2),
                orderTime = datareader.GetDateTime(3),
                updateTime = datareader.GetDateTime(4),
                vendegId = datareader.GetInt32(5)
            };
            connector.Close();
            return new { message = "Siker", result = order };
        }

        [HttpPost("neworder")]
        public object AddNewOrder(NewOrderDTO neworder)
        {
            var connector = new MySqlConnection(ConnetionString);

            connector.Open();

            string sql = @"INSERT INTO `rendeles`(`dish`, `description`, `orderTime`, `updateTime`, `vendegId`) VALUES(@dish,@description,@orderTime,@updateTime,@vendegid)";
            var command = new MySqlCommand(sql, connector);

            command.Parameters.AddWithValue("@dish", neworder.dish);
            command.Parameters.AddWithValue("@description", neworder.description);
            command.Parameters.AddWithValue("@orderTime", DateTime.Now);
            command.Parameters.AddWithValue("@updateTime", DateTime.Now);
            command.Parameters.AddWithValue("@vendegId", neworder.vendegId);
            command.ExecuteNonQuery();

            connector.Close();
            return new { message = "Siker", results = neworder };
        }

        [HttpPut("update")]
        public object UpdateRendeles([FromQuery] int id, [FromBody] OrderUpdateDTO orderupdate)
        {
            var connector = new MySqlConnection(ConnetionString);

            connector.Open();

            string sql = @"UPDATE `rendeles` SET `dish`=@dish, `description`=@description, `updateTime`=@updateTime, `vendegId`=@vendegId WHERE `id`= @id;";
            var command = new MySqlCommand(sql, connector);

            command.Parameters.AddWithValue("@dish", orderupdate.dish);
            command.Parameters.AddWithValue("@description", orderupdate.description);
            command.Parameters.AddWithValue("@updateTime", DateTime.Now);
            command.Parameters.AddWithValue("@vendegId", orderupdate.vendegId);
            command.Parameters.AddWithValue("@id", id);

            object result = command.ExecuteNonQuery() > 0 ? new { message = "Siker" } : new { message = "Nem letezik ez a rendeles." };
            connector.Close();

            return result;
        }

        [HttpDelete("Delete")]

        public object DeleteBlogger([FromBody] int id)
        {
            var connector = new MySqlConnection(ConnetionString);

            connector.Open();

            string sql = @"DELETE FROM `rendeles` WHERE `id`= @id;";
            var command = new MySqlCommand(sql, connector);

            command.Parameters.AddWithValue("@id", id);

            object result = command.ExecuteNonQuery() > 0 ? new { message = "Siker" } : new { message = "Nem letezik a rendeles." };

            connector.Close();
            return result;
        }







    }
}
