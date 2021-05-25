using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IReportService
    {
        public List<List<string>> GetReportEquipments();
        public List<List<string>> GetReportEquipmentsByStatus(string statusName);
        public List<List<string>> GetReportEmployee();
        public List<List<string>> GetReportEmployeeByDepartment(string departmentName);
        public List<List<string>> GetReportEquipmentsByEmployee(string employeeName);
        public List<List<string>> GetReportEquipmentsByEmployees();
    }
}
