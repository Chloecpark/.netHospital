using System;
using System.IO;

namespace Assignment1
{
    public class AdminMenu
    {
        Information info = new Information();
        Patient patient;
        Doctor doctor;
        Admin admin;


        int ID;
        public AdminMenu(int ID)
        {
            this.ID = ID;
        }

        public void displayAdminMenu(Information info)
        {
            admin = info.adminInfo(ID);
            if (admin != null)
            {
                bool invalidChoice = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine(" __________________________________________________");
                    Console.WriteLine(" |         DOTNET Hospital Management System       |");
                    Console.WriteLine(" |_________________________________________________|");
                    Console.WriteLine(" |                    Admin Menu                   |");
                    Console.WriteLine(" |_________________________________________________|\n\n");
                    Console.WriteLine("Welcome to DOTNET HOSPITAL Management " + admin.firstName + " adminson\n\n");
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. List all doctors");
                    Console.WriteLine("2. Check doctor details");
                    Console.WriteLine("3. List all patient");
                    Console.WriteLine("4. Check patient details");
                    Console.WriteLine("5. Add doctor");
                    Console.WriteLine("6. Add patient");
                    Console.WriteLine("7. Exit to login");
                    Console.WriteLine("8. Exit system");

                    List<int> menus = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
                    if (int.TryParse(Console.ReadLine(), out int userInput))
                    {
                        switch (userInput)
                        {
                            case 1:
                                listDoctor(admin);
                                break;
                            case 2:
                                checkDoctorDetail(admin);
                                break;
                            case 3:
                                listPatient(admin);
                                break;
                            case 4:
                                checkPatientDetail(admin);
                                break;
                            case 5:
                                addDoctor(admin);
                                break;
                            case 6:
                                addPatient(admin);
                                break;
                            case 7:
                                logout();
                                break;
                            case 8:
                                exit();
                                break;
                            default:
                                invalidChoice = true;
                                Console.WriteLine("Invalid choice. Please select a valid option.");
                                Console.ReadKey();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.Please enter a valid integer.");
                        invalidChoice = true;
                        Console.ReadKey();
                    }
                } while (invalidChoice);   
            }
        }

        public void listDoctor(Admin admin)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   All Doctors                   |");
            Console.WriteLine(" |_________________________________________________|\n\n");

            List<Doctor> doctors = info.GetAllDoctors();

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctor in the system.");
                Console.ReadKey();
                displayAdminMenu(info);
                return;
            }

            Console.WriteLine("All doctors registered to the DOTNET Hospital Management system\n");

            Console.WriteLine("Name\t\tEmail Address\t\t\tPhone\t\tAddress");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < doctors.Count; i++)
            {
                Console.WriteLine(doctors[i].ToString());
            }
            Console.ReadKey();
            displayAdminMenu(info);
        }

        public void checkDoctorDetail(Admin admin)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                 Doctor Details                  |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Please enter the ID of the doctor who's details you are checking.");
            int doctorID;

            if (int.TryParse(Console.ReadLine(), out doctorID))
            {
                doctor = info.doctorInfo(doctorID);

                if (doctor != null)
                {
                    Console.WriteLine("\nDetails for " + doctor.firstName + " doctorson\n");
                    Console.WriteLine("Name\t\tEmail Address\t\t\tPhone\t\tAddress");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                    Console.WriteLine(doctor.ToString());
                }
                else
                {
                    Console.WriteLine("Doctor not found with the provided ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid doctor ID.");
            }

            Console.ReadKey();
            displayAdminMenu(info);
        }

        public void listPatient(Admin admin)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   All Petients                  |");
            Console.WriteLine(" |_________________________________________________|\n\n");

            List<Patient> patients = info.GetAllPatients();

            if (patients.Count == 0)
            {
                Console.WriteLine("No patient in the system.");
                Console.ReadKey();
                displayAdminMenu(info);
                return;
            }
            
            Console.WriteLine("All patients registered to the DOTNET Hospital Management system\n");
            Console.WriteLine("Name\t\tDoctor\t\tEmail Address\t\t\tPhone\t\tAddress");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            foreach (Patient patient in patients)
            {
                List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);

                string doctorName = "N/A\t";

                if (patientAppointments.Count > 0)
                {
                    doctorName = patientAppointments[0].dFirstName + " " + patientAppointments[0].dLastName;
                }

                Console.WriteLine(patient.ToString(doctorName));
            }

            Console.ReadKey();
            displayAdminMenu(info);
        }

        public void checkPatientDetail(Admin admin)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                 Patient Details                 |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Please enter the ID of the patient who's details you are checking.");
            int patientID;

            if (int.TryParse(Console.ReadLine(), out patientID))
            {
                patient = info.patientInfo(patientID);

                if (patient != null)
                {
                    List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);

                    string doctorName = "N/A\t";

                    if (patientAppointments.Count > 0)
                    {
                        
                        doctorName = patientAppointments[0].dFirstName + " " + patientAppointments[0].dLastName;
                    }

                    Console.WriteLine("\nDetails for " + patient.firstName + " patientson\n");
                    Console.WriteLine("Patient\t\tDoctor\t\tEmail Address\t\t\tPhone\t\tAddress");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                    Console.WriteLine(patient.ToString(doctorName));
                }
                else
                {
                    Console.WriteLine("Patient not found with the provided ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid patient ID.");
            }
            Console.ReadKey();
            displayAdminMenu(info);
        }

        public void addDoctor(Admin admin)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   Add Doctor                    |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management system");
            Console.WriteLine("First Name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Street Number: ");
            int streetNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Street: ");
            string street = Console.ReadLine();
            Console.WriteLine("City: ");
            string city = Console.ReadLine();
            Console.WriteLine("State: ");
            string state = Console.ReadLine();

            Doctor newDoctor = new Doctor(info.GenerateUniqueDoctorID(), firstName, lastName, email, phone, Convert.ToInt32(streetNum), street, city, state);

            using (StreamWriter writer = File.AppendText("doctor.txt"))
            {
                
                writer.Write("\n" + newDoctor.ID + "," + newDoctor.firstName + "," + newDoctor.lastName + "," + newDoctor.email + "," + newDoctor.phone + "," + newDoctor.streetNum + "," + newDoctor.street + "," + newDoctor.city + ","+ newDoctor.state);

            }

            Console.WriteLine($"Dr {newDoctor.firstName} has been added to the system!");

            Console.ReadKey();
            displayAdminMenu(info);
            
        }

        public void addPatient(Admin admin)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   Add Patient                   |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management system");
            Console.WriteLine("First Name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Street Number: ");
            int streetNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Street: ");
            string street = Console.ReadLine();
            Console.WriteLine("City: ");
            string city = Console.ReadLine();
            Console.WriteLine("State: ");
            string state = Console.ReadLine();

            Patient newPatient = new Patient(info.GenerateUniquePatientID(), firstName, lastName, email, phone, Convert.ToInt32(streetNum), street, city, state);

            using (StreamWriter writer = File.AppendText("patient.txt"))
            {

                writer.Write("\n" + newPatient.ID + "," + newPatient.firstName + "," + newPatient.lastName + "," + newPatient.email + "," + newPatient.phone + "," + newPatient.streetNum + "," + newPatient.street + "," + newPatient.city + "," + newPatient.state);

            }

            Console.WriteLine($"{newPatient.firstName} has been added to the system!");


            Console.ReadKey();
            displayAdminMenu(info);
        }

        public void logout()
        {
            Login.loginMenu();
        }

        public void exit()
        {
            Environment.Exit(0);
        }
    }
}