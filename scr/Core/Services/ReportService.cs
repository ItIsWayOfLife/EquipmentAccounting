using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Services.Common;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class ReportService : Service, IReportService
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEmployeeService _employeeService;
        public ReportService(IUnitOfWork uow,
            IEquipmentService equipmentService,
            IEmployeeService employeeService) : base(uow)
        {
            _equipmentService = equipmentService;
            _employeeService = employeeService;
        }

        public List<List<string>> GetReportEmployee()
        {
            var employeeDTOs = _employeeService.GetAll();

            List<List<string>> reportList = new List<List<string>>();

            List<string> header = new List<string>() { "Код", "ФИО", "Пол", "Дата рождения", "Телефон", "Отдел", "Должность" };

            reportList.Add(header);

            foreach (var employeeDTO in employeeDTOs)
            {
                List<string> columReport = new List<string>();

                columReport.Add(employeeDTO.Id.ToString());
                columReport.Add(employeeDTO.FullName.ToString());
                columReport.Add(employeeDTO.Sex.ToString());
                columReport.Add(employeeDTO.DateOfBirth.ToString());
                columReport.Add(employeeDTO.Phone.ToString());
                columReport.Add(employeeDTO.DepartmentName.ToString());
                columReport.Add(employeeDTO.PositionName.ToString());

                reportList.Add(columReport);
            }

            return reportList;
        }

        public List<List<string>> GetReportEmployeeByDepartment(string departmentName)
        {
            var employeeDTOs = _employeeService.GetAll().Where(p => p.DepartmentName == departmentName);

            List<List<string>> reportList = new List<List<string>>();

            List<string> header = new List<string>() { "Код", "ФИО", "Пол", "Дата рождения", "Телефон", "Отдел", "Должность" };

            reportList.Add(header);

            foreach (var employeeDTO in employeeDTOs)
            {
                List<string> columReport = new List<string>();

                columReport.Add(employeeDTO.Id.ToString());
                columReport.Add(employeeDTO.FullName.ToString());
                columReport.Add(employeeDTO.Sex.ToString());
                columReport.Add(employeeDTO.DateOfBirth.ToString());
                columReport.Add(employeeDTO.Phone.ToString());
                columReport.Add(employeeDTO.DepartmentName.ToString());
                columReport.Add(employeeDTO.PositionName.ToString());

                reportList.Add(columReport);
            }

            return reportList;
        }

        public List<List<string>> GetReportEquipments()
        {
            var equipments = _equipmentService.GetAll();

            List<List<string>> reportList = new List<List<string>>();

            List<string> header = new List<string>() { "Код", "Инвентарный номер", "Название", "Вид оборудования", "Состояние оборудования", "Первоначальная стоимость", "Срок полезного использования", "Годовая ставка", "Сумма отчислений в месяц", "Отдел", "Код сотрудника", "Сотрудник" };

            reportList.Add(header);

            double sum = 0;

            foreach (var equipment in equipments)
            {
                List<string> columReport = new List<string>();

                columReport.Add(equipment.Id.ToString());
                columReport.Add(equipment.InventoryNumber.ToString());
                columReport.Add(equipment.Name.ToString());
                columReport.Add(equipment.EquipmentTypeName.ToString());
                columReport.Add(equipment.StatusEquipmentName.ToString());
                columReport.Add(equipment.Price.ToString());
                columReport.Add(equipment.Term.ToString());
                columReport.Add(equipment.ProcentYear.ToString());
                columReport.Add(equipment.DeductionAmountPerMonth.ToString());
                columReport.Add(equipment.Department.ToString());
                columReport.Add(equipment.EmployeeId.ToString());
                columReport.Add(equipment.EmployeeFullName.ToString());

                sum += equipment.Price;

                reportList.Add(columReport);
            }

            List<string> totalSumList = new List<string>() { "Общая сумма (цена) за всё оборудование: ", $"{sum}", "", "", "", "", "", "", "", "", "", "" };
            reportList.Add(totalSumList);

            return reportList;
        }

        public List<List<string>> GetReportEquipmentsByStatus(string statusName)
        {
            var equipments = _equipmentService.GetAll().Where(p => p.StatusEquipmentName == statusName);

            List<List<string>> reportList = new List<List<string>>();

            List<string> header = new List<string>() { "Код", "Инвентарный номер", "Название", "Вид оборудования", "Состояние оборудования", "Первоначальная стоимость", "Срок полезного использования", "Годовая ставка", "Сумма отчислений в месяц", "Отдел", "Код сотрудника", "Сотрудник" };

            reportList.Add(header);

            double sum = 0;

            foreach (var equipment in equipments)
            {
                List<string> columReport = new List<string>();

                columReport.Add(equipment.Id.ToString());
                columReport.Add(equipment.InventoryNumber.ToString());
                columReport.Add(equipment.Name.ToString());
                columReport.Add(equipment.EquipmentTypeName.ToString());
                columReport.Add(equipment.StatusEquipmentName.ToString());
                columReport.Add(equipment.Price.ToString());
                columReport.Add(equipment.Term.ToString());
                columReport.Add(equipment.ProcentYear.ToString());
                columReport.Add(equipment.DeductionAmountPerMonth.ToString());
                columReport.Add(equipment.Department.ToString());
                columReport.Add(equipment.EmployeeId.ToString());
                columReport.Add(equipment.EmployeeFullName.ToString());

                sum += equipment.Price;

                reportList.Add(columReport);
            }

            List<string> totalSumList = new List<string>() { "Общая сумма (цена) за всё оборудование: ", $"{sum}", "", "", "", "", "", "", "", "", "", "" };
            reportList.Add(totalSumList);

            return reportList;
        }

        public List<List<string>> GetReportEquipmentsByEmployee(string employeeIdFullName)
        {
            throw new System.NotImplementedException();
        }

        public List<List<string>> GetReportEquipmentsByEmployees()
        {
            var equipments = _equipmentService.GetAll();
            var employees = _employeeService.GetAll();

            List<List<string>> reportList = new List<List<string>>();

            List<string> header = new List<string>() { "Код", "Инвентарный номер", "Название", "Вид оборудования", "Состояние оборудования", "Первоначальная стоимость", "Срок полезного использования", "Годовая ставка", "Сумма отчислений в месяц", "Отдел", "Код сотрудника", "Сотрудник" };

            reportList.Add(header);

            double sum = 0;

            foreach (var employee in employees)
            {
                List<string> emlList = new List<string>() { $"Сотрудник: {employee.Id}|{employee.FullName}", "", "", "", "", "", "", "", "", "", "", "" };
                reportList.Add(emlList);

                foreach (var equipment in equipments.Where(p => p.EmployeeId == employee.Id))
                {

                    List<string> columReport = new List<string>();

                    columReport.Add(equipment.Id.ToString());
                    columReport.Add(equipment.InventoryNumber.ToString());
                    columReport.Add(equipment.Name.ToString());
                    columReport.Add(equipment.EquipmentTypeName.ToString());
                    columReport.Add(equipment.StatusEquipmentName.ToString());
                    columReport.Add(equipment.Price.ToString());
                    columReport.Add(equipment.Term.ToString());
                    columReport.Add(equipment.ProcentYear.ToString());
                    columReport.Add(equipment.DeductionAmountPerMonth.ToString());
                    columReport.Add(equipment.Department.ToString());
                    columReport.Add(equipment.EmployeeId.ToString());
                    columReport.Add(equipment.EmployeeFullName.ToString());

                    reportList.Add(columReport);

                    sum += equipment.Price;
                }
                List<string> totalSumList = new List<string>() { $"Цена за всё оборудование сотрудника: {employee.Id}|{employee.FullName}", $"{sum}", "", "", "", "", "", "", "", "", "", "" };
                reportList.Add(totalSumList);
                sum = 0;              
            }

            return reportList;
        }

    }
}

