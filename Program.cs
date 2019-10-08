using System;
using static System.Console;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace Parkeringhus
{
    class Program
    {
        //static string connectionString= "Server=.;Database=Parkeringshus;Integrated Security=true";
        static string connectionString = "Server=.;Initial Catalog=CarPark;Integrated Security=true";
        //static string connectionString = "Data Source=.;Initial Catalog=Parkeringshus;Integrated Security=true";
        static void Main(string[] args)
        {
            bool shouldNotExit = true;

            while (shouldNotExit)
            {
                WriteLine("1. Register arrival");
                WriteLine("2. Register departure");
                WriteLine("3. Show parking registry");
                WriteLine("4. Exit");

                ConsoleKeyInfo keyPressed = ReadKey(true);

                Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        {
                            Write("First name: ");

                            string firstName = ReadLine();

                            Write("Last name: ");

                            string lastName = ReadLine();

                            Write("Social security number: ");

                            string socialSecurityNumber = ReadLine();

                            Write("Phone number: ");

                            string phoneNumber = ReadLine();

                            Write("E-mail: ");

                            string email = ReadLine();

                            Write("Registration number: ");

                            string registrationNumber = ReadLine();

                            Write("Notes: ");

                            string notes = ReadLine();

                            DateTime arrival = DateTime.Now;

                            RegisterArrival(
                                firstName,
                                lastName,
                                socialSecurityNumber,
                                phoneNumber,
                                email,
                                registrationNumber,
                                notes,
                                arrival);

                            Clear();

                            WriteLine("Arrival registered");

                            Thread.Sleep(2000);

                            break;
                        }

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            Write("Car: ");

                            string registrationNumber = ReadLine();

                            DateTime departure = DateTime.Now;

                            RegisterDeparture(registrationNumber, departure);
                        }

                        Clear();

                        // TODO: create notice snippet

                        WriteLine("Departure registered");

                        Thread.Sleep(2000);

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        {
                            ShowParkingRegistry();

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

        private static void ShowParkingRegistry()
        {
            WriteLine("ID   Registration number       Arrived at       Departed at");
            WriteLine("--------------------------------------------------------------");

            string query = @"SELECT Id, 
	                                FirstName,
                                    LastName,
                                    SocialSecurityNumber,
	                                PhoneNumber,
                                    Email,
	                                RegistrationNumber, 
	                                Notes, 
	                                ArrivedAt, 
	                                DepartedAt
	                           FROM Parking";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string id = dataReader["Id"].ToString();
                string registrationNumber = dataReader["RegistrationNumber"].ToString();
                string arrivedAt = dataReader["ArrivedAt"].ToString();
                string departedAt = dataReader["DepartedAt"].ToString();

                Write(id.PadRight(5, ' '));
                Write(registrationNumber.PadRight(24, ' '));
                Write(arrivedAt.PadRight(20, ' '));
                WriteLine(departedAt);
            }

            connection.Close();

            WriteLine();
            WriteLine("<Press key to continue");

            ReadKey(true);
        }

        private static void RegisterDeparture(string registrationNumber, DateTime departedAt)
        {
            string query = @"UPDATE Parking 
                                SET DepartedAt = @DepartedAt
                              WHERE RegistrationNumber = @RegistrationNumber
                                AND DepartedAt IS NULL";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("DepartedAt", departedAt);
            command.Parameters.AddWithValue("RegistrationNumber", registrationNumber);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        private static void RegisterArrival(string firstName, string lastName, string socialSecurityNumber, string phoneNumber, string email, string registrationNumber, string notes, DateTime arrivedAt)
        {
            string query = @"INSERT INTO Parking (FirstName, 
                                                  LastName, 
                                                  SocialSecurityNumber,
                                                  PhoneNumber,
                                                  Email,
                                                  RegistrationNumber, 
                                                  Notes, 
                                                  ArrivedAt)
                                          VALUES (@FirstName,
                                                  @LastName,
                                                  @SocialSecurityNumber, 
                                                  @PhoneNumber,
                                                  @Email, 
                                                  @RegistrationNumber, 
                                                  @Notes, 
                                                  @ArrivedAt)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@SocialSecurityNumber", socialSecurityNumber);
            command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
            command.Parameters.AddWithValue("@Notes", notes);
            command.Parameters.AddWithValue("@ArrivedAt", arrivedAt);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
