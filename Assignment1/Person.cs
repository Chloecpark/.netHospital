using System;
using System.Data;
using System.IO;
using System.Net;
using System.Numerics;


namespace Assignment1
{
    public abstract class Person
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Person(int ID, string firstName, string lastName)
        {
            this.ID = ID;
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }

    public class Patient : Person
    {
        public string email { get; set; }
        public string phone { get; set; }
        public int streetNum { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public Patient(int ID, string firstName, string lastName, string email, string phone, int streetNum, string street, string city, string state) : base(ID, firstName, lastName)
        {
            this.email = email;
            this.phone = phone;
            this.streetNum = streetNum;
            this.street = street;
            this.city = city;
            this.state = state;
        }

        public string ToString(string doctorName)
        {
            return firstName + " " + lastName + "\t" + doctorName +"\t"+ email + "\t\t" + phone + "\t" + streetNum + " " + street + " " + city + " " + state;
        }
    }
    
    public class Doctor : Person
    {
        public string email { get; set; }
        public string phone { get; set; }
        public int streetNum { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public Doctor(int ID, string firstName, string lastName, string email, string phone, int streetNum, string street, string city, string state) : base(ID, firstName, lastName)
        {
            this.email = email;
            this.phone = phone;
            this.streetNum = streetNum;
            this.street = street;
            this.city = city;
            this.state = state;
        }


        public override string ToString()
        {
            return firstName +" "+ lastName +"\t" + email + "\t\t" + phone + "\t" + streetNum +" "+ street +" "+ city +" "+ state;
        }
    }

    public class Appointment : Person
    {
        public int doctorID { get; set; }
        public string dFirstName { get; set; }
        public string dLastName { get; set; }
        public string description { get; set; }

        public Appointment(int ID, string firstName, string lastName, int doctorID, string dFirstName, string dLastName, string description) : base(ID, firstName, lastName)
        {
            this.doctorID = doctorID;
            this.dFirstName = dFirstName;
            this.dLastName = dLastName;
            this.description = description;
        }
        public override string ToString()
        {
            return dFirstName + " " + dLastName + "\t" + firstName + " " + lastName + "\t" + description;
        }

    }
    public class Admin : Person
    {
        public Admin(int ID, string firstName, string lastName) : base(ID, firstName, lastName)
        {

        }
    }
}