using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_api_employee.Models;

namespace Web_api_employee.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();

            string query = @"
                             SELECT EmployeeID, EmployeeName, Department, MailID, DOJ 
                             FROM dbo.Employees 
                            ";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employees emp)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                             INSERT INTO dbo.Employees(EmployeeName, Department, MailID, DOJ) VALUES
                             (
                              '" + emp.EmployeeName + @"'
                              ,'" + emp.Department + @"'
                              ,'" + emp.MailID + @"'
                              ,'" + emp.DOJ + @"'
                             )
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

        public string Put(Employees emp)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                             UPDATE dbo.Employees
                             SET EmployeeName = '" + emp.EmployeeName + @"'
                             , Department = '" + emp.Department + @"'
                             , MailID = '" + emp.MailID + @"'
                             , DOJ = '" + emp.DOJ + @"'
                             WHERE EmployeeID = " + emp.EmployeeID + @"
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

        public string Delete(Employees emp)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                             DELETE FROM dbo.Employees
                             WHERE EmployeeID = " + emp.EmployeeID + @"
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
