/*
Author:Yuvaraj J
Date:10/04/2022
Modified by:Yuvaraj
Modified Date:13/04/2022
Reviewd by:Anitha M
Reviewd date:13/04/2022
*/

namespace ShoppingSiteBusiness;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Serilog;
using ConsoleTables;
public class Authentication:IAuthentication // auth class
{   
         private  string Username="";
         private  string Password="";

         public  string UserName{
             set{Username=value;}
             get{return Username;}
             }
         public  string passWord{
             set{Password=value;}
             }
   public Authentication(){
        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
    }

  public bool IsUserExist(string Username){
        string? Password=null;
           MySqlConnection connection = new MySqlConnection();
           connection.ConnectionString = "server=localhost;user=root;database=shopping;port=3306;password=Aspire@123";
           MySqlCommand commannd = new MySqlCommand(); 
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();
                commannd.Connection = connection;

                commannd.CommandText = "SignUp";
                commannd.CommandType = CommandType.StoredProcedure;

                commannd.Parameters.AddWithValue("@NewUsername", Username);
                commannd.Parameters["@NewUsername"].Direction = ParameterDirection.Input;

                commannd.Parameters.AddWithValue("@NewPassword",Password);
                commannd.Parameters["@NewPassword"].Direction = ParameterDirection.Input;

                commannd.Parameters.Add("@Success", MySqlDbType.VarChar);
                commannd.Parameters["@Success"].Direction = ParameterDirection.Output;

                commannd.ExecuteNonQuery();
            
                if(commannd.Parameters["@Success"].Value.ToString()=="Account Created"){
                    Console.WriteLine("Account created successfully");
                }
                else if(commannd.Parameters["@Success"].Value.ToString()=="Existing User"){
                    Console.WriteLine("Username aldready exist");
                    return false;
                }
            }
            catch (MySqlException error)
            {
                Log.Error(error.ToString());
            }
            finally{
            Log.CloseAndFlush();
            connection.Close();
            }
            return false;
        }
          public void SaveToDataBase(string NewUsername,string NewPassword){
           MySqlConnection connection = new MySqlConnection();
           connection.ConnectionString = "server=localhost;user=root;database=shopping;port=3306;password=Aspire@123";
           MySqlCommand commannd = new MySqlCommand(); 

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();
                commannd.Connection = connection;

                commannd.CommandText = "SignUp";
                commannd.CommandType = CommandType.StoredProcedure;

                commannd.Parameters.AddWithValue("@NewUsername", NewUsername);
                commannd.Parameters["@NewUsername"].Direction = ParameterDirection.Input;

                commannd.Parameters.AddWithValue("@NewPassword", NewPassword);
                commannd.Parameters["@NewPassword"].Direction = ParameterDirection.Input;

                commannd.Parameters.Add("@Success", MySqlDbType.VarChar);
                commannd.Parameters["@Success"].Direction = ParameterDirection.Output;

                commannd.ExecuteNonQuery();
            
                if(commannd.Parameters["@Success"].Value.ToString()=="Account Created"){
                    Console.WriteLine("Account created successfully");
                }

            }
             catch (MySqlException error)
            {
                Log.Error(error.ToString());
            }
            finally{

            connection.Close();
            Log.CloseAndFlush();
            }
            
        }

        public bool LoginUser(){
           MySqlConnection connection = new MySqlConnection();
           connection.ConnectionString = "server=localhost;user=root;database=shopping;port=3306;password=Aspire@123";
           MySqlCommand commannd = new MySqlCommand();          
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();
                commannd.Connection = connection;

                commannd.CommandText = "Authentication";
                commannd.CommandType = CommandType.StoredProcedure;

                commannd.Parameters.AddWithValue("@InUsername", Username);
                commannd.Parameters["@InUsername"].Direction = ParameterDirection.Input;

                commannd.Parameters.AddWithValue("@InPassword", Password);
                commannd.Parameters["@InPassword"].Direction = ParameterDirection.Input;

                commannd.Parameters.Add("@Exist", MySqlDbType.VarChar);
                commannd.Parameters["@Exist"].Direction = ParameterDirection.Output;

                commannd.ExecuteNonQuery();
                if(commannd.Parameters["@Exist"].Value.ToString()=="Successfull"){
                    return true;
                }
                if(commannd.Parameters["@Exist"].Value.ToString()=="User Exist")
                {
                    Console.WriteLine("User Exist");
                }
                
            }
            catch (MySqlException error)
            {
                Log.Error(error.ToString());
            }
            finally{
            Log.CloseAndFlush();
            connection.Close();
            }
            return false;

        }
     }

