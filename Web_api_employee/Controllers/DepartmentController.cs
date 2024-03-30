using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_api_employee.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Http.Cors;
using static System.Net.WebRequestMethods;

namespace Web_api_employee.Controllers
{
    //[EnableCors(origins: "https://localhost:3000", headers:"*", methods:"*")]
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"
                             SELECT DepartmentID, DepartmentName 
                             FROM dbo.Departments 
                            ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString)) 
            using (var cmd = new SqlCommand(query, con)) 
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK,table);
        }
        public string Post(Departments dep)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                             INSERT INTO dbo.Departments VALUES
                             ('"+ dep.DepartmentName + @"')
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to Add";
            }
        }

        public string Put(Departments dep)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                             UPDATE dbo.Departments
                             SET DepartmentName = '" + dep.DepartmentName + @"'
                             WHERE DepartmentID = " + dep.DepartmentID + @"
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to Update";
            }
        }

        public string Delete(int dep )
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                             DELETE FROM dbo.Departments
                             WHERE DepartmentID = " + dep + @"
                            ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully!";
            }
            catch (Exception ex)
            {
                return "Failed to Delete";
            }
        }
    }
}
