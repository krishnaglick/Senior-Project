﻿
using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.TestData
{
    public class PhoneNumberGenerator : List<PhoneNumber>
    {
        public char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public PhoneNumberGenerator(Random random)
        {
            for (int i = 0; i < random.Next(1, 4); i++)
            {
                this.Add(
                    new PhoneNumber
                    {
                        phoneNumber = new string(numbers[random.Next(0, 10)], 9)
                    }
                );
            }
        }
    }
}
