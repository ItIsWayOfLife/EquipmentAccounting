using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Report;
using Web.Reports;

namespace Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStatusEquipmentService _statusEquipmentService;
        private readonly IDepartmentService _departmentService;

        public ReportController(IReportService reportService,
               IWebHostEnvironment webHostEnvironment,
               IStatusEquipmentService statusEquipmentService,
               IDepartmentService departmentService)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
            _statusEquipmentService = statusEquipmentService;
            _departmentService = departmentService;
        }

        public IActionResult Index(string exMessage)
        {
            if (exMessage != null)
            {
                ViewBag.ExMessage = exMessage;
            }
            else
            {
                ViewBag.ExMessage = null;
            }

            // statusEquipments
            List<string> statusEquipments = _statusEquipmentService.GetAllName().ToList();
            statusEquipments.Insert(0, "Состояние оборудования");

            // departments
            List<string> departments = _departmentService.GetAllName().ToList();
            departments.Insert(0, "Отдел");

            //if (searchDepartment != string.Empty && searchDepartment != "Отдел" && searchDepartment != null)
            //{
            //    employeesViewModels = employeesViewModels.Where(p => p.DepartmentName == searchDepartment).ToList();
            //}

            return View(new ReportIndexViewModel()
            { 
                StatusSelect = new SelectList(statusEquipments),
                DepartmentSelect = new SelectList(departments)
            });
        }


        [HttpPost]
        public IActionResult GetReportEquipments(string searchStatusEquipment)
        {
            string exMessage = null;

            try
            {

                if (searchStatusEquipment == "Состояние оборудования")
                {
                    List<List<string>> reportEquipments = _reportService.GetReportEquipments();
                    string title = "Отчёт оборудования";

                    ReportPDF report = new ReportPDF(_webHostEnvironment);
                    return File(report.Report(reportEquipments, title), "application/pdf");
                }
                else
                {
                    List<List<string>> reportEquipments = _reportService.GetReportEquipmentsByStatus(searchStatusEquipment);
                    string title = $"Отчёт оборудования по состоянию оборудования: {searchStatusEquipment}";

                    ReportPDF report = new ReportPDF(_webHostEnvironment);
                    return File(report.Report(reportEquipments, title), "application/pdf");
                }
            }
            catch (ValidationException ex)
            {
                exMessage = ex.Message;
            }
            return RedirectToAction("Index", new
            {
                exMessage
            });
        }

        [HttpPost]
        public IActionResult GetReportDepartments(string searchDepartment)
        {
            string exMessage = null;

            try
            {

                if (searchDepartment == "Отдел")
                {
                    List<List<string>> reportEmployees = _reportService.GetReportEmployee();
                    string title = "Отчёт сотрудников";

                    ReportPDF report = new ReportPDF(_webHostEnvironment);
                    return File(report.Report(reportEmployees, title), "application/pdf");
                }
                else
                {
                    List<List<string>> reportEquipments = _reportService.GetReportEmployeeByDepartment(searchDepartment);
                    string title = $"Отчёт сорудников по отделу: {searchDepartment}";

                    ReportPDF report = new ReportPDF(_webHostEnvironment);
                    return File(report.Report(reportEquipments, title), "application/pdf");
                }
            }
            catch (ValidationException ex)
            {
                exMessage = ex.Message;
            }
            return RedirectToAction("Index", new
            {
                exMessage
            });
        }

        [HttpPost]
        public IActionResult GetReportEquipmentsByEmployees(string searchEmployee)
        {
            string exMessage = null;

            try
            {
                    List<List<string>> reportEmployees = _reportService.GetReportEquipmentsByEmployees();
                    string title = "Отчёт оборудования по работникам";

                    ReportPDF report = new ReportPDF(_webHostEnvironment);
                    return File(report.Report(reportEmployees, title), "application/pdf");
            }
            catch (ValidationException ex)
            {
                exMessage = ex.Message;
            }
            return RedirectToAction("Index", new
            {
                exMessage
            });
        }
    }
}
