using ShoppingSiteBusiness;
using ConsoleTables;
namespace ShoppingsiteConsole
{
     public partial class HomeController
    {
        private string NewUserName="";
        private string NewPassword="";
        private string UserName="";
        private string Password="";
        private readonly IAuthentication _authentication;
        private readonly IValidation _validation;
        public HomeController(IAuthentication authentication,IValidation validation){
         _authentication=authentication;
         _validation=validation;

        }
        public void Home(){        


            OPTION:
            Console.WriteLine("Welcome to ElectroShopp");
            Console.WriteLine("Please Enter the option");
            Console.WriteLine("1.Signup");
            Console.WriteLine("2.Login");
            Console.WriteLine("3.Quit");

            int option =Convert.ToInt16(Console.ReadLine());

            if(option==1){
                ReenterData:
                Console.Write("Enter the New Username:");
                NewUserName=Console.ReadLine();
                
                Console.Write("Enter the New Password:");
                NewPassword=Console.ReadLine();

                Console.Write("Reenter the Password:");
                string ReNewPassword=Console.ReadLine();

                if(_authentication.IsUserExist(NewUserName)==true)
                 {
                     Console.WriteLine("The User Aldready Exist");
                     goto ReenterData;
                }

                if(_validation.ValidatePassword(NewPassword,ReNewPassword)){
                    _authentication.SaveToDataBase(NewUserName,NewPassword);
                }
                else{
                    Console.WriteLine("Please include password with UpperCase LowerCase and Special character");
                    goto ReenterData;
                }

            }
            else if(option==2){

                Console.Write("Enter the Username:");
                _authentication.UserName=Console.ReadLine();

                Console.Write("Enter the Password:");
                _authentication.passWord=Console.ReadLine();

                if(_authentication.LoginUser()){
                    Console.WriteLine("Welcome "+_authentication.UserName);

                }
                else{
                    Console.WriteLine("Entered Wrong UserName or Password");
                }

            }
            else if(option==3){

            }
            else{
                Console.WriteLine("Enetr the Valid Option");
                goto OPTION;
            }

        }
   
   
    }
    

}