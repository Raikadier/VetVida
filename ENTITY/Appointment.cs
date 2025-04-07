using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public string Diagnostic { get; set; }
        public Veterinary Veterinary { get; set; }
        public Pet Pet { get; set; }

        public Appointment() { }

        public Appointment(DateTime date, string diagnostic, Veterinary veterinary, Pet pet)
        {
            Date = date;
            Diagnostic = diagnostic;
            Veterinary = veterinary;
            Pet = pet;
        }

        public override string ToString()
        {
            return $"Date: {Date}, Diagnostic: {Diagnostic}, Veterinary: {Veterinary}, Pet: {Pet}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Appointment appointment)
            {
                return Date == appointment.Date &&
                       Diagnostic == appointment.Diagnostic &&
                       EqualityComparer<Veterinary>.Default.Equals(Veterinary, appointment.Veterinary) &&
                       EqualityComparer<Pet>.Default.Equals(Pet, appointment.Pet);
            }
            return false;
        }

        // i can see you...
    }
}
