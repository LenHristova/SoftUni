using System.Collections.Generic;

namespace P04_Hospital
{
    public class Doctor
    {
        public string Name { get; set; }
        public List<Patient> Patients { get; set; } 

        public Doctor(string name)
        {
            Name = name;
            Patients = new List<Patient>();
        }

        public void AddPatient(Patient patient)
        {
            Patients.Add(patient);
        }
    }
}