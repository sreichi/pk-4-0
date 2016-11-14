using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Reichinger.Masterarbeit.PK_4_0.Test
{
    public class Class1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2,2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}