using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTestApp.Sync;
using System.Linq;

namespace XUnitTestSuite.Sync
{
    /// <summary>
    /// Verify AsyncObject behaviors
    /// </summary>
    public class AsyncObjectShould
    {
        private const string VoidAsyncNoArgs = "VoidAsyncNoArgs";

        /// <summary>
        /// Test an void async with no arguments
        /// </summary>
        [Fact]
        public void CallVoidAsyncNoArgs()
        {
            var _dut = new AsyncObject();

            _dut.VoidAsyncNoArgs();

            Assert.NotNull(_dut.StringsList.FirstOrDefault(s => s.Contains(VoidAsyncNoArgs)));
        }
    }
}
