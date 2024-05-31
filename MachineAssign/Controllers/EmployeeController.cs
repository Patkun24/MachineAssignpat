using MachineAssign.Models;
using MachineAssign.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace MachineAssign.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;

        public EmployeeController(IWebHostEnvironment webHostEnvironment, INotyfService notyf)
        {
            _webHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult GetAllEmpDetails()
        {

            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            List<EmpModel> EmpList = new List<EmpModel>();
            List<Countrymodel> CountryList = new List<Countrymodel>();
            EmpModel empmodel = new EmpModel();
            EmpList = EmpRepo.GetAllEmployees();
            CountryList = EmpRepo.GetCountry();
            ViewBag.Country = CountryList;
            ViewBag.Employee = EmpList;
            return View(empmodel);
        }

        public ViewResult CreateEmployee()
        {

            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            List<Countrymodel> CountryList = new List<Countrymodel>();
            CountryList = EmpRepo.GetCountry();
            ViewBag.Country = CountryList;
            return View();
        }

        public ViewResult EditEmployee(int id)
        {

            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            List<Countrymodel> CountryList = new List<Countrymodel>();
            List<StateModel> StateList = new List<StateModel>();
            List<Citymodel> CityList = new List<Citymodel>();
            List<EmpModel> EmpList = new List<EmpModel>();
            EmpList = EmpRepo.GetAllEmployees();
            EmpModel empmodel = EmpList.FirstOrDefault(x => x.Id == id);
            CountryList = EmpRepo.GetCountry();
            Countrymodel countrymodel = CountryList.FirstOrDefault(x => x.Countryname == empmodel.Country);
            //ViewBag.Employee = EmpList;
            ViewBag.Employee = empmodel;
            StateList = GetStates(countrymodel.Id);
            StateModel statemodel = StateList.FirstOrDefault(x => x.Statename == empmodel.State);
            CityList = Getcity(statemodel.Id);
            ViewBag.Country = CountryList;
            ViewBag.State = StateList;
            ViewBag.City = CityList;
            return View(empmodel);
        }

        public List<StateModel> GetStates(int countryId)
        {
            EmpRepository EmpRepo = new EmpRepository();
            List<StateModel> StateList = new List<StateModel>();
            StateList = EmpRepo.GetState(countryId);

            return StateList;
        }

        public List<Citymodel> Getcity(int stateId)
        {
            EmpRepository EmpRepo = new EmpRepository();
            List<Citymodel> CityList = new List<Citymodel>();
            CityList = EmpRepo.GetCity(stateId);

            return CityList;
        }

        public List<Citymodel> newmethod89(int stateId)
        {
            EmpRepository EmpRepo = new EmpRepository();
            List<Citymodel> CityList = new List<Citymodel>();
            CityList = EmpRepo.GetCity(stateId);

            return CityList;
        }

        public async Task<IActionResult> Delete(int id)
        {
            EmpRepository EmpRepo = new EmpRepository();
             EmpRepo.DeleteEmp( id);
            //_notyf.Warning(ViewBag.DeleteRegionSuccess, 6);
            _notyf.Success("deleted Successfully", 6);
            return  RedirectToAction("GetAllEmpDetails", "Employee");
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmpModel model)
        {
            EmpRepository EmpRepo = new EmpRepository();
            var profilePath = "/uploads/Employee/";




            if (ModelState.IsValid)
            {

                if (model.profilepicture != null && model.profilepicture.Length > 0)
                {

                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/Employee");
                    var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{new Random().Next(1000, 9999)}" + "_" + model.profilepicture.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    profilePath = profilePath + uniqueFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.profilepicture.CopyToAsync(stream);
                    }

                }

                EmpRepo.InsertEmp(model, profilePath);
                return RedirectToAction("GetAllEmpDetails", "Employee");
            }
            else
            {
                List<Countrymodel> CountryList = new List<Countrymodel>();
                CountryList = EmpRepo.GetCountry();
                ViewBag.Country = CountryList;
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult EditEmployee(EmpModel model)
        {
            EmpRepository EmpRepo = new EmpRepository();
            var profilePath = "/uploads/Employee/";




            if (ModelState.IsValid)
            {

                if (model.profilepicture != null && model.profilepicture.Length > 0)
                {

                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/Employee");
                    var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}{new Random().Next(1000, 9999)}" + "_" + model.profilepicture.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    profilePath = profilePath + uniqueFileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.profilepicture.CopyToAsync(stream);
                    }

                }

                EmpRepo.UpdateEmp(model, profilePath);
                return RedirectToAction("GetAllEmpDetails", "Employee");
            }
            else
            {
                List<Countrymodel> CountryList = new List<Countrymodel>();
                List<StateModel> StateList = new List<StateModel>();
                List<Citymodel> CityList = new List<Citymodel>();
                CountryList = EmpRepo.GetCountry();
                ViewBag.Country = CountryList;
                CountryList = EmpRepo.GetCountry();
                Countrymodel countrymodel = CountryList.FirstOrDefault(x => x.Id == Convert.ToInt32(model.Country));
                //ViewBag.Employee = EmpList;
                ViewBag.Employee = model;
                if(countrymodel!=null)
                {
                    StateList = GetStates(countrymodel.Id);

                }
                
                if(model.State!=null)
                {
                    StateModel statemodel = StateList.FirstOrDefault(x => x.Id == Convert.ToInt32(model.State));
                    CityList = Getcity(statemodel.Id);
                    ViewBag.City = CityList;
                }
                
                ViewBag.Country = CountryList;
                ViewBag.State = StateList;
                


                return View(model);
            }
            
        }
    }
}
