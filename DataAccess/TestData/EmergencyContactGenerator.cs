using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.TestData
{
    public class EmergencyContactGenerator : List<EmergencyContact>
    {
        public EmergencyContactGenerator(Random random)
        {
            for (int i = 0; i < random.Next(1, 5); i++)
            {
                this.Add(
                    new EmergencyContact
                    {
                        FirstName = Patrons.Names[random.Next(0, Patrons.Names.Length)],
                        LastName = Patrons.Names[random.Next(0, Patrons.Names.Length)],
                        PhoneNumber = new PhoneNumberGenerator(random)[0].ToString()
                    }
                );
            }
        }
    }
}
