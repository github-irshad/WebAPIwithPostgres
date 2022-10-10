using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using PostgresWebApi.Models;

namespace PostgresWebApi.Controllers{
  [Route("[controller]")]
  [ApiController]
  public class EmployeeController:ControllerBase
  {
    private readonly IConfiguration _configuration ;

    public EmployeeController(IConfiguration configuration){
      _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
      string query = @"select EmployeeId as EmployeeId, EmployeeName as EmployeeName,
      Department as Department,to_char(DateofJoining,'YYYY-MM-DD') as DateofJoining, 
      PhotoFileName as PhotoFileName from Employee";
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


    [HttpPost]
    public JsonResult Post(Employee emp)
    {
      string query = @"insert into Employee (EmployeeName,Department,DateofJoining,PhotoFileName) 
      values (@EmployeeName,@Department,@DateofJoining,@PhotoFileName)";
      DataTable table = new DataTable();

      string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

      NpgsqlDataReader myReader;
      using(NpgsqlConnection mycon =new NpgsqlConnection(sqlDataSource))
      {
        mycon.Open();
        
        using(NpgsqlCommand mycommand = new NpgsqlCommand(query,mycon))
        {
          mycommand.Parameters.AddWithValue("@EmployeeName",emp.EmployeeName);
          mycommand.Parameters.AddWithValue("@Department",emp.Department);
          mycommand.Parameters.AddWithValue("@DateOfJoining",Convert.ToDateTime(emp.DateOfJoining));
          mycommand.Parameters.AddWithValue("@PhotoFileName",emp.PhotoFileName);
          myReader = mycommand.ExecuteReader();
          table.Load(myReader);

          myReader.Close();
          mycon.Close();
        }
      }


      return new JsonResult("Added Successfully");
    }


    [HttpPut]
    public JsonResult Put(Employee emp)
    {
      string query = @"update Employee
       set EmployeeName = @EmployeeName,
       Department = @Department,
       DateofJoining = @DateofJoining,
       PhotoFileName = @PhotoFileName where EmployeeId = @EmployeeId" ;
      DataTable table = new DataTable();

      string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

      NpgsqlDataReader myReader;
      using(NpgsqlConnection mycon =new NpgsqlConnection(sqlDataSource))
      {
        mycon.Open();
        
        using(NpgsqlCommand mycommand = new NpgsqlCommand(query,mycon))
        {
         mycommand.Parameters.AddWithValue("@EmployeeId",emp.EmployeeId);
         mycommand.Parameters.AddWithValue("@EmployeeName",emp.EmployeeName);
          mycommand.Parameters.AddWithValue("@Department",emp.Department);
          mycommand.Parameters.AddWithValue("@DateOfJoining",Convert.ToDateTime(emp.DateOfJoining));
          mycommand.Parameters.AddWithValue("@PhotoFileName",emp.PhotoFileName);
          myReader = mycommand.ExecuteReader();
          table.Load(myReader);

          myReader.Close();
          mycon.Close();
        }
      }


      return new JsonResult("Updated Successfully");
    }


    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
      string query = @"delete from Employee where EmployeeId = @EmployeeId";
      DataTable table = new DataTable();

      string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

      NpgsqlDataReader myReader;
      using(NpgsqlConnection mycon =new NpgsqlConnection(sqlDataSource))
      {
        mycon.Open();
        
        using(NpgsqlCommand mycommand = new NpgsqlCommand(query,mycon))
        {
          mycommand.Parameters.AddWithValue("@EmployeeId",id);
          
          myReader = mycommand.ExecuteReader();
          table.Load(myReader);

          myReader.Close();
          mycon.Close();
        }
      }


      return new JsonResult("Deleted Successfully");
    }

    [HttpGet("{id}")]
    public JsonResult Get(int id)
    {
      string query = @"select EmployeeId as EmployeeId, EmployeeName as EmployeeName,
      Department as Department,to_char(DateofJoining,'YYYY-MM-DD') as DateofJoining, 
      PhotoFileName as PhotoFileName from Employee where EmployeeId = @EmployeeId";
      DataTable table = new DataTable();

      string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

      NpgsqlDataReader myReader;
      using(NpgsqlConnection mycon =new NpgsqlConnection(sqlDataSource))
      {
        mycon.Open();
        
        using(NpgsqlCommand mycommand = new NpgsqlCommand(query,mycon))
        {
          mycommand.Parameters.AddWithValue("@EmployeeId",id);
          
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