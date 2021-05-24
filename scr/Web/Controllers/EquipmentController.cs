using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Employee;
using Web.Models.Equipment;

namespace Web.Controllers
{
    public class EquipmentController : Controller 
    {
        public readonly IEquipmentService _equipmentService;
        public readonly IStatusEquipmentService _statusEquipmentService;
        public readonly IEmployeeService _employeeService;

        public EquipmentController(IEquipmentService equipmentService,
            IStatusEquipmentService statusEquipmentService,
            IEmployeeService employeeService)
        {
            _equipmentService = equipmentService;
            _statusEquipmentService = statusEquipmentService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee)
        {
            var equipmentDTOs = _equipmentService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentDTO, EquipmentViewModel>()).CreateMapper();
            var equipmentViewModels = mapper.Map<IEnumerable<EquipmentDTO>, List<EquipmentViewModel>>(equipmentDTOs);

         
            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            statusEquipments.Insert(0, "Статус");

            if (searchStatusEquipment != string.Empty && searchStatusEquipment != "Статус" && searchStatusEquipment != null)
            {
                equipmentViewModels = equipmentViewModels.Where(p => p.StatusEquipmentName == searchStatusEquipment).ToList();
            }

            // employees
            List<string> employees = _employeeService.GetAllIdName().ToList();
            employees.Insert(0, "Работник");

            if (searchEmployee != string.Empty && searchEmployee != "Работник" && searchEmployee != null)
            {
                string[] empSearchArray = searchEmployee.Split('|');
                equipmentViewModels = equipmentViewModels.Where(p => p.EmployeeFullName == empSearchArray[1]).ToList();
            }


            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Коду", "Инвентарному номеру", "Названию", "Цене", "Сроку полезного использования", "Годовой ставке", "Сумме отчислений в месяц" };

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Id.ToString() != null && p.Id.ToString().ToLower().Equals(searchString.ToLower())).ToList();
                }
                else if (searchSelection[1].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Id.ToString() == null || p.Id.ToString() == string.Empty).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.InventoryNumber != null && p.InventoryNumber.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[2].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.InventoryNumber == null || p.InventoryNumber == string.Empty).ToList();
                }
                else if (searchSelection[3].ToLower() == searchSelectionString.ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Name != null && p.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelection[3].ToLower() == searchSelectionString.ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Name == null || p.Name == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[4].ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Price.ToString().Contains(searchString)).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[4].ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Price == 0).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[5].ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Term.ToString().Contains(searchString)).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[5].ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.Term == 0).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[6].ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.ProcentYear.ToString().Contains(searchString)).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[6].ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.ProcentYear == 0).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[7].ToLower() && searchString != string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => p.DeductionAmountPerMonth.ToString().Contains(searchString)).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[7].ToLower() && searchString == string.Empty)
                {
                    equipmentViewModels = equipmentViewModels.Where(p => Convert.ToDouble(p.DeductionAmountPerMonth) == 0).ToList();
                }
            }

            return View(new EquipmentIndexViewModel()
            {
                EquipmentViewModels = equipmentViewModels,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString,
                SearchString = searchString,
                SearchStatusEquipment = searchStatusEquipment,
                StatusEquipmentSelect = new SelectList(statusEquipments),
                SearchEmployee = searchEmployee,
                EmployeeSelect = new SelectList(employees)
            });
        }

        [HttpGet]
        public IActionResult Add(string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            // employees
            List<string> employees = _employeeService.GetAllIdName().ToList();

            return View(new EquipmentAddEditViewModel()
            {
                EquipmentViewModel = new EquipmentViewModel() { },
                StatusEquipmentsSelect = new SelectList(statusEquipments),
                EmployeeSelect = new SelectList(employees)
            });
        }

        [HttpPost]
        public IActionResult Add(EquipmentAddEditViewModel model, string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;

            if (ModelState.IsValid)
            {
                var equipmentDTO = new EquipmentDTO()
                {
                    Id = model.EquipmentViewModel.Id,
                    DeductionAmountPerMonth = model.EquipmentViewModel.DeductionAmountPerMonth,
                    EmployeeFullName = model.EquipmentViewModel.EmployeeFullName,
                    InventoryNumber = model.EquipmentViewModel.InventoryNumber,
                    Name = model.EquipmentViewModel.Name,
                    Price = model.EquipmentViewModel.Price,
                    ProcentYear = model.EquipmentViewModel.ProcentYear,
                    StatusEquipmentName = model.EquipmentViewModel.StatusEquipmentName,
                    Term = model.EquipmentViewModel.Term
                };

                try
                {
                    _equipmentService.Add(equipmentDTO);

                    return RedirectToAction("Index", new
                    {
                        searchSelectionString,
                        searchString,
                        searchStatusEquipment,
                        searchEmployee
                    });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            // employees
            List<string> employees = _employeeService.GetAllIdName().ToList();

            model.StatusEquipmentsSelect = new SelectList(statusEquipments);
            model.EmployeeSelect = new SelectList(employees);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee)
        {
            try
            {
                _equipmentService.Delete(id);
            }
            catch (ValidationException)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = string.Empty });
            }

            return RedirectToAction("Index", new
            {
                searchSelectionString,
                searchString,
                searchStatusEquipment,
                searchEmployee
            });
        }

        [HttpGet]
        public IActionResult Edit(int id, string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;

            var equipmentDTO = _equipmentService.Get(id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentDTO, EquipmentViewModel>()).CreateMapper();
            var equipmentViewModel = mapper.Map<EquipmentDTO, EquipmentViewModel>(equipmentDTO);

            equipmentViewModel.EmployeeFullName = equipmentDTO.EmployeeId + "|" + equipmentDTO.EmployeeFullName;

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            // employees
            List<string> employees = _employeeService.GetAllIdName().ToList();


            return View(new EquipmentAddEditViewModel()
            {
                EquipmentViewModel = equipmentViewModel,
                StatusEquipmentsSelect = new SelectList(statusEquipments),
                EmployeeSelect = new SelectList(employees)
            });
        }

        [HttpPost]
        public IActionResult Edit(EquipmentAddEditViewModel model, string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentViewModel, EquipmentDTO>()).CreateMapper();
                var equipmentDTO = mapper.Map<EquipmentViewModel, EquipmentDTO>(model.EquipmentViewModel);

                try
                {
                    _equipmentService.Edit(equipmentDTO);

                    return RedirectToAction("Index", new
                    {
                        searchSelectionString,
                        searchString,
                        searchStatusEquipment,
                        searchEmployee
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
