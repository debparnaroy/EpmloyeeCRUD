using EmployeeManagementCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementCRUD.Controllers
{
    public class HomeController : Controller
    {
        EmployeeContext db = new EmployeeContext();

        public ActionResult Index()
        {
            var data = db.Employees.ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            Employee objEmployee = new Employee();
            if (ModelState.IsValid)
            {
                objEmployee.Name = employee.Name;
                objEmployee.Email = employee.Email;
                objEmployee.Address = employee.Address;
                objEmployee.Phone = employee.Phone;
                db.Employees.Add(objEmployee);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public JsonResult FindEmployee(int id)
        {
            var emp = db.Employees.Find(id);
            return Json(emp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FindAllEmployee(string id)
        {
            var emp = id;
            return Json(emp, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult EditEmployee(Employee Model)
        {
            db.Entry(Model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            Employee deleteData = db.Employees.Find(id);
            db.Employees.Remove(deleteData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteAllEmployee(string id)
        {
            if (id.Length > 0)
            {
                string[] data = id.Split(',');
                foreach (string s in data)
                {
                    int i = int.Parse(s);
                    var d = db.Employees.Find(i);
                    db.Employees.Remove(d);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}