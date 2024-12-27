using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestingAPIS.Models;

namespace TestingAPIS.Controllers
{
    [RoutePrefix("Api/Student")]
    public class StudentController : ApiController
    {
        TestDBEntities DB = new TestDBEntities();

        [Route("AddOrUpdateStudent")]
        [HttpPost]
        public object AddotrUpdatestudent(Student st)
        {
            try
            {
                if (st.Id == 0)
                {
                    studentmaster sm = new studentmaster();
                    sm.name = st.Name;
                    sm.rollno = st.Rollno;
                    sm.address = st.Address;
                    sm.@class = st.Class;
                    DB.studentmasters.Add(sm);
                    DB.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfully"
                    };
                }
                else
                {
                    var obj = DB.studentmasters.Where(x => x.Id == st.Id).ToList().FirstOrDefault();
                    if (obj.Id > 0)
                    {
                        obj.name = st.Name;
                        obj.rollno = st.Rollno;
                        obj.address = st.Address;
                        obj.@class = st.Class;
                        DB.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfully"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }

        [Route("GetAllStudentDetails")]
        [HttpGet]
        public object Studentdetails()
        {
            var a = DB.studentmasters.ToList();
            return a;
        }

        [Route("GetByIdStudentDetail")]
        [HttpGet]
        public object StudentdetailById(int id)
        {
            var obj = DB.studentmasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            return obj;
        }

        [Route("DeleteStudentById")]
        [HttpDelete]
        public object Deletestudent(int id)
        {
            var obj = DB.studentmasters.Where(x => x.Id == id).ToList().FirstOrDefault();
            DB.studentmasters.Remove(obj);
            DB.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
    }
}