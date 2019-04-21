using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Company
{
    class DepartmentViewModel : MyPropertyChanged
    {
        private readonly CollectionView departments;
        private string department;
        ObservableCollection<DepartmentDTO> list;

        public DepartmentViewModel()
        {
            list = new ObservableCollection<DepartmentDTO>();
            list.Add(new DepartmentDTO() { DepartName = "first" });
            list.Add(new DepartmentDTO() { DepartName = "second" });
            departments = new CollectionView(list);
        }

        public string Department
        {
            get { return department; }
            set
            {
                if (department == value) return;
                department = value;
            }
        }

        public CollectionView Departments
        {
            get { return departments; }
        }

        /// <summary>
        /// Добавление департамента в список
        /// </summary>
        /// <param name="name">Наименование департамента</param>
        public void AddDepart(string name)
        {
            list.Add(new DepartmentDTO() { DepartName = name });
        }

        /// <summary>
        /// Удаление департамента из списка
        /// </summary>
        /// <param name="department">Департамент</param>
        public void DelDepart(object department, EmployeeViewModel employee)
        {
            var depart = (DepartmentDTO)department;
            for(var i = 0; i < employee.GetEmployeeList.Count; i++)
            {
                if (employee.GetEmployeeList[i].Department == depart.DepartName)
                    employee.DelEmployee(employee.GetEmployeeList[i]);
            }
            list.Remove(depart);
            department = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Изменение наименования департамента
        /// </summary>
        /// <param name="departmentName">Наименование департамента</param>
        public void ChangeDepartmentName(object depart, string departmentName, EmployeeViewModel employee)
        {
            foreach (var s in employee.GetEmployeeList)
            {
                var department = (DepartmentDTO)depart;
                if(s.Department == department.DepartName)
                    s.Department = departmentName;
            }

            foreach (var department in list)
            {
                if (department == depart)
                    department.DepartName = departmentName;
            }
        }
    }
}
