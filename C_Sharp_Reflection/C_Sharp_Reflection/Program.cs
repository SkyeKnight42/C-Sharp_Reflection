using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace C_Sharp_Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> optionsList = new List<string>() { "Create a new car.", "Exit the program" };
            int carValuePlacement = 1;
            string carValueString = "View value of a car";
            int carRegPlacement = 2;
            string carRegString = "View registration of a car";
            int carYearPlacement = 3;
            string carYearString = "View year of registration of a car";
            int carStatsPlacement = 4;
            string carStatsString = "View statistics of a car";

            Car newCar1 = new Car();
            newCar1.carValues("Ford", "Mondeo", "ABCD123", 1998, 2000);
            Car newCar2 = new Car();
            newCar2.carValues("Vauxhall", "Car", "ABCD123", 2002, 50000);
            Car newCar3 = new Car();
            newCar3.carValues("Ferrari", "Vulcan", "FAST 0N3", 2016, 200000);

            List<Car> carStorage = new List<Car>();

            carStorage.Add(newCar1);
            carStorage.Add(newCar2);
            carStorage.Add(newCar3);

            bool exitStatus = false;
            bool addOptions = true;

            int currentYear = DateTime.Now.Year;
            Console.WriteLine(currentYear);
            do
            {
                if (carStorage.Count > 0 && addOptions)
                {
                    optionsList.Insert(carValuePlacement, carValueString);
                    optionsList.Insert(carRegPlacement, carRegString);
                    optionsList.Insert(carYearPlacement, carYearString);
                    optionsList.Insert(carStatsPlacement, carStatsString);
                    addOptions = false;
                }
                Console.Clear();
                Console.WriteLine("This is a car storage program. \nPlease select enter one of the selections below");
                for (int x = 1; x <= optionsList.Count; x++)
                    Console.Write("[" + x + "] " + optionsList[x - 1] + "\n");

                int userInput;
                if (!Int32.TryParse(Console.ReadLine(), out userInput))
                    Console.Clear();
                //Console.WriteLine("User Input was: " + userInput);
                if (userInput != 0)
                    switch (userInput)
                    {
                        case 1: // Create a car
                            Console.Clear();
                            CreateACar();
                            break;
                        case 2: // Value or Exit
                            if (carStorage.Count > 0) // Value
                                ViewValue();
                            else // Exit
                                exitStatus = true;
                            break;
                        case 3: //REG
                            ViewRegistration();
                            break;
                        case 4: // YEAR OF REG
                            ViewRegYear();
                            break;
                        case 5:
                            ViewDetails();
                            break;
                        case 6:
                            if (carStorage.Count > 0) // Exit application
                                exitStatus = true;
                            break;
                        default:
                            Console.Clear();
                            break;
                    }
            } while (exitStatus == false);

            Environment.Exit(0);

            void CreateACar()
            {
                int entryTracker = 0;
                string make = "";
                string model = "";
                string registration = "";
                int year = 0;
                int value = 0;
                Console.Clear();
                Console.WriteLine("Please enter the car details in the following format");
                Console.WriteLine("MAKE MODEL REGISTRATION(1234ABC) YEAR(0000) VALUE(£)");
                Console.WriteLine("Between each detail, please hit the return key");
                do
                {
                    //var userInput = Console.ReadLine();

                    switch (entryTracker)
                    {
                        case 0: // MAKE
                            Console.Clear();
                            Console.WriteLine("Please enter the car MAKE.");
                            do
                            {
                                string input = Console.ReadLine();
                                if (Regex.IsMatch(input, @"^[a-zA-Z]+$"))
                                    make = input;
                            } while (make.Length == 0);
                            entryTracker++;
                            break;
                        case 1: // MODEL
                            Console.Clear();
                            Console.WriteLine("Please enter the car MODEL.");
                            do
                            {
                                string input = Console.ReadLine();
                                if (Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
                                    model = input;
                            } while (model.Length == 0);
                            entryTracker++;
                            break;
                        case 2: // REG
                            Console.Clear();
                            Console.WriteLine("Please enter the car REGISTRATION.\n[0000000]");
                            Console.WriteLine("Please use capital letters.");
                            do
                            {
                                string input = Console.ReadLine();
                                if (Regex.IsMatch(input, @"^[A-Z0-9]+$"))
                                    registration = input;
                            } while (registration.Length < 7);
                            entryTracker++;
                            break;
                        case 3: // YEAR REG
                            Console.Clear();
                            Console.WriteLine("Please enter the YEAR of REGISTRATION.");
                            do
                            {
                                string temp = Console.ReadLine();
                                if (Regex.IsMatch(temp, @"^[0-9]+$") && temp.Length == 4)
                                    year = Int32.Parse(temp);
                            } while (year == 0 && year <= currentYear);
                            entryTracker++;
                            break;
                        case 4: // VALUE
                            Console.Clear();
                            Console.WriteLine("Please enter the VALUE of the car.");
                            do
                            {
                                string temp = Console.ReadLine();

                                if (Regex.IsMatch(temp, @"^[0-9]+$"))
                                    value = Int32.Parse(temp);
                            } while (value == 0 || value < 0);
                            entryTracker++;
                            break;
                        default:

                            break;
                    }
                } while (entryTracker < 5);

                Car newCar = new Car();
                newCar.carValues(make, model, registration, year, value);
                carStorage.Add(newCar);
                Console.Clear();
            }

            void ViewValue()
            {
                bool inputValid = false;
                int input;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Please select an option from below to see its value.");
                    for (int i = 1; i <= carStorage.Count; i++)
                    {
                        Console.WriteLine("[" + i + "] " + carStorage[i - 1].carMake + " " + carStorage[i - 1].carModel + ".");
                    }
                    bool valid = Int32.TryParse(Console.ReadLine(), out input);
                    if (input > 0 && input <= carStorage.Count && valid)
                        inputValid = true;
                } while (!inputValid);
                Console.WriteLine("The value of " + carStorage[input-1].carMake + " " + carStorage[input-1].carModel + " is: £" + carStorage[input-1].carValue);
                Console.ReadLine();
            }

            void ViewRegistration()
            {
                bool inputValid = false;
                int input;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Please select an option from below to see its registration.");
                    for (int i = 1; i <= carStorage.Count; i++)
                    {
                        Console.WriteLine("[" + i + "] " + carStorage[i - 1].carMake + " " + carStorage[i - 1].carModel + ".\n");
                    }
                    bool valid = Int32.TryParse(Console.ReadLine(), out input);
                    if (input > 0 && input <= carStorage.Count && valid)
                        inputValid = true;
                } while (!inputValid);
                Console.WriteLine("The registration of " + carStorage[input - 1].carMake + " " + carStorage[input - 1].carModel + " is: " + carStorage[input - 1].carRegistration);
                Console.ReadLine();
            void ViewRegistration()
            {
                bool inputValid = false;
                int input;
                Console.Clear();
                Console.WriteLine("Please select an option from below to see its registration.");
                for (int i = 1; i <= carStorage.Count; i++)
                {
                    Console.WriteLine("[" + i + "] " + carStorage[i - 1].carMake + " " + carStorage[i - 1].carModel + ".\n");
                }
                do
                {
                    input = Int32.Parse(Console.ReadLine());
                    if (input > 0 && input <= carStorage.Count)
                        inputValid = true;
                } while (!inputValid);
                Console.WriteLine("The registration of " + carStorage[input - 1].carMake + " " + carStorage[input - 1].carModel + " is: " + carStorage[input - 1].carRegistration);
                Console.ReadLine();
            }
            }

            void ViewRegYear()
            {
                bool inputValid = false;
                int input;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Please select an option from below to see its year of registration.");
                    for (int i = 1; i <= carStorage.Count; i++)
                    {
                        Console.WriteLine("[" + i + "] " + carStorage[i - 1].carMake + " " + carStorage[i - 1].carModel + ".\n");
                    }
                    bool valid = Int32.TryParse(Console.ReadLine(), out input);
                    if (input > 0 && input <= carStorage.Count && valid)
                        inputValid = true;
                } while (!inputValid);
                Console.WriteLine("The year of registration of " + carStorage[input - 1].carMake + " " + carStorage[input - 1].carModel + " is: " + carStorage[input - 1].carYearofReg);
                Console.ReadLine();
            }

            void ViewDetails()
            {
                bool inputValid = false;
                int input;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Please select an option from below to see a cars full details");
                    for (int i = 1; i <= carStorage.Count; i++)
                    {
                        Console.WriteLine("[" + i + "] " + carStorage[i - 1].carMake + " " + carStorage[i - 1].carModel + ".\n");
                    }
                    bool valid = Int32.TryParse(Console.ReadLine(), out input);
                    if (input > 0 && input <= carStorage.Count && valid)
                        inputValid = true;
                } while (!inputValid);
                input--;
                Console.WriteLine("\nCar Make: " + carStorage[input].carMake);
                Console.WriteLine("\nCar Model: " + carStorage[input].carModel);
                Console.WriteLine("\nCar Registration: " + carStorage[input].carRegistration);
                Console.WriteLine("\nCar Year of Registration: " + carStorage[input].carYearofReg);
                Console.WriteLine("\nCar Value: £" + carStorage[input].carValue);
                Console.ReadLine();
            }
        }
    }

    class Car
    {
        public string carMake { get; set; }
        public string carModel { get; set; }
        public string carRegistration { get; set; }
        public int carYearofReg { get; set; }
        public int carValue { get; set; }

        public void carValues(string make, string model, string reg, int yearreg, int value)
        {
            carMake = make;
            carModel = model;
            carRegistration = reg;
            carYearofReg = yearreg;
            carValue = value;
        }
    }
}
