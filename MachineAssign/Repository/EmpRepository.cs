using MachineAssign.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MachineAssign.Repository
{
    public class EmpRepository
    {

        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            //string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            string constr = "Server=DESKTOP-KJ90GOC\\SQLEXPRESS;Database=Neosoft_Kunal;Integrated Security=True;";
            con = new SqlConnection(constr);

        }

        public List<EmpModel> GetAllEmployees()
        {
            connection();
            List<EmpModel> EmpList = new List<EmpModel>();


            SqlCommand com = new SqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new EmpModel
                    {
                        Id = Convert.ToInt32(dr["Row_Id"]),
                        LastName = dr["LastName"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                         Country = dr["CountryName"].ToString(),
                         State = dr["StateName"].ToString(),
                         City = dr["CityName"].ToString(),
                        Email = dr["EmailAddress"].ToString(),
                        MobileNumber = dr["MobileNumber"].ToString(),
                        Pan_no = dr["PanNumber"].ToString(),
                         Passport_no = dr["PassportNumber"].ToString(),
                         Gender = dr["Gender"].ToString(),
                         IsActive = Convert.ToBoolean(dr["IsActive"]),
                         profilepicpath = dr["ProfileImage"].ToString(),
                        BirthDate = dr["DateOfBirth"].ToString(),
                        joinDate = dr["DateOfJoinee"].ToString(),

                    }
                    );
            }

            return EmpList;
        }

        public List<Countrymodel> GetCountry()
        {
            connection();
            List<Countrymodel> CountryList = new List<Countrymodel>();
            SqlCommand com = new SqlCommand("GetCountry", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                CountryList.Add(

                    new Countrymodel
                    {
                        Id = Convert.ToInt32(dr["Row_Id"]),
                        Countryname = dr["CountryName"].ToString()
                    }
                    );
            }

            return CountryList;
        }

        public List<StateModel> GetState(int countryid)
        {
            connection();
            List<StateModel> StateList = new List<StateModel>();
            SqlCommand com = new SqlCommand("GetState", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@countryId", countryid);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                StateList.Add(

                    new StateModel
                    {
                        Id = Convert.ToInt32(dr["Row_Id"]),
                        CountryId = Convert.ToInt32(dr["CountryId"]),
                        Statename = dr["Statename"].ToString()
                    }
                    );
            }

            return StateList;
        }

        public List<Citymodel> GetCity(int stateid)
        {
            connection();
            List<Citymodel> CityList = new List<Citymodel>();
            SqlCommand com = new SqlCommand("GetCity", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@stateId", stateid);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                CityList.Add(

                    new Citymodel
                    {
                        Id = Convert.ToInt32(dr["Row_Id"]),
                        StateId = Convert.ToInt32(dr["StateId"]),
                        CityName = dr["CityName"].ToString()
                    }
                    );
            }

            return CityList;
        }


        public void DeleteEmp(int id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteEmployee", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            

        }
        public void InsertEmp(EmpModel empModel, string profilepath)
        {
            try
            {

                connection();
                SqlCommand com = new SqlCommand("InsertEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FirstName", empModel.FirstName);
                com.Parameters.AddWithValue("@LastName", empModel.LastName);
                com.Parameters.AddWithValue("@CountryId", empModel.Country);
                com.Parameters.AddWithValue("@StateId", empModel.State);
                com.Parameters.AddWithValue("@CityId", empModel.City);
                com.Parameters.AddWithValue("@EmailAddress", empModel.Email);
                com.Parameters.AddWithValue("@MobileNumber", empModel.MobileNumber);
                com.Parameters.AddWithValue("@PanNumber", empModel.Pan_no);
                com.Parameters.AddWithValue("@PassportNumber", empModel.Passport_no);
                com.Parameters.AddWithValue("@ProfileImage", profilepath.ToString());
                com.Parameters.AddWithValue("@Gender", empModel.Gender);
                com.Parameters.AddWithValue("@IsActive", empModel.IsActive);
                com.Parameters.AddWithValue("@DateOfBirth", empModel.BirthDate);
                com.Parameters.AddWithValue("@DateOfJoinee", empModel.joinDate);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();


            }
            catch(Exception ex)
            {

            }




        }

        public void UpdateEmp(EmpModel empModel, string profilepath)
        {
            try
            {

                connection();
                SqlCommand com = new SqlCommand("UpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", empModel.Id);
                com.Parameters.AddWithValue("@FirstName", empModel.FirstName);
                com.Parameters.AddWithValue("@LastName", empModel.LastName);
                com.Parameters.AddWithValue("@CountryId", empModel.Country);
                com.Parameters.AddWithValue("@StateId", empModel.State);
                com.Parameters.AddWithValue("@CityId", empModel.City);
                com.Parameters.AddWithValue("@EmailAddress", empModel.Email);
                com.Parameters.AddWithValue("@MobileNumber", empModel.MobileNumber);
                com.Parameters.AddWithValue("@PanNumber", empModel.Pan_no);
                com.Parameters.AddWithValue("@PassportNumber", empModel.Passport_no);
                com.Parameters.AddWithValue("@ProfileImage", profilepath.ToString());
                com.Parameters.AddWithValue("@Gender", empModel.Gender);
                com.Parameters.AddWithValue("@IsActive", empModel.IsActive);
                com.Parameters.AddWithValue("@DateOfBirth", empModel.BirthDate);
                com.Parameters.AddWithValue("@DateOfJoinee", empModel.joinDate);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();


            }
            catch (Exception ex)
            {

            }




        }

    }
}
