using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_savarankiska
{
    class GuineaPig : Animal
    {
        private static int VaccinationDuration = 1;
        public GuineaPig(string name, string breed, string owner, string phone, DateTime vaccinationDate)
            : base(name, breed, owner, phone, vaccinationDate)
        {
        }
        public override bool isVaccinationExpired()
        {
            return VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) > 0;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as GuineaPig);
        }

        public bool Equals(GuineaPig guineaPig)
        {
            return base.Equals(guineaPig);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Breed.GetHashCode();
        }
        public static bool operator ==(GuineaPig lhs, GuineaPig rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }
                return false;
            }
            return lhs.Equals(rhs);
        }
        public static bool operator !=(GuineaPig lhs, GuineaPig rhs)
        {
            return !(lhs == rhs);
        }
        public override String ToString()
        {
            return String.Format(" Breed: {0,-20} Name: {1,-10} Owner: {2,-10} ({3}) Last vaccination date: {4:yyyy-MM-dd}", Breed, Name, Owner, Phone, VaccinationDate);
        }
    }
}