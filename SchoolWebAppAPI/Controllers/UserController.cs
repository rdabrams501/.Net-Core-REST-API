using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using SchoolWebAppAPI.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using WebAppAPITest1.Models;

namespace WebAppAPITest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User userLogin)
        {
            try
            {
                using var conn = new SqlConnection($"server = rdabramsschool.database.windows.net; user = {userLogin.Username}; password = {userLogin.Password}; database = schooldb; Connection Timeout = 30");
                conn.Open();
                WebAppConst.CONSTRING = $"server = rdabramsschool.database.windows.net; user = {userLogin.Username}; password = {userLogin.Password}; database = schooldb; Connection Timeout = 30";
                return Ok("Successful Login!");
            }
            catch (Exception ex)
            {
                WebAppConst.CONSTRING = WebAppConst.ORIGINAL_CONSTRING;
                return BadRequest(ex.Message);
            }
        }
    }
}
