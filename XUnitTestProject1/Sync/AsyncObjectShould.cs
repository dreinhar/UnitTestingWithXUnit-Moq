using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTestApp.Sync;
using System.Linq;
using System.Threading.Tasks;

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

        [Fact]
        public void InCorrectlyPassingTest()
        {
            SystemUnderTest.SimpleAsync();
        }

        [Fact(Skip = "This test is designed to fail. It is a good example of why an async method should always return a value")]
        public async Task CorrectlyFailingTest()
        {
            await SystemUnderTest.SimpleAsync();
        }

        [Fact]
        public void ExampleThrowsExceptionTest()
        {
            Assert.ThrowsAsync<Exception>(async ()
              => { await SystemUnderTest.SimpleAsync(); });
        }
    }
}
