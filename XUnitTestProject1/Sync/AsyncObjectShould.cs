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
        private AsyncObject _dut;

        /// <summary>
        /// Initialize the test suite
        /// </summary>
        public AsyncObjectShould()
        {
            _dut = new AsyncObject();
        }

        /// <summary>
        /// Test an void async with no arguments
        /// </summary>
        [Fact]
        public void CallVoidAsyncNoArgs()
        {
            _dut.VoidAsyncNoArgs();

            Assert.NotNull(_dut.StringsList.FirstOrDefault(s => s.Contains(VoidAsyncNoArgs)));
        }

        /// <summary>
        /// Test a void returning async method that runs without exception.
        /// </summary>
        [Fact]
        public void CorrectlyPassingTest()
        {
            _dut.SimpleAsync();
        }

        /// <summary>
        /// Test a void returning async method that fails
        /// </summary>
        [Fact]
        public void InCorrectlyPassingTest()
        {
            _dut.FailAsync();
        }

        /// <summary>
        /// This test will fail as expected because of the exception thrown
        /// by the async method
        /// </summary>
        /// <returns></returns>
        [Fact(Skip = "This test is designed to fail. It is a good example of why an async method should always return a value")]
        public async Task CorrectlyFailingTest()
        {
            await _dut.FailAsync();
        }

        /// <summary>
        /// This test is an example of how to verify an exception is
        /// thrown from an async operation
        /// </summary>
        [Fact]
        public async void ExampleThrowsExceptionTest()
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async ()
              => { await _dut.FailAsync(); });
        }
    }
}
