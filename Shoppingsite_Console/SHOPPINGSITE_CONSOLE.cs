/*
Author:Yuvaraj J
Created Date:7/04/2022
Modified By:Yuvaraj J
Modified Date:13/04/2022
Reviewd by:Anitha Manogaran
Reviewd Date:13/04/2022
Suggessions: 
            1.Nameing Conventions for name space upper to pascalcase
            2.Dont use Gendralized System namespace include specific namespace
            3.Underscore not required
            4.Change the class name meaningfull
            5.same class name but small case for object
            6.Refer whitespace exception
            7.review 
Modified Date:18/04/2022 (All the changes are made based on suggestions)

*/

using Microsoft.Extensions.DependencyInjection;
using ShoppingSiteBusiness;
namespace ShoppingsiteConsole
{
    class ShoppingsiteConsole
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
               .AddScoped<IAuthentication, Authentication>()
               .AddScoped<IValidation,Validations>()
               .AddScoped<IDisplay,Display>()  
               .BuildServiceProvider();

               HomeController homecontroller=new HomeController(new Authentication(),new Validations());
               homecontroller.Home();

               

        }

    }

        
}