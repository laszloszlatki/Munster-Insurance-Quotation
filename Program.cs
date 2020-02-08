using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/* Author:  Laszlo Szlatki
 * Date:    17/04/2018
 * Version: 1.0
 * 
 * Create new Vehicle and calculate car part of insurance (no loop when invalid options selected for emission)
 * Create new customer, gender charge is done (needs a loop when invalid option entered) 
 * *************** need to add rest of the charges for customer (added in Version 1.1)
 * *************** need the main method (added in Version 1.1)
 * *************** need to save info to a txt file!!!! need research for it!!!!!!!
 * 
 * Date:    18/04/2018
 * Version: 1.1
 * Emission loop added, vehicle details output method added
 * added customer name field, age charge, county charge with loops when invalid data entered
 * added in main method: customer and vehicle created, details and total fee displayed
 * ************** need to add a loop to ask worker if another quote is required or close the program (added in Version 1.2)
 * ************** need to terminate execurion if age is >=80 (resolved in Version 1.2)
 * ************** need to save details in a txt file!!!!  (added in Version 1.2)
 * 
 * Date:    19/04/2018
 * Version: 1.2
 * loop added to ask worker if another quote is required or close the program
 * terminate if age is over 80 resolved
 * entered details are saved in customerQuotes.txt file
 * user authentication implemented
 *
 */

