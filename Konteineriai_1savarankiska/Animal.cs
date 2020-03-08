using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_savarankiska
{
    abstract class Animal
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public DateTime VaccinationDate { get; set; }

        public Animal(string name, string breed, string owner, string phone, DateTime vaccinationDate)
        {
            Name = name;
            Breed = breed;
            Owner = owner;
            Phone = phone;
            VaccinationDate = vaccinationDate;
        }
        abstract public bool isVaccinationExpired();

        public static bool operator <=(Animal lhs, Animal rhs)
        {
            if (lhs.Name.CompareTo(rhs.Name) > 0)
            {
                return true;
            }
            else if (lhs.Name.CompareTo(rhs.Name) == 0)
            {
                if (lhs.Owner.CompareTo(rhs.Owner) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool operator >=(Animal lhs, Animal rhs)
        {
            return !(lhs <= rhs);
        }
    }
}
