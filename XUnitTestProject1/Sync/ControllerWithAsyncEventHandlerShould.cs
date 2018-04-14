using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using XUnitTestApp.Sync;

namespace XUnitTestSuite.Sync
{
    /// <summary>
    /// Verify an MVC controller with an async event handler's behavior
    /// </summary>
    public class ControllerWithAsyncEventHandlerShould
    {
        /// <summary>
        /// This test demonstrates that an async event can't be 
        /// tested effectively
        /// </summary>
        [Fact(Skip = "This test is fails because the assert is performed before the thread sets the EventCalled property")]
        public void CallAsyncEventHandler()
        {
            var view = new ViewObjectWithEventHandler();
            var controller = new ControllerWithAsyncEventHandler(view);

            view.RunEvent();

            Assert.Equal(1, controller.EventCalled);
        }
    }
}
