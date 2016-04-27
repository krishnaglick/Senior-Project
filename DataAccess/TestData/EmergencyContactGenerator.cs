
using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.TestData
{
    public class EmergencyContactGenerator : List<EmergencyContact>
    {
        public EmergencyContactGenerator(Random random)
        {
            var png = new PhoneNumberGenerator(random);
            for (int i = 0; i < random.Next(1, 5); i++)
            {
                this.Add(
                    new EmergencyContact
                    {
                        FirstName = Patrons.Names[random.Next(0, Patrons.Names.Length)],
                        LastName = Patrons.Names[random.Next(0, Patrons.Names.Length)],
                        PhoneNumber = png.GetRandomPhoneNumber()
                    }
                );
            }
        }
    }
}
