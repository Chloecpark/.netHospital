using System.IO;
using System;
namespace Assignment1
{
    public class Login
    {
        public int ID { get; set; }
        public String Password { get; set; }
        public UserRole Role { get; set; }

        public Login(int ID, string Password, UserRole Role)
        {
            this.ID = ID;
            this.Password = Password;
            this.Role = Role;
        }

        List<Login> users = new List<Login> { };

        static void Main(string[] args)
        {
            loginMenu();
        }

        public static void loginMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine(" __________________________________________________");
                Console.WriteLine(" |         DOTNET Hospital Management System       |");
                Console.WriteLine(" |_________________________________________________|");
                Console.WriteLine(" |                      Login                      |");
                Console.WriteLine(" |_________________________________________________|\n\n");
                Console.Write("ID: ");
                int ID;
                if (!int.TryParse(Console.ReadLine(), out ID))
                {
                    throw new ArgumentException("Invalid input for ID. Please enter a valid integer.");
                }

                string password = GetPassword();

                if (File.Exists("login.txt"))
                {
                    bool loginSuccessful = false;
                    string[] lines = File.ReadAllLines("login.txt");

                    foreach (string currentUser in lines)
                    {
                        string[] login = currentUser.Split(',');
                        if (ID.ToString() == login[0] && password == login[1])
                        {
                            loginSuccessful = true;
                            Information info = new Information();
                            Console.WriteLine("\nValid Credentials");
                            Console.ReadKey();

                            switch ((UserRole)Enum.Parse(typeof(UserRole), login[2], true))
                            {
                                case UserRole.Patient:
                                    PatientMenu pm = new PatientMenu(ID);
                                    pm.displayPatientMenu(info);
                                    Console.ReadKey();
                                    break;

                                case UserRole.Doctor:
                                    DoctorMenu dm = new DoctorMenu(ID);
                                    dm.displayDoctorMenu(info);
                                    Console.ReadKey();
                                    break;

                                case UserRole.Admin:
                                    AdminMenu am = new AdminMenu(ID);
                                    am.displayAdminMenu(info);
                                    Console.ReadKey();
                                    break;
                            }
                            break;
                        }
                    }
                    if (!loginSuccessful)
                    {
                        Console.WriteLine("\nIncorrect ID or password.");
                        Console.WriteLine("Press any key to login, press 'n' to exit the system");
                        ConsoleKeyInfo retryKey = Console.ReadKey();
                        Console.WriteLine();

                        if (retryKey.Key == ConsoleKey.N)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            loginMenu();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: login.txt is missing.\n");
                    Console.Write("Press any key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                loginMenu();
            }
        }



        public static string GetPassword()
        {
            Console.Write("Password: ");
            string password = "";

            ConsoleKeyInfo key_info;
            do
            {
                key_info = Console.ReadKey(true);
                if (!char.IsControl(key_info.KeyChar))
                {
                    password += key_info.KeyChar;
                    Console.Write("*");
                }
                else if (key_info.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key_info.Key != ConsoleKey.Enter);
            return password;
        }
    }

    public enum UserRole
    {
        Patient,
        Doctor,
        Admin
    }
}