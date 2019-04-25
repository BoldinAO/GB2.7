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
            list = new ObservableCollection<WorkerDTO>();
            workers = new CollectionView(list);
            Sql.GetData(this);
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
        public void AddWorker(int id, string name, string secondName, string surName, string depart)
        {
            list.Add(new WorkerDTO() { Id = id, Name = name, SecondName = secondName, SurName = surName, Department = depart });
        }

        public void SaveWorker(int id, string name, string secondName, string surName, string depart)
        {
            Sql.WriteData(new WorkerDTO() { Id = id, Name = name, SecondName = secondName, SurName = surName, Department = depart });
            AddWorker(id, name, secondName, surName, depart);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public void DelEmployee(object employee)
        {
            Sql.DelData(employee);
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
            worker.Department = department.DepartName;
            Sql.ChangeData(worker);
        }
    }
}
