using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace PostgresWebApi.Controllers{
  [Route("")]
  [ApiController]
  public class DepartmentController:ControllerBase
  {
    private readonly IConfiguration _configuration ;

    public DepartmentController(IConfiguration configuration){
      _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
      string query = @"select DepartmentId as DepartmentId, DepartmentName as DepartmentName from Department";
      DataTable table = new DataTable();

      string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

      NpgsqlDataReader myReader;
      using(NpgsqlConnection mycon =new NpgsqlConnection(sqlDataSource))
      {
        mycon.Open();
        
        using(NpgsqlCommand mycommand = new NpgsqlCommand(query,mycon))
        {
          myReader = mycommand.ExecuteReader();
          table.Load(myReader);

          myReader.Close();
          mycon.Close();
        }
      }


      return new JsonResult(table);
    }
  }
}