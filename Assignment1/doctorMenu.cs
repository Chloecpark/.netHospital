using System;

namespace Assignment1
{
	public class DoctorMenu
    {
        Information info = new Information();
        Patient patient;
        Doctor doctor;

        int ID;

        public DoctorMenu(int ID)
        {
            this.ID = ID;
        }

        public void displayDoctorMenu(Information info)
        {
            doctor = info.doctorInfo(ID);
            if (doctor != null)
            {
                bool invalidChoice = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine(" __________________________________________________");
                    Console.WriteLine(" |         DOTNET Hospital Management System       |");
                    Console.WriteLine(" |_________________________________________________|");
                    Console.WriteLine(" |                   Doctor Menu                   |");
                    Console.WriteLine(" |_________________________________________________|\n\n");
                    Console.WriteLine("Welcome to DOTNET HOSPITAL Management " + doctor.firstName + " doctorson\n\n");
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. List doctor details");
                    Console.WriteLine("2. List patients");
                    Console.WriteLine("3. List appointments");
                    Console.WriteLine("4. Check particular patient");
                    Console.WriteLine("5. List appointments with patient");
                    Console.WriteLine("6. Exit to login");
                    Console.WriteLine("7. Exit system");

                    List<int> menus = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
                    if (int.TryParse(Console.ReadLine(), out int userInput))
                    {
                        switch (userInput)
                        {
                            case 1:
                                listDoctor(doctor);
                                break;
                            case 2:
                                listPatient(doctor);
                                break;
                            case 3:
                                listAppointment(doctor);
                                break;
                            case 4:
                                checkPatient(doctor);
                                break;
                            case 5:
                                listAppointmentWPatient(doctor);
                                break;
                            case 6:
                                logout();
                                break;
                            case 7:
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

        public void listDoctor(Doctor doctor)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   My Details                    |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Name\t\tEmail Address\t\t\tPhone\t\tAddress");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine(doctor.ToString());
            Console.ReadKey();
            displayDoctorMenu(info);
        }

        public void listPatient(Doctor doctor)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                   My Patients                   |");
            Console.WriteLine(" |_________________________________________________|\n\n");

            List<Appointment> doctorAppointments = info.GetDoctorAppointments(doctor.ID);

            if (doctorAppointments.Count == 0)
            {
                Console.WriteLine("You don't have any patients.");
                Console.ReadKey();
                displayDoctorMenu(info);
                return;
            }

            Console.WriteLine("Name\t\tDoctor\t\tEmail Address\t\t\tPhone\t\tAddress");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");

            List<int> displayedPatients = new List<int>();

            foreach (Appointment appointment in doctorAppointments)
            {
                patient = info.patientInfo(appointment.ID);

                if (patient != null && !displayedPatients.Contains(patient.ID))
                {
                    List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);

                    string doctorName = "N/A";

                    if (patientAppointments.Count > 0)
                    {
                        doctorName = patientAppointments[0].dFirstName + " " + patientAppointments[0].dLastName;
                    }
                    Console.WriteLine(patient.ToString(doctorName));
                    displayedPatients.Add(patient.ID);
                }
            }

            Console.ReadKey();
            displayDoctorMenu(info);
        }

        public void listAppointment(Doctor doctor)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                 All Appointments                |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Doctor\t\tPatient\t\tDescription");
            Console.WriteLine("---------------------------------------------------");
            List<Appointment> doctorAppointments = info.GetDoctorAppointments(doctor.ID);
            if (doctorAppointments.Count > 0)
            {

                foreach (Appointment appointment in doctorAppointments)
                {
                    Console.WriteLine(appointment.ToString());
                }
            }
            else
            {
                Console.WriteLine("You don't have any appointments.");
            }
            Console.ReadKey();
            displayDoctorMenu(info);
        }

        public void checkPatient(Doctor doctor)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |              Check Patient Details              |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Enter the ID of the patient to check: ");

            int patientID;
            if (int.TryParse(Console.ReadLine(), out patientID))
            {
                Patient patient = info.patientInfo(patientID);

                if (patient != null)
                {
                    List<Appointment> doctorAppointments = info.GetDoctorAppointments(doctor.ID);
                    bool appointmentFound = false;
                    List<int> displayedPatients = new List<int>();

                    foreach (Appointment appointment in doctorAppointments)
                    {
                        if (appointment.ID == patientID)
                        {
                            appointmentFound = true;
                            break;
                        }
                    }

                    if (appointmentFound)
                    {
                        Console.WriteLine("\nDetails for " + patient.firstName + " patientson\n");
                        Console.WriteLine("Patient\t\tDoctor\t\tEmail Address\t\t\tPhone\t\tAddress");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------");

                        if (!displayedPatients.Contains(patient.ID))
                        {
                            List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);

                            string doctorName = "N/A";

                            if (patientAppointments.Count > 0)
                            {
                                doctorName = patientAppointments[0].dFirstName + " " + patientAppointments[0].dLastName;
                            }
                            Console.WriteLine(patient.ToString(doctorName));
                            displayedPatients.Add(patient.ID);
                        }
                    }
                    else
                    {
                        Console.WriteLine("This patient is not allocated to you.");
                    }
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
            displayDoctorMenu(info);
        }

        public void listAppointmentWPatient(Doctor doctor)
        {
            Console.Clear();
            Console.WriteLine(" __________________________________________________");
            Console.WriteLine(" |         DOTNET Hospital Management System       |");
            Console.WriteLine(" |_________________________________________________|");
            Console.WriteLine(" |                Appointments with                |");
            Console.WriteLine(" |_________________________________________________|\n\n");
            Console.WriteLine("Enter the ID of the patient you would like to view appointments for: ");

            int patientID;

            if (int.TryParse(Console.ReadLine(), out patientID))
            {
                Patient patient = info.patientInfo(patientID);

                if (patient != null)
                {
                    List<Appointment> patientAppointments = info.GetPatientAppointments(patient.ID);
                    bool doctorHeaderPrinted = false;
                    bool noAppointmentPrinted = false;

                    if (patientAppointments.Count > 0 )
                    {
                        foreach (Appointment appointment in patientAppointments)
                        {
                            if(appointment.doctorID == doctor.ID)
                            {
                                if(!doctorHeaderPrinted)
                                {
                                    Console.WriteLine("\nDoctor\t\tPatient\t\tDescription");
                                    Console.WriteLine("---------------------------------------------------");
                                    doctorHeaderPrinted = true;
                                }
                                Console.WriteLine(appointment.ToString());
                            }
                            else
                            {
                                if(!noAppointmentPrinted)
                                {
                                    Console.WriteLine("No appointments found for the selected patient.");
                                    noAppointmentPrinted = true;
                                }
                            }
                        }
                    }
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
            displayDoctorMenu(info);
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