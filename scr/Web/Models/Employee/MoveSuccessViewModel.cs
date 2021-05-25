namespace Web.Models.Employee
{
    public class MoveSuccessViewModel
    {
       public string Name { get; set; }
        public string InventoryNumber { get; set; }
        public string OldDepartment { get; set; }
        public string OldEmployeeFullName { get; set; }
        public string NewDepartment { get; set; }
        public string NewEmployeeFullName { get; set; }
    }
}