namespace Munster_Insurance_Quotation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Munster Motor Insurance Group";
            // user verification
            Employee myEmployee = new Employee();
            myEmployee.login();

            // loop to prompt worker to create new quote or close program
            string newQuote = "Y";
            while (newQuote == "Y")
            {
                // creating a customer object
                Customer myCustomer = new Customer();
                // creating a vehicle object
                Vehicle myVehicle = new Vehicle();
                // prompting worker to enter customer details
                myCustomer.NewCustomer();
                // prompting worker to enter car details if passed on customer age
                if (myCustomer.Age < 80)
                {
                    myVehicle.NewVehicle();

                    Console.Clear();
                    // retutn details of customer and car for retference
                    Console.WriteLine(myCustomer.returnCustomerDetails());
                    Console.WriteLine(myVehicle.returnCarDetails());

                    // return total fee payable
                    double TotalFeePayable = myVehicle.CalculateVehiclePart() + myCustomer.CalculateCustomerPart();
                    Console.WriteLine($"The total fee payable is: {TotalFeePayable}");
                }

                // new StreamWriter object created and used to store quote details
                StreamWriter sw = new StreamWriter("CustomerQuotes.txt", true);
                sw.WriteLine(myCustomer.CustomerDetailsForFile() + myVehicle.CarDetailsForFile() + (myVehicle.CalculateVehiclePart()+myCustomer.CalculateCustomerPart()));
                sw.Close();
                // close application Y/N
                Console.WriteLine("\n\nDo you wish to create a new quotation?\nPress 'Y' if yes, or else, close the application");
                newQuote = Console.ReadLine().ToUpper();
                if (newQuote == "Y")
                {
                    Console.Clear();
                    //return "";
                }
                else
                {
                    Console.WriteLine("Have a nice day!");
                    System.Threading.Thread.Sleep(1000);
                    break;
                    //return "Have a nice day!";
                }
            }
        }
    }

    // Vehicle class
    public class Vehicle
    {
        // properties
        public string Make { get; set; }
        public string Model { get; set; }
        public string Emission { get; set; }
        public string CategoryOfInsurance { get; set; }

        // methods
        public void NewVehicle()
        {
            Console.Write("Enter Car Make: ");
            Make = Console.ReadLine().ToUpper().Trim();
            Console.Write("Enter Car Model: ");
            Model = Console.ReadLine().ToLower().Trim();
            // while loop to prompt user to re-enter incorrect emission option
            while (true)
            {
                Console.Write("Enter emission category:\nH - high emission\nM - medium emission\nL - low emission: ");
                Emission = (Console.ReadLine().ToUpper());
                if (Emission == "H" || Emission == "M" || Emission == "L")
                    break;
                else
                {
                    Console.WriteLine("Invalid option for emission\n\nPlease enter 'H' or 'M' or 'L'");
                }
            }

            while (true)
            {
                Console.WriteLine("Please enter Category of insurance\n'F' - fully comprehenceive\n'T' - Third party fire and theft");
                CategoryOfInsurance = Console.ReadLine().ToUpper();
                if (CategoryOfInsurance == "F" || CategoryOfInsurance == "T")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option for insurance category.\nPlease enter 'F' or 'T'");
                }
            }
            string carDetailsForFile = ($"{Make},{Model},{Emission},{CategoryOfInsurance}");
        }

        // calculate and return vehiclerelated portion of insurance fee
        public double CalculateVehiclePart()
        {
            double emissionCharge = 0;
            double modelCharge = 0;
            double categoryCharge = 0;

            // calculate make/model charge
            if (this.Make == "BMW")
            {
                switch (Model)
                {
                    case "convertible":
                        modelCharge = 200;
                        break;
                    case "gran turismo":
                        modelCharge = 250;
                        break;
                    case "bmw x6":
                        modelCharge = 300;
                        break;
                    case "bmw z4":
                        modelCharge = 175;
                        break;
                    case "roadster":
                        modelCharge = 175;
                        break;
                    default:
                        modelCharge = 0;
                        break;
                }
            }
            else if (this.Make == "OPEL")
            {
                switch (Model)
                {
                    case "corsa":
                        modelCharge = 50;
                        break;
                    case "astra":
                        modelCharge = 105;
                        break;
                    case "vectra":
                        modelCharge = 150;
                        break;
                    default:
                        modelCharge = 0;
                        break;
                }
            }
            else if (this.Make == "TOYOTA")
            {
                switch (Model)
                {
                    case "yaris":
                        modelCharge = 50;
                        break;
                    case "auris":
                        modelCharge = 75;
                        break;
                    case "corolla":
                        modelCharge = 100;
                        break;
                    case "avensis":
                        modelCharge = 125;
                        break;
                    default:
                        modelCharge = 0;
                        break;
                }
            }
            else if (this.Make == "RENAULT")
            {
                switch (Model)
                {
                    case "fleunce":
                        modelCharge = 100;
                        break;
                    case "megane":
                        modelCharge = 75;
                        break;
                    case "clio":
                        modelCharge = 50;
                        break;
                    default:
                        modelCharge = 0;
                        break;
                }
            }
            else
            {
                modelCharge = 0;
            }

            // calculation for insurance category
            if (CategoryOfInsurance =="F")
            {
                categoryCharge = 200;
            }
            else if (CategoryOfInsurance == "T")
            {
                categoryCharge = -120;
            }

            // calculate emission charge

            if (this.Emission == "H")
            {
                emissionCharge = 300;
            }
            else if (this.Emission == "M")
            {
                emissionCharge = 150;
            }
            else if (this.Emission == "L")
            {
                emissionCharge = -55;
            }

            // return incurance portion payable based on car specifications
            return (modelCharge + emissionCharge + categoryCharge);
        }
        // method to output vehicle details entered
        public string returnCarDetails()
        {
            return $"\nMake: {Make}\nModel: {Model}\nEmission: {Emission}\nCategory of Insurance: {CategoryOfInsurance}\n";
        }
        // car details to be saved in the txt file
        public string CarDetailsForFile()
        {
            return $"{Make},{Model},{Emission},{CategoryOfInsurance},";
        }
    }

    // customer class inherit form Person
    public class Customer : Person
    {
        // properties
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public int Age;
        public string County { get; set; }

        // methods
        public void NewCustomer()
        {
            Console.Write("Enter customer's name: ");
            CustomerName = Console.ReadLine();
            Console.Write("Enter customer's gender (M/F): ");
            while (true)
            {
                Gender = Console.ReadLine().ToUpper();
                if (Gender == "M" || Gender == "F")
                    break;
                else
                {
                    Console.WriteLine("Invalid option for customer gender\n\nPlease enter 'M' or 'F'");
                }
            }
            while (true)
            {
                Console.Write("Enter customer's age: ");
                string ageString = Console.ReadLine().Trim();
                if (int.TryParse(ageString, out Age))
                {
                    if (Age >= 80)
                    {
                        Console.WriteLine("No insurance provided for over 80's");
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter age, using the number keys only");
                }
            }
            Console.Write("Enter customer's county: ");
            County = Console.ReadLine().ToUpper().Trim();
            string customerDetailsForFile = ($"{CustomerName},{Gender},{Age.ToString()},{County}");
        }

        // calculate and return customer part of insurance
        public double CalculateCustomerPart()
        {
            double baseCharge = 1000;
            double genderCharge = 0;
            double countyCharge = 0;
            double ageCharge = 0;

            // calculate gender charge
            if (Gender == "M")
            {
                genderCharge = baseCharge * 2;
            }
            else if (Gender == "F")
            {
                genderCharge = baseCharge * 0.8;
            }

            // calculate county charge            
            switch (County)
            {
                case "CORK":
                    countyCharge = 50;
                    break;
                case "CLARE":
                    countyCharge = 225;
                    break;
                case "KERRY":
                    countyCharge = 50;
                    break;
                case "LIMERICK":
                    countyCharge = -75;
                    break;
                case "TIPPERARY":
                    countyCharge = -80;
                    break;
                case "WATERFORD":
                    countyCharge = -100;
                    break;
                default:
                    countyCharge = 0;
                    break;
            }

            // calculate age chage
            if (Age < 20)
            {
                ageCharge = genderCharge * 0.2;
            }
            else if (Age <= 35)
            {
                ageCharge = genderCharge * (-0.4);
            }
            else if (Age < 80)
            {
                ageCharge = genderCharge * (-0.65);
            }

            // return incurance portion payable based on customer specifications
            return (genderCharge + countyCharge + ageCharge);
        }

        // method to output customer details
        public string returnCustomerDetails()
        {
            return $"\nName: {CustomerName}\nGender: {Gender}\nAge: {Age}\nCounty: {County}\n";
        }
        // customer details to be saved in the txt file
        public string CustomerDetailsForFile()
        {
            return $"{CustomerName},{Gender},{Age},{County},";
        }
    }

    /////////////////////////////////////// classes to be populated in a later sprint

    // person superclass
    public class Person
    { }

    // Employee class inherits from Person
    public class Employee : Person
    {
        public string userName { get; set; }
        public string password { get; set; }

        // login username and password request
        public void login()
        {
            Console.Write("Username: ");
            userName = Console.ReadLine();

            Console.Write("Password: ");
            password = Console.ReadLine();

            // user authentication
            if (userName == "Martin Munster" && password == "Hello World!")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WindowHeight = 55;
                Console.WindowWidth = 55;
                Console.Clear();
                Console.WriteLine("   ************************************************ ");
                Console.WriteLine("   *                                              * ");
                Console.WriteLine("   *   Welcome to Munster Motor Insurance Group   * ");
                Console.WriteLine("   *       Car insurance quotation creator        * ");
                Console.WriteLine("   *                                              * ");
                Console.WriteLine("   ************************************************ ");
                Console.WriteLine("   *                                              * ");
                Console.WriteLine("   *     Fill in details to generate a quote!     * ");
                Console.WriteLine("   *                                              * ");
                Console.WriteLine("   ************************************************ ");
                Console.WriteLine("\n\n");
            }
            // if authentication fails, close application
            else
            {
                Console.WriteLine("You are not authorised to use this quote generator");
                Console.WriteLine("Please contact your system administrator for assistance");
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }
    }
    
    // Manager inherits from Employee
    public class Manager : Employee
    {
        // run reports
    }
}