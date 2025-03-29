using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class PetOwner : Person
    {
        public string Status { get; set; }
        public List<Pet> Pets = new List<Pet>();

        public PetOwner() { }

        public PetOwner(string status) { Status = status; }

        public PetOwner(string name, string email, string phone, string address, string status) : base(name, email, phone, address)
        {
            Status = status;
        }

        public override string ToString()
        {
            return base.ToString() + $", Status: {Status}";
        }
    }
}
