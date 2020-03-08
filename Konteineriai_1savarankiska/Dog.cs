using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_savarankiska
{
    class Dog : AnimalMarked
    {
        private static int VaccinationDuration = 1;
        public bool Aggressive { get; set; }
        public Dog(string name, string breed, string owner, string phone, DateTime vaccinationDate, int chipId, bool aggressive) :
            base(name, breed, owner, phone, vaccinationDate, chipId)
        {
            Aggressive = aggressive;
        }
        public override bool isVaccinationExpired()
        {
            return VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) > 0;
        }

        public override String ToString()
        {
            return String.Format("ChipId: {0,-5} Breed: {1,-20} Name: {2,-10} Owner: {3,-10} ({4}) Last vaccination date: {5:yyyy-MM-dd} Agressive: {6}", ChipId, Breed, Name, Owner, Phone, VaccinationDate, Aggressive);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Dog);
        }

        public bool Equals(Dog dog)
        {
            return base.Equals(dog);
        }

        public override int GetHashCode()
        {
            return ChipId.GetHashCode() ^ Name.GetHashCode();
        }

        public static bool operator ==(Dog lhs, Dog rhs)
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

        public static bool operator !=(Dog lhs, Dog rhs)
        {
            return !(lhs == rhs);
        }
        public static bool operator <=(Dog lhs, Dog rhs)
        {
            return (lhs.ChipId <= rhs.ChipId);
        }

        public static bool operator >=(Dog lhs, Dog rhs)
        {
            return (lhs.ChipId >= rhs.ChipId);
        }

    }
}
