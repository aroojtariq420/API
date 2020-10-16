using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Demo.Models;

namespace WebApi_Demo.Controllers
{
    public class TeacherController : ApiController
    {
        public IEnumerable<Teacher> GetEmployees()
        {
            using (TeacherEntities db = new TeacherEntities())
            {
                return db.Teachers.ToList();
            }
        }
        public HttpResponseMessage GetEmployeeById(int id)
        {
            using (TeacherEntities db = new TeacherEntities())
            {
                var entity = db.Teachers.FirstOrDefault(e => e.ID == id);
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
        public HttpResponseMessage Post([FromBody] Teacher employee)
        {
            try
            {
                using (TeacherEntities db = new TeacherEntities())
                {
                    db.Teachers.Add(employee);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
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
            using (TeacherEntities db = new TeacherEntities())
            {
                var entity = db.Teachers.FirstOrDefault(e => e.ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not found to delete");
                }
                else
                {
                    db.Teachers.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Teacher employee)
        {
            using (TeacherEntities db = new TeacherEntities())
            {
                try
                {
                    var entity = db.Teachers.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.Name = employee.Name;
                     
                        entity.Age = employee.Age;
                     
                        entity.Dep = employee.Dep;
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
