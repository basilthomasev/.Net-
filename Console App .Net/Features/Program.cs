using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Models;
using Features.OptionAndOprations;
using Features.Repository;

namespace Features 
{
   public class Program 
   {
        static void Main(string[] args)
        {
            bool boolValue = true;
            Options objOption = new Options();

            Operations objOperation = new Operations();

            while (boolValue)
            {
                //code to clear the window
                Console.Clear();

                //code to retrive the user options
                int optionValue = objOption.Option();

                //code to do the operation
                objOperation.Operation(optionValue);

                /*----------------------------------------------------------------------------------------------*/
                int continueValue = 0;
                Console.WriteLine(Message.continu);
                bool check = true;
                while (check)
                {
                    try
                    {
                        continueValue = Convert.ToInt32(Console.ReadLine());
                        if (continueValue != 0 || continueValue != 1)
                        {
                            check = false;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid Input, Please try a valid digit");
                    }
                }
                if (continueValue == 0)  
                {
                    boolValue = false;
                    System.Environment.Exit(0);
                }
            }
        }
    }
}
