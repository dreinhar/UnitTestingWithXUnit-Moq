using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestSuite
{
    public class XUnitTheories
    {
        /// <summary>
        /// Constant and basic type paramaterized test
        /// </summary>
        /// <param name="value_1">first constant</param>
        /// <param name="value_2">second constant</param>
        /// <param name="sum">expected result</param>
        [Theory]
        [InlineData(1,2,3)]
        [InlineData(11,22,33)]
        public void InjectConstantsTheory(int value_1, int value_2, int sum)
        {
            Assert.Equal(sum, value_1 + value_2);
        }
    }
}
