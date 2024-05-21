using System;
using System.IO;
using System.Net;

namespace Assignment1
{
    public class Information
    {
        public Patient patient;
        public Doctor doctor;
        public Admin admin;
        public Appointment appointment;

        public Information()
        {

        }
        public Information(Patient patient, Doctor doctor, Admin admin, Appointment appointment)
        {
            this.patient = patient;
            this.doctor = doctor;
            this.admin = admin;
            this.appointment = appointment;
        }

        public Patient patientInfo(int ID)
        {
            patient = null;

            if (File.Exists("patient.txt"))
            {
                string[] lines = File.ReadAllLines("patient.txt");

                foreach (string currentPat in lines)
                {
                    string[] info = currentPat.Split(',');
                    if (ID.ToString() == info[0])
                    {
                        patient = new Patient(ID, info[1], info[2], info[3], info[4], Convert.ToInt32(info[5]), info[6], info[7], info[8]);
                    }
                }
            }
            return patient;
        }

        public Doctor doctorInfo(int ID)
        {
            doctor = null;
            if (File.Exists("doctor.txt"))
            {
                string[] lines = File.ReadAllLines("doctor.txt");

                foreach (string currentDoc in lines)
                {
                    string[] info = currentDoc.Split(',');
                    if (ID.ToString() == info[0])
                    {
                        doctor = new Doctor(ID, info[1], info[2], info[3], info[4], Convert.ToInt32(info[5]), info[6], info[7], info[8]);
                        break;
                    }
                }
            }
            return doctor;
        }

        public Admin adminInfo(int ID)
        {
            admin = null;
            if (File.Exists("admin.txt"))
            {
                string[] lines = File.ReadAllLines("admin.txt");

                foreach (string currentAdm in lines)
                {
                    string[] info = currentAdm.Split(',');
                    if (ID.ToString() == info[0])
                    {
                        admin = new Admin(ID, info[1], info[2]);
                    }
                }
            }
            return admin;
        }

        public Appointment appointmentInfo(int ID)
        {
            appointment = null;
            if (File.Exists("appointment.txt"))
            {
                string[] lines = File.ReadAllLines("appointment.txt");

                foreach (string currentApp in lines)
                {
                    string[] info = currentApp.Split(',');
                    if (ID.ToString() == info[0])
                    {
                        appointment = new Appointment(ID, info[1], info[2], Convert.ToInt32(info[3]), info[4], info[5], info[6]);
                    }
                }
            }
            return appointment;
        }

        public List<Appointment> GetPatientAppointments(int patientID)
        {
            List<Appointment> appointments = new List<Appointment>();

            if (File.Exists("appointment.txt"))
            {
                string[] lines = File.ReadAllLines("appointment.txt");

                foreach (string currentApp in lines)
                {
                    string[] info = currentApp.Split(',');

                    if (patientID.ToString() == info[0])
                    {
                        string pFirstName = info[1];
                        string pLastName = info[2];
                        int doctorID = Convert.ToInt32(info[3]);
                        string dFirstName = info[4];
                        string dLastName = info[5];
                        string description = info[6];
                        appointments.Add(new Appointment(patientID, pFirstName, pLastName, doctorID, dFirstName, dLastName, description));
                    }
                }
            }

            return appointments;
        }

        public List<Appointment> GetDoctorAppointments(int doctorID)
        {
            List<Appointment> appointments = new List<Appointment>();

            if (File.Exists("appointment.txt"))
            {
                string[] lines = File.ReadAllLines("appointment.txt");

                foreach (string currentApp in lines)
                {
                    string[] info = currentApp.Split(',');

                    if (doctorID.ToString() == info[3])
                    {
                        string pFirstName = info[1];
                        string pLastName = info[2];
                        int patientID = Convert.ToInt32(info[0]);
                        string dFirstName = info[4];
                        string dLastName = info[5];
                        string description = info[6];
                        appointments.Add(new Appointment(patientID, pFirstName, pLastName, doctorID, dFirstName, dLastName, description));
                    }
                }
            }

            return appointments;
        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> allDoctors = new List<Doctor>();

            if (File.Exists("doctor.txt"))
            {
                string[] lines = File.ReadAllLines("doctor.txt");

                foreach (string currentDoc in lines)
                {
                    string[] info = currentDoc.Split(',');

                    doctor = new Doctor(Convert.ToInt32(info[0]), info[1], info[2], info[3], info[4], Convert.ToInt32(info[5]), info[6], info[7], info[8]);

                    allDoctors.Add(doctor);
                }
            }
            return allDoctors;
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> allPatients = new List<Patient>();

            if (File.Exists("patient.txt"))
            {
                string[] lines = File.ReadAllLines("patient.txt");

                foreach (string currentPat in lines)
                {
                    string[] info = currentPat.Split(',');

                    Patient patient = new Patient(Convert.ToInt32(info[0]), info[1], info[2], info[3], info[4], Convert.ToInt32(info[5]), info[6], info[7], info[8]);

                    allPatients.Add(patient);
                }
            }
            return allPatients;
        }


        public int GenerateUniqueDoctorID()
        {
            int maxID = 0;
            if (File.Exists("doctor.txt"))
            {
                string[] lines = File.ReadAllLines("doctor.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    int doctorID = int.Parse(parts[0]);
                    if (doctorID > maxID)
                    {
                        maxID = doctorID;
                    }
                }
            }
            return maxID + 1;
        }

        public int GenerateUniquePatientID()
        {
            int maxID = 0;
            if (File.Exists("patient.txt"))
            {
                string[] lines = File.ReadAllLines("patient.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    int patientID = int.Parse(parts[0]);
                    if (patientID > maxID)
                    {
                        maxID = patientID;
                    }
                }
            }
            return maxID + 1;
        }
    }
}