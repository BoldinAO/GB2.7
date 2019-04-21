using Company.DTO;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System;

namespace Company
{
    class EmployeeViewModel
    {
        private readonly CollectionView workers;
        ObservableCollection<WorkerDTO> list;

        public EmployeeViewModel()
        {
            list = new ObservableCollection<WorkerDTO>
            {
                new WorkerDTO() { Name = "Имя1", SecondName = "Отчество1", SurName = "Фамилия1" },
                new WorkerDTO() { Name = "Имя2", SecondName = "Отчество2", SurName = "Фамилия2" }
            };
            workers = new CollectionView(list);
        }

        public CollectionView Workers
        {
            get { return workers; }
        }

        public ObservableCollection<WorkerDTO> GetEmployeeList
        {
            get { return list; }
        }

        public object Current => throw new NotImplementedException();

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="secondName">Отчество</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="depart">Департамент</param>
        public void AddWorker(string name, string secondName, string surName, object depart)
        {
            DepartmentDTO department = (DepartmentDTO)depart;
            if(department != null)
                list.Add(new WorkerDTO() { Name = name, SecondName = secondName, SurName = surName, Department = department.DepartName });
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public void DelEmployee(object employee)
        {
            list.Remove((WorkerDTO)employee);
            employee = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Изменение данных пользователя
        /// </summary>
        /// <param name="departmentName">Наименование департамента</param>
        public void ChangeEmployeeData(object workr, string name = null, string secondName = null, string surName = null, object depart = null)
        {
            WorkerDTO worker = (WorkerDTO)workr;
            worker.Name = name;
            worker.SecondName = secondName;
            worker.SurName = surName;
            DepartmentDTO department = (DepartmentDTO)depart;
            if (department != null)
                worker.Department = department.DepartName;
        }
    }
}
