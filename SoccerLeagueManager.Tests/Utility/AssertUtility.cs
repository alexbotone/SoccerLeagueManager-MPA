using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueManager.Tests.Utility
{
    class AssertUtility
    {

        //extension method for copy object with new instance creation
        public static void AssertAreEqual(object expected, object actual)
        {
            Type expectedType = expected.GetType();
            Type actualType = actual.GetType();
            foreach (PropertyInfo actualProperty in actualType.GetProperties())
            {
                PropertyInfo expectedProperty = expectedType.GetProperty(actualProperty.Name);
                if (expectedProperty != null)
                    Assert.AreEqual(expectedProperty.GetValue(expected), actualProperty.GetValue(actual));
            }
        }
    }
}