public class Validations:IValidation
{
    public bool ValidatePassword(string passWord,string Repassword){    
        string PasswordTemp = passWord;
        bool IsPassRepassSame=false;
        if (string.IsNullOrWhiteSpace(PasswordTemp))
        {
        throw new Exception("Password should not be empty");
        }
        if(string.Compare(passWord,Repassword)==0){
          IsPassRepassSame=true;
        }     
    
        var hasNumber=new Regex(@"[0-9]+");
        var hasUpperCase=new Regex(@"[A-Z]+");
        var hasLowerCase=new Regex(@"[a-z]+");
        var hasPassLength=new Regex(@".{8,}");
        var hasSymbols=new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]+");
      
        if (IsPassRepassSame && hasNumber.IsMatch(PasswordTemp) && hasUpperCase.IsMatch(PasswordTemp) && hasPassLength.IsMatch(PasswordTemp) && hasLowerCase.IsMatch(PasswordTemp) && hasSymbols.IsMatch(PasswordTemp))
        {
            return true;
        }
        else{
                return false;
        }
    }

    public  bool ValidateMobilNo(string PhoneNo){    

        if (string.IsNullOrWhiteSpace(PhoneNo))
        {
        throw new Exception("PhNo should not be empty");
        }

        var IsPhNoValid=new Regex(@"[6-9]{1}[0-9]{9}");

        if(IsPhNoValid.IsMatch(PhoneNo))
        {
            return true;
        }
        else{
            return false;
        }
        
    }

    public  bool ValidateUserName(string UserName)
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
        throw new Exception("UserName should not be empty");
        }
        var IsUserNameValid=new Regex(@"[a-zA-z]$");

        if(IsUserNameValid.IsMatch(UserName))
        {
        return true;
        }
        else{
            return false;
        }

    }

    public  bool ValidateEmail(string Email)
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
        throw new Exception("UserName should not be empty");
        }
       var IsEmailValid=new Regex(@"^[a-z 0-9]{3,20}@[a-z]{3,10}[.]{1}(com|co.in)$");

       if(IsEmailValid.IsMatch(Email))
       {
           return true;
       }
       else{
           return false;
       }
    }
}

public class Display:IDisplay{
    public List<CartItem> ShowCategoryItem(){
           List<CartItem> Display=new List<CartItem>();
           MySqlConnection connection = new MySqlConnection();
           connection.ConnectionString = "server=localhost;user=root;database=shopping;port=3306;password=Aspire@123";
           MySqlCommand commannd = new MySqlCommand();          
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();
                commannd.Connection = connection;

                commannd.CommandText = "ShowCategoryItemListSP";
                commannd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader myReader;
                myReader =commannd.ExecuteReader();
                myReader.Read();
                
                while(myReader.Read())
                {
                      Display.Add(new CartItem(myReader[0].ToString(),(int)myReader["CategoryId"],myReader[2].ToString(),(int)myReader["ItemId"],myReader["ItemName"].ToString(),myReader["Description"].ToString(),(decimal)myReader["ItemPrice"],(int)myReader["Stock"],(int)myReader["ImgId"]));           
                }
                return Display;
                
            }
            catch (MySqlException error)
            {
                Log.Error(error.ToString());
                Console.WriteLine(error.ToString());
            }
            finally{
            Log.CloseAndFlush();
            connection.Close();
            }
            return Display;

    }
    public void DisplayCategoryItems(){
            var table = new ConsoleTable("Selection","CategoryId", "Categorytype", "CategoryName","ItemId","ItemName","Description","ItemPrice","Stock");
            Display display=new Display();
            List<CartItem> DBlist=display.ShowCategoryItem();
            foreach (var item in DBlist)
            {
                 table.AddRow(item.CategoryId,item.Categortype,item.CategoryName,item.ItemId,item.ItemName,item.Description,item.ItemPrice,item.Stock);

            }
            table.Write();
            Console.WriteLine();
        }
}
        