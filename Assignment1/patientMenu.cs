using System;
using System.IO;
using System.Numerics;

namespace Assignment1
{

    public class PatientMenu
    {
        Information info = new Information();
        Patient patient;
        Doctor doctor;
        

        int ID;
        public PatientMenu(int ID)
        {
            this.ID = ID;
        }

        public void displayPatientMenu(Information info)
        {
            patient = info.patientInfo(ID);
            if (patient != null)
            {
                bool invalidChoice = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine(" __________________________________________________");
                    Console.WriteLine(" |         DOTNET Hospital Management System       |");
                    Console.WriteLine(" |_________________________________________________|");
                    Console.WriteLine(" |                   Patient Menu                  |");
                    Console.WriteLine(" |_________________________________________________|\n\n");
                    Console.WriteLine("Welcome to DOTNET HOSPITAL Management {0} patientson\n\n", patient.firstName);
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. List patient details");
                    Console.WriteLine("2. List my doctor details");
                    Console.WriteLine("3. List all appointment");
                    Console.WriteLine("4. Book appointment");
                    Console.WriteLine("5. Exit to login");
                    Console.WriteLine("6. Exit system");

                    List<int> menus = new List<int> { 1, 2, 3, 4, 5, 6 };
                    if (int.TryParse(Console.ReadLine(), out int userInput))
                    {
                        switch (userInput)
                        {
                            case 1:
                                listPatient(patient);
                                break;
                            case 2:
                                listDoctor(patient);
                                break;
                            case 3:
                                listAppointment(patient);
                                break;
                            case 4:
                                bookAppointment(patient);
                                break;
                            case 5:
                                logout();
                                break;
                            case 6:
                                exit();
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please select a valid option.");
                                invalidChoice = true;
                                Console.ReadKey();
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                        invalidChoice = true;
                        Console.ReadKey();
                    }

                } while (invalidChoice);
                
            }
            
        }


        public void listPatient(Patient patient)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   My Details                    |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("{0} {1} patientson's Details\n\n", patient.firstName, patient.lastName);
            Console.WriteLine("Patient ID: " + patient.ID);
            Console.WriteLine("Full Name: {0} {1} ", patient.firstName, patient.lastName);
            Console.WriteLine("Address: {0} {1} {2} {3}", patient.streetNum, patient.street, patient.city, patient.state);
            Console.WriteLine("Email: " + patient.email);
            Console.WriteLine("Phone: " + patient.phone);
            Console.ReadKey();
            displayPatientMenu(info);
        }

        public void listDoctor(Patient patient)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                    My Doctor                    |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);

            if (patientAppointments.Count > 0)
            {
                List<int> displayedDoctorIds = new List<int>();

                foreach (Appointment appointment in patientAppointments)
                {
                    if (!displayedDoctorIds.Contains(appointment.doctorID))
                    {
                        doctor = info.doctorInfo(appointment.doctorID);
                        if (doctor != null)
                        {
                            Console.WriteLine("Your doctor: {0}\n\n", doctor.firstName);
                            Console.WriteLine("Name\t\tEmail Address\t\t\tPhone\t\tAddress");
                            Console.WriteLine("-------------------------------------------------------------------------------------");
                            Console.WriteLine(doctor.ToString());

                            displayedDoctorIds.Add(appointment.doctorID);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("You don't have any doctor allocated.");
            }
            Console.ReadKey();
            displayPatientMenu(info);
        }

        public void listAppointment(Patient patient)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                 My Appointments                 |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            
            List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);
            if (patientAppointments.Count > 0)
            {
                Console.WriteLine("Appointments for " + patient.firstName + " patientson:\n\n");
                Console.WriteLine("Doctor\t\tPatient\t\tDescription");
                Console.WriteLine("---------------------------------------------------");
                foreach (Appointment appointment in patientAppointments)
                {
                    Console.WriteLine(appointment.ToString());
                }
            }
            else
            {
                Console.WriteLine("You don't have any appointments.");
            }
            Console.ReadKey();
            displayPatientMenu(info);
        }

        public void bookAppointment(Patient patient)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                Book Appointments                |");
            Console.WriteLine(" |_________________________________________________|\n\n");

            string appointmentFilePath = "appointment.txt";
            List<string> appointmentLines = File.ReadAllLines(appointmentFilePath).ToList();
            bool hasPreviousAppointment = false;

            foreach (string line in appointmentLines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 4 && parts[0] == patient.ID.ToString())
                {
                    hasPreviousAppointment = true;
                    break;
                }
            }

            if (!hasPreviousAppointment)
            {
                List<Doctor> doctors = info.GetAllDoctors();

                if (doctors.Count == 0)
                {
                    Console.WriteLine("No doctors available for booking appointments.");
                    Console.ReadKey();
                    displayPatientMenu(info);
                    return;
                }

                Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");

                for (int i = 0; i < doctors.Count; i++)
                {
                    Console.WriteLine("{0} {1} {2} | {3} | {4} | {5} {6} {7} {8}", i + 1, doctors[i].firstName, doctors[i].lastName, doctors[i].email, doctors[i].phone, doctors[i].streetNum, doctors[i].street, doctors[i].city, doctors[i].state);
                }

                int selectedDoctorIndex = -1;

                while (selectedDoctorIndex < 0 || selectedDoctorIndex >= doctors.Count)
                {
                    Console.WriteLine("\nPlease choose a doctor:");
                    if (int.TryParse(Console.ReadLine(), out int doctorChoice))
                    {
                        selectedDoctorIndex = doctorChoice - 1;
                        Console.WriteLine("\nYou are booking a new appointment with " + doctors[selectedDoctorIndex].firstName + " doctorson");
                    }

                    if (selectedDoctorIndex < 0 || selectedDoctorIndex >= doctors.Count)
                    {
                        Console.WriteLine("Invalid choice. Please select a valid doctor.");
                    }
                }

                Doctor selectedDoctor = doctors[selectedDoctorIndex];

                Console.WriteLine("Description of the appointment: ");
                string description = Console.ReadLine();

                using (StreamWriter writer = File.AppendText(appointmentFilePath))
                {
                    string patId = Convert.ToString(patient.ID);
                    writer.Write("\n"+ patId+ ","+patient.firstName+ ","+ patient.lastName + "," + selectedDoctor.ID + "," + selectedDoctor.firstName + "," + selectedDoctor.lastName + "," + description);
                }
                Console.WriteLine("\nThe appointment has been booked successfully.");
            }
            else
            {
                List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);

                if (patientAppointments.Count > 0)
                {
                    Doctor doctor = info.doctorInfo(patientAppointments[0].doctorID);

                    if (doctor != null)
                    {
                        Console.WriteLine("You are booking a new appointment with " + doctor.firstName + " doctorson\n");
                        Console.WriteLine("Description of the appointment: ");
                        string description = Console.ReadLine();

                        using (StreamWriter writer = File.AppendText(appointmentFilePath))
                        {
                            string patId = Convert.ToString(patient.ID);
                            writer.Write("\n" + patId + "," + patient.firstName + "," + patient.lastName + "," + doctor.ID + "," + doctor.firstName + "," + doctor.lastName + "," + description);
                        }

                        Console.WriteLine("\nThe appointment description has been updated successfully.");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have any appointments with doctors.");
                }

            }
            Console.ReadKey();
            displayPatientMenu(info);
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