using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace Webapi.nUnit
{
    public class UnitTest
    {
        [Test]
        public void PassingTest()
        {
            Assert.AreEqual(4, Add(2, 2));
        }

        private int Add(int x, int y)
        {
            return x + y;
        }
    }
}
