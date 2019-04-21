namespace Company
{
    class DepartmentDTO : MyPropertyChanged
    {
        string departName;
        //Id департамента
        public int Id { get; }
        //Наименование департамента
        public string DepartName {
            get
            {
                return departName;
            }
            set
            {
                departName = value;
                OnPropertyChanged("DepartName");
            }
        }
    }
}