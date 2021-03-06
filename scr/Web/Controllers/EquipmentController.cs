using AutoMapper;
using ClosedXML.Excel;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
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
        public readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentController(IEquipmentService equipmentService,
            IStatusEquipmentService statusEquipmentService,
            IEmployeeService employeeService,
            IEquipmentTypeService equipmentTypeService)
        {
            _equipmentService = equipmentService;
            _statusEquipmentService = statusEquipmentService;
            _employeeService = employeeService;
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString,
            string searchStatusEquipment,
            string searchEmployee,
            string searchEquipmentType)
        {
           var equipmentIndexViewModel = GetEquipmentIndexViewModel(searchSelectionString,  searchString, searchStatusEquipment,
             searchEmployee,
             searchEquipmentType);

            return View(equipmentIndexViewModel);
        }

        private EquipmentIndexViewModel GetEquipmentIndexViewModel(string searchSelectionString, string searchString,
            string searchStatusEquipment,
            string searchEmployee,
            string searchEquipmentType)
        {
            var equipmentDTOs = _equipmentService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentDTO, EquipmentViewModel>()).CreateMapper();
            var equipmentViewModels = mapper.Map<IEnumerable<EquipmentDTO>, List<EquipmentViewModel>>(equipmentDTOs);

            // equipmentTypes
            List<string> equipmentTypes = _equipmentTypeService.GetAllName().ToList();
            equipmentTypes.Insert(0, "Вид оборудования");

            if (searchEquipmentType != string.Empty && searchEquipmentType != "Вид оборудования" && searchEquipmentType != null)
            {
                equipmentViewModels = equipmentViewModels.Where(p => p.EquipmentTypeName == searchEquipmentType).ToList();
            }

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            statusEquipments.Insert(0, "Состояние оборудования");

            if (searchStatusEquipment != string.Empty && searchStatusEquipment != "Состояние оборудования" && searchStatusEquipment != null)
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

            return new EquipmentIndexViewModel()
            {
                EquipmentViewModels = equipmentViewModels,
                SearchSelection = new SelectList(searchSelection),
                SearchSelectionString = searchSelectionString,
                SearchString = searchString,
                SearchStatusEquipment = searchStatusEquipment,
                StatusEquipmentSelect = new SelectList(statusEquipments),
                SearchEmployee = searchEmployee,
                EmployeeSelect = new SelectList(employees),
                SearchEquipmentType = searchEquipmentType,
                EquipmentTypeSelect = new SelectList(equipmentTypes)
            };
        }

        [HttpGet]
        public IActionResult Add(string searchSelectionString, string searchString, string searchStatusEquipment,
            string searchEmployee, string searchEquipmentType)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;
            ViewBag.SearchEquipmentType = searchEquipmentType;

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            // employees
            List<string> employees = _employeeService.GetAllIdName().ToList();
            // equipmentTypes
            List<string> equipmentTypes = _equipmentTypeService.GetAllName().ToList();

            return View(new EquipmentAddEditViewModel()
            {
                EquipmentViewModel = new EquipmentViewModel() { },
                StatusEquipmentsSelect = new SelectList(statusEquipments),
                EmployeeSelect = new SelectList(employees),
                EquipmentTypeSelect = new SelectList(equipmentTypes)
            });
        }

        [HttpPost]
        public IActionResult Add(EquipmentAddEditViewModel model, string searchSelectionString, string searchString, string searchStatusEquipment,
            string searchEmployee, string searchEquipmentType)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;
            ViewBag.SearchEquipmentType = searchEquipmentType;

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
                    Term = model.EquipmentViewModel.Term,
                    EquipmentTypeName = model.EquipmentViewModel.EquipmentTypeName
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
            // equipmentTypes
            List<string> equipmentTypes = _equipmentTypeService.GetAllName().ToList();

            model.StatusEquipmentsSelect = new SelectList(statusEquipments);
            model.EmployeeSelect = new SelectList(employees);
            model.EquipmentTypeSelect = new SelectList(equipmentTypes);

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, string searchSelectionString, string searchString, string searchStatusEquipment,
            string searchEmployee, string searchEquipmentType)
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
                searchEmployee,
                searchEquipmentType
            });
        }

        [HttpGet]
        public IActionResult Edit(int id, string searchSelectionString, string searchString, string searchStatusEquipment, string searchEmployee, string searchEquipmentType)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;
            ViewBag.SearchEquipmentType = searchEquipmentType;

            var equipmentDTO = _equipmentService.Get(id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EquipmentDTO, EquipmentViewModel>()).CreateMapper();
            var equipmentViewModel = mapper.Map<EquipmentDTO, EquipmentViewModel>(equipmentDTO);

            equipmentViewModel.EmployeeFullName = equipmentDTO.EmployeeId + "|" + equipmentDTO.EmployeeFullName;

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            // employees
            List<string> employees = _employeeService.GetAllIdName().ToList();
            // equipmentTypes
            List<string> equipmentTypes = _equipmentTypeService.GetAllName().ToList();

            return View(new EquipmentAddEditViewModel()
            {
                EquipmentViewModel = equipmentViewModel,
                StatusEquipmentsSelect = new SelectList(statusEquipments),
                EmployeeSelect = new SelectList(employees),
                EquipmentTypeSelect = new SelectList(equipmentTypes)
            });
        }

        [HttpPost]
        public IActionResult Edit(EquipmentAddEditViewModel model, string searchSelectionString, string searchString, string searchStatusEquipment,
            string searchEmployee, string searchEquipmentType)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;
            ViewBag.SearchStatusEquipment = searchStatusEquipment;
            ViewBag.SearchEmployee = searchEmployee;
            ViewBag.SearchEquipmentType = searchEquipmentType;

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
                        searchEmployee,
                        searchEquipmentType
                    });
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult GetExcel(string searchSelectionString, string searchString,
            string searchStatusEquipment,
            string searchEmployee,
            string searchEquipmentType)
        {
            var models = GetEquipmentIndexViewModel(searchSelectionString, searchString, searchStatusEquipment,
                searchEmployee, searchEquipmentType).EquipmentViewModels;

            using (var workBook = new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Оборудование");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Код";
                worksheet.Cell(currentRow, 2).Value = "Инвентарный номер";
                worksheet.Cell(currentRow, 3).Value = "Название";
                worksheet.Cell(currentRow, 4).Value = "Вид оборудования";
                worksheet.Cell(currentRow, 5).Value = "Состояние оборудования";
                worksheet.Cell(currentRow, 6).Value = "Первоначальная стоимость";
                worksheet.Cell(currentRow, 7).Value = "Срок полезного использования";
                worksheet.Cell(currentRow, 8).Value = "Годовая ставка";
                worksheet.Cell(currentRow, 9).Value = "Сумма отчислений в месяц";
                worksheet.Cell(currentRow, 10).Value = "Отдел";
                worksheet.Cell(currentRow, 11).Value = "Код сотрудника";
                worksheet.Cell(currentRow, 12).Value = "Сотрудник";

                foreach (var model in models)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = model.Id;
                    worksheet.Cell(currentRow, 2).Value = model.InventoryNumber;
                    worksheet.Cell(currentRow, 3).Value = model.Name;
                    worksheet.Cell(currentRow, 4).Value = model.EquipmentTypeName;
                    worksheet.Cell(currentRow, 5).Value = model.StatusEquipmentName;
                    worksheet.Cell(currentRow, 6).Value = model.Price;
                    worksheet.Cell(currentRow, 7).Value = model.Term;
                    worksheet.Cell(currentRow, 8).Value = model.ProcentYear;
                    worksheet.Cell(currentRow, 9).Value = model.DeductionAmountPerMonth;
                    worksheet.Cell(currentRow, 10).Value = model.Department;
                    worksheet.Cell(currentRow, 11).Value = model.EmployeeId;
                    worksheet.Cell(currentRow, 12).Value = model.EmployeeFullName;
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(
                       content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Equipments.xlsx");
                }
            }
        }
    }
}
