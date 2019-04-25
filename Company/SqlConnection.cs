using System.Data.SqlClient;
using Company.DTO;

namespace Company
{
    static class Sql
    {
        static string _connString;
        static SqlConnection _sqlCOnn;
        static string _sql;
        static SqlDataReader _reader;

        static Sql()
        {
            _connString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                        Initial Catalog=D:\DB\COMPANY\COMPANYDB.MDF;
                        Integrated Security=True;
                        Connect Timeout=30;
                        Encrypt=False;
                        TrustServerCertificate=False;
                        ApplicationIntent=ReadWrite;
                        MultiSubnetFailover=False";

            _sqlCOnn = new SqlConnection(_connString);
        }

        public static void WriteData(object DTO)
        {
            if (DTO is WorkerDTO)
                _sql = $@"INSERT INTO Worker (Id, Name, SurName, SecondName, DepartName) VALUES ({(DTO as WorkerDTO).Id}, '{(DTO as WorkerDTO).Name}', " +
                            $"'{(DTO as WorkerDTO).SurName}', '{(DTO as WorkerDTO).SecondName}', '{(DTO as WorkerDTO).Department}')";
            if (DTO is DepartmentDTO)
                _sql = $@"INSERT INTO Department (Id, DepartName) VALUES ({(DTO as DepartmentDTO).Id}, '{(DTO as DepartmentDTO).DepartName}')";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void GetData(object ViewModel)
        {
            if (ViewModel is DepartmentViewModel)
            {
                _sql = $@"SELECT * FROM Department";
            }

            if (ViewModel is EmployeeViewModel)
            {
                _sql = $@"SELECT * FROM Worker";
            }

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                _reader = command.ExecuteReader();
                if(ViewModel is DepartmentViewModel)
                    while (_reader.Read())
                        (ViewModel as DepartmentViewModel).AddDepart((int)_reader["Id"], _reader["DepartName"].ToString());

                if (ViewModel is EmployeeViewModel)
                    while (_reader.Read())
                        (ViewModel as EmployeeViewModel).AddWorker((int)_reader["Id"], _reader["Name"].ToString(), _reader["SecondName"].ToString(), _reader["SurName"].ToString(), _reader["DepartName"].ToString());
            }
        }

        public static void DelData(object DTO)
        {
            if (DTO is WorkerDTO)
                _sql = $@"DELETE Worker WHERE Id = {(DTO as WorkerDTO).Id}";
            if (DTO is DepartmentDTO)
                _sql = $@"DELETE Department WHERE Id = {(DTO as DepartmentDTO).Id}";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void ChangeData(object DTO)
        {
            if (DTO is WorkerDTO)
                _sql = $@"UPDATE Worker SET Name = '{(DTO as WorkerDTO).Name}', " +
                            $"SurName = '{(DTO as WorkerDTO).SurName}', SecondName = '{(DTO as WorkerDTO).SecondName}', " +
                            $"DepartName = '{(DTO as WorkerDTO).Department}' WHERE Id = {(DTO as WorkerDTO).Id}";
            if (DTO is DepartmentDTO)
                _sql = $@"UPDATE Department SET DepartName = '{(DTO as DepartmentDTO).DepartName}' WHERE Id = {(DTO as DepartmentDTO).Id}";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                command.ExecuteNonQuery();
            }
        }
    }
}
