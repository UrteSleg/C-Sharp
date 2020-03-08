using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_savarankiska
{
    abstract class AnimalMarked : Animal
    {
        public int ChipId { get; set; }
        //public string Name { get; set; }
        //public string Owner { get; set; }
        public AnimalMarked(string name, string breed, string owner, string phone, DateTime vaccinationDate, int chipId)
            : base(name, breed, owner, phone, vaccinationDate)
        {
            ChipId = chipId;
            //Name = name;
            //Owner = owner;

        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as AnimalMarked);
        }

        public bool Equals(AnimalMarked animal)
        {
            if (Object.ReferenceEquals(animal, null))
            {
                return false;
            }

            if (this.GetType() != animal.GetType())
            {
                return false;
            }

            return (Name == animal.Name);
        }
        /*
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(AnimalMarked lhs, AnimalMarked rhs)
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

        public static bool operator !=(AnimalMarked lhs, AnimalMarked rhs)
        {
            return !(lhs == rhs);
        }
        */

        public static bool operator <=(AnimalMarked lhs, AnimalMarked rhs)
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
        public static bool operator >=(AnimalMarked lhs, AnimalMarked rhs)
        {
            return !(lhs <= rhs);
        }
    }
}
