
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility.Enum;
using Utility.ExtensionMethod;

namespace Tests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void EnumsTest()
        {
            var ethnicity = EthnicityID.Asian;
            var ethnicityViewModel = EnumViewModel.ParseEnum(ethnicity);
            Assert.IsTrue(ethnicityViewModel.id == 7);
            Assert.IsTrue(ethnicityViewModel.name == "Asian");
            Assert.IsTrue(ethnicityViewModel.description == "Asian");
        }

        [TestClass]
        public class ExtensionTests
        {
            [TestMethod]
            public void ArrayTests()
            {
                int[] ints = new[] {0, 1, 2, 3};
                var intList = ints.ToList();
                var expectedResult = new List<int> {0, 1, 2, 3};
                for (int i = 0; i < expectedResult.Count; i++)
                {
                    Assert.AreEqual(intList[i], expectedResult[i]);
                }
            }
        } 
    }
}
