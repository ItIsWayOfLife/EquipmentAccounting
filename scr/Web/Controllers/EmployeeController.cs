using AutoMapper;
using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Employee;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,
            IPositionService positionService,
            IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _positionService = positionService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString, string searchPosition, string searchDepartment)
        {
            var employees = _employeeService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var employeesViewModels = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employees);

            // departments
            List<string> departments = _departmentService.GetAllName().ToList();
            departments.Insert(0, "Отдел");

            if (searchDepartment != string.Empty && searchDepartment != "Отдел" && searchDepartment != null)
            {
                employeesViewModels = employeesViewModels.Where(p => p.DepartmentName == searchDepartment).ToList();
            }

            // position
            List<string> positions = _positionService.GetAllName().ToList();
            positions.Insert(0, "Должность");

            if (searchPosition != string.Empty && searchPosition != "Должность" && searchPosition != null)
            {
                employeesViewModels = employeesViewModels.Where(p => p.PositionName == searchPosition).ToList();
            }

            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Коду", "ФИО", "Телефону" };

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    employeesViewModels = employeesViewModels.Where(p => p.Id.ToString() != null && p.Id.ToString().ToLower().Equals(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    employeesViewModels = employeesViewModels.Where(p => p.Id.ToString() == null || p.Id.ToString() == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    employeesViewModels = employeesViewModels.Where(p => p.FullName != null && p.FullName.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    employeesViewModels = employeesViewModels.Where(p => p.FullName == null || p.FullName == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[3].ToLower() && searchString != string.Empty)
                {
                    employeesViewModels = employeesViewModels.Where(p => p.Phone.ToString().Contains(searchString)).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[3].ToLower() && searchString == string.Empty)
                {
                    employeesViewModels = employeesViewModels.Where(p => p.Phone == null || p.Phone == string.Empty).ToList();
                }

            }

            return View(new EmployeeIndexViewModel()
            {
                EmployeeViewModels = employeesViewModels,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString,
                SearchString = searchString,
                DepartmentSelect = new SelectList(departments),
                SearchDepartment = searchDepartment,
                PositionSelect = new SelectList(positions),
                SearchPosition = searchPosition
            });
        }

        [HttpGet]
        public IActionResult Add(string searchSelectionString, string searchString, string searchPosition, string searchDepartment)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchDepartment = searchDepartment;
            ViewBag.SearchPosition = searchPosition;
           
            // departments
            List<string> departments = _departmentService.GetAllName().ToList();

            // positions
            List<string> positions = _positionService.GetAllName().ToList();

            return View(new EmployeeAddEditViewModel()
            {
                EmployeeViewModel = new EmployeeViewModel() {  },
                DepartmentSelect = new SelectList(departments),
                PositionSelect = new SelectList(positions)
            });
        }

        [HttpPost]
        public IActionResult Add(EmployeeAddEditViewModel model, string searchSelectionString, string searchString, string searchPosition, string searchDepartment)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchDepartment = searchDepartment;
            ViewBag.SearchPosition = searchPosition;

            if (ModelState.IsValid)
            {
                var employeeDTO = new EmployeeDTO()
                {
                    Id = model.EmployeeViewModel.Id,
                    DateOfBirth = model.EmployeeViewModel.DateOfBirth,
                    FullName = model.EmployeeViewModel.FullName,
                    DepartmentName = model.EmployeeViewModel.DepartmentName,
                    Phone = model.EmployeeViewModel.Phone,
                    PositionName = model.EmployeeViewModel.PositionName,
                    Sex = model.EmployeeViewModel.Sex
                };

                try
                {
                    _employeeService.Add(employeeDTO);

                    return RedirectToAction("Index", new
                    {
                        searchSelectionString,
                        searchString,
                        searchPosition,
                        searchDepartment
                    });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            // departments
            List<string> departments = _departmentService.GetAllName().ToList();

            // positions
            List<string> positions = _positionService.GetAllName().ToList();

            model.DepartmentSelect = new SelectList(departments);
            model.PositionSelect = new SelectList(positions);
          
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, string searchSelectionString, string searchString, string searchPosition, string searchDepartment)
        {
            try
            {
                _employeeService.Delete(id);
            }
            catch (ValidationException)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = string.Empty });
            }

            return RedirectToAction("Index", new
            {
                searchSelectionString,
                searchString,
                searchPosition,
                searchDepartment
            });
        }

        [HttpGet]
        public IActionResult Edit(int id, string searchSelectionString, string searchString, string searchPosition, string searchDepartment)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchDepartment = searchDepartment;
            ViewBag.SearchPosition = searchPosition;

            var employeeDTO = _employeeService.Get(id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var employeeViewModel = mapper.Map<EmployeeDTO, EmployeeViewModel>(employeeDTO);

            // departments
            List<string> departments = _departmentService.GetAllName().ToList();

            // positions
            List<string> positions = _positionService.GetAllName().ToList();

            return View(new EmployeeAddEditViewModel()
            {
                EmployeeViewModel = employeeViewModel,
                DepartmentSelect = new SelectList(departments),
                PositionSelect = new SelectList(positions)
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeeAddEditViewModel model, string searchSelectionString, string searchString, string searchPosition, string searchDepartment)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchDepartment = searchDepartment;
            ViewBag.SearchPosition = searchPosition;

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeViewModel, EmployeeDTO>()).CreateMapper();
                var employeeDTO = mapper.Map<EmployeeViewModel, EmployeeDTO>(model.EmployeeViewModel);

                try
                {
                    _employeeService.Edit(employeeDTO);

                    return RedirectToAction("Index", new
                    {
                        searchSelectionString,
                        searchString,
                        searchPosition,
                        searchDepartment
                    });
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
