using System;
using static System.Console;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Parkeringhus
{
    class Program
    {
        static string connenctionstring = "Data Source = .; Initial Catalog = Parkeringhus; Integrated Security = true";
        static void Main(string[] args)
        {


            bool shouldNotExit = true;
            while (shouldNotExit)

            {

                Console.WriteLine("1. Register arrivals");
                Console.WriteLine("2. Register departure");
                Console.WriteLine("3. Show Parking registery");
                Console.WriteLine("4. Exit");

                ConsoleKeyInfo keypressed = ReadKey(true);
                Clear();

                switch (keypressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Write("Customer Name:");
                        string customer = ReadLine();


                        Write("contact details:");
                        string contactDetails = ReadLine();

                        Write("Registration number:");
                        string registrationNumber = ReadLine();

                        Write("Description:");
                        string description = ReadLine();
                        DateTime arrival = DateTime.Now;
                        string query = @"INSERT INTO Parking(Customer, ContactDetails, RegistrationNumber, Description, Arrival)
                                        VALUES(@Customer, @ContactDetails, @RegistrationNumber, @Description, @Arrival)";

                        SqlConnection connection = new SqlConnection(connenctionstring);
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Customer", customer);
                        command.Parameters.AddWithValue("@ContactDetails", contactDetails);
                        command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Arrival", arrival);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        // Add method to insert record into database

                        Clear();

                        WriteLine("Parkering registered");

                        Thread.Sleep(2000);
                        
                        //Write("Arrival (yyyy-mm-dd  hh:mm):");
                        //string arrival = ReadLine();

                        //Write("Departure (yyyy-mm-dd  hh:mm):");
                        //string departure = ReadLine();

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        shouldNotExit = false;
                        break;



                }


                Clear();

            }
        }
    }
}
