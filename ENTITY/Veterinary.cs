using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Veterinary : Person
    {
        public string Speciality { get; set; }
        public string Position { get; set; }
        public Veterinary() { }

        public Veterinary(string speciality, string position) { Speciality = speciality; Position = position; }

        public Veterinary(string name, string email, string phone, string address, string speciality, string position) : base(name, email, phone, address)
        {
            Speciality = speciality;
            Position = position;
        }

        public override string ToString()
        {
            return base.ToString() + $", Speciality: {Speciality}, Position: {Position}";
        }
    }
}
