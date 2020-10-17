using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Employee> GetEmployees()
        {
            using (EmployeeMangementSystemEntities db = new EmployeeMangementSystemEntities())
            {
                return db.Employees.ToList();
            }
        }
        public HttpResponseMessage GetEmployeeById(int id)
        {
            using (EmployeeMangementSystemEntities db = new EmployeeMangementSystemEntities())
            {
                var entity = db.Employees.FirstOrDefault(e => e.Employee_ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + " not found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeMangementSystemEntities db = new EmployeeMangementSystemEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.Employee_ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (EmployeeMangementSystemEntities db = new EmployeeMangementSystemEntities())
            {
                var entity = db.Employees.FirstOrDefault(e => e.Employee_ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not found to delete");
                }
                else
                {
                    db.Employees.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            using (EmployeeMangementSystemEntities db = new EmployeeMangementSystemEntities())
            {
                try
                {
                    var entity = db.Employees.FirstOrDefault(e => e.Employee_ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.Employee_Name = employee.Employee_Name;
                        entity.Employee_Sallery = employee.Employee_Sallery;
                        entity.Employee_Age = employee.Employee_Age;
                        
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
