using System;
using static System.Console;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Parkeringhus
{
    class Program
    {
        static string connenctionstring = "Data Source = (local); Initial Catalog = Parkeringhus; Integrated Security = true";
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



                        {
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


                            break;


                        }

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            Write("Registration number: ");
                            string registrationNumber = ReadLine();
                            DateTime departure = DateTime.Now;

                        string query = @"UPDATE Parking
                                        SET Departure = @Departure 
                                        WHERE RegistrationNumber = @RegistrationNumber
                                        AND Departure IS NULL";

                            SqlConnection connection = new SqlConnection(connenctionstring);
                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("Departure", departure);
                            command.Parameters.AddWithValue("RegistrationNumber", registrationNumber);
                            connection.Open();

                            command.ExecuteNonQuery();
                            connection.Close();

                           
                        }
                        Clear();
                        WriteLine("Departure registered");
                        Thread.Sleep(2000);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        {
                            WriteLine("ID       Registration number             Arrived at               Departed at");
                            WriteLine("-----------------------------------------------------------------------------");

                            string query = @"SELECT Id, 
                                        Customer,
                                        ContactDetails,
                                        RegistrationNumber,
                                        Description,
                                        Arrival, 
                                        Departure
                                        FROM Parking;";
                            SqlConnection connection = new SqlConnection(connenctionstring);
                            SqlCommand command = new SqlCommand(query, connection);

                            connection.Open();

                            SqlDataReader datareader = command.ExecuteReader();
                            while (datareader.Read())
                            {
                                string id = datareader["Id"].ToString();
                                string registrationNumber = datareader["RegistrationNumber"].ToString();
                                string arrival = datareader["Arrival"].ToString();
                                string departure = datareader["Departure"].ToString();


                                Write(id.PadRight(4, ' '));
                                Write(registrationNumber.PadRight(8, ' '));
                                Write(arrival.PadRight(16, ' '));
                                WriteLine(departure);

                            }



                            connection.Close();

                            WriteLine();
                            WriteLine("<press any key to continue>");

                            ReadKey(true);

                            break;
                        }

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
