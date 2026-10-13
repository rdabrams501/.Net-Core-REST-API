using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using SchoolWebAppAPI.Models;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using WebAppAPITest1;
using WebAppAPITest1.Controllers;
using WebAppAPITest1.Models;
using static System.Net.WebRequestMethods;


namespace SchoolWebAppAPI.Controllers
{
    public class LINQStaffController : ControllerBase
    {
        private readonly string connectionString = WebAppConst.CONSTRING;
        private readonly ILogger<StaffController> _logger;
        private List<Staff> listStaff = new List<Staff>();
        DataSet dataSet = new DataSet();


        //Gets a staff by name with custom pathway
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> LinqGet(string name)
        {
            
            try
            {
                await using (var conn = new SqlConnection(connectionString))
                {
                   SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM staff", conn);
                   dataAdapter.Fill(dataSet, "staff");
                }

                var dataTable = dataSet.Tables["staff"];
                var row = dataTable.AsEnumerable().FirstOrDefault(r => r.Field<string>("name").Contains(name));
                if(row == null || row.Field<string>("name") == "")
                {
                    return BadRequest("Row is empty! No staff found!!!!");
                }

                Staff staffer = new Staff(row.Field<int>("staff_id"), row.Field<string>("name"), row.Field<string>("title"), row.Field<string>("status"), row.Field<string>("notes"), row.Field<DateTime>("date_changed"), row.Field<bool>("is_deleted"));

                return Ok(staffer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }
    }
}
