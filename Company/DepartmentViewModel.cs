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
            departments = new CollectionView(list);
            Sql.GetData(this);
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
        public void AddDepart(int id, string name)
        {
            list.Add(new DepartmentDTO() { Id = id, DepartName = name });
        }

        public void SaveDepart(int id, string name)
        {
            Sql.WriteData(new DepartmentDTO() { Id = id, DepartName = name });
            AddDepart(id, name);
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
            Sql.DelData(depart);
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
            var department = (DepartmentDTO)depart;
            foreach (var s in employee.GetEmployeeList)
            {
                if (s.Department == department.DepartName)
                {
                    s.Department = departmentName;
                    Sql.ChangeData(s);
                }
            }

            department.DepartName = departmentName;
            Sql.ChangeData(department);
        }
    }
}
