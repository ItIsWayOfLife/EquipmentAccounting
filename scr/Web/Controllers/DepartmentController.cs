using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Department;
using Web.Models.StatusEquipment;

namespace Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString)
        {
            var departmemts = _departmentService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Department, DepartmentViewModel>()).CreateMapper();
            var departmemtsViewModels = mapper.Map<IEnumerable<Department>, List<DepartmentViewModel>>(departmemts);

            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Id", "Названию" };

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    departmemtsViewModels = departmemtsViewModels.Where(p => p.Id.ToString() != null && p.Id.ToString().ToLower().Equals(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    departmemtsViewModels = departmemtsViewModels.Where(p => p.Id.ToString() == null || p.Id.ToString() == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    departmemtsViewModels = departmemtsViewModels.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    departmemtsViewModels = departmemtsViewModels.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
            }

            return View(new DepartmentIndexViewModel()
            {
                DepartmentViewModels = departmemtsViewModels,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString,
                SearchString = searchString
            });
        }

        [HttpGet]
        public IActionResult Add(string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            return View();
        }

        [HttpPost]
        public IActionResult Add(DepartmentViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                try
{
                    _departmentService.Add(department);

                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, string searchSelectionString, string searchString)
        {
            try
            {
                _departmentService.Delete(id);
            }
            catch (ValidationException)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = string.Empty });
            }

            return RedirectToAction("Index", new { searchSelectionString, searchString });
        }

        [HttpGet]
        public IActionResult Edit(int id, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            var department = _departmentService.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Department, DepartmentViewModel>()).CreateMapper();
            var departmentViewModel = mapper.Map<Department, DepartmentViewModel>(department);

            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentViewModel, Department>()).CreateMapper();
                var departnment = mapper.Map<DepartmentViewModel, Department>(model);

                try
                {
                    _departmentService.Edit(departnment);

                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

    }
}
