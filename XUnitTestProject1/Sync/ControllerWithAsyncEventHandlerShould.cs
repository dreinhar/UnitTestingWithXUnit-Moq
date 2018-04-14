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
        /// Verify that a controller's async event handler is called by a 
        /// view's event 
        /// </summary>
        [Fact]
        public void CallAsyncEventHandler()
        {
            var view = new ViewObjectWithEventHandler();
            var controller = new ControllerWithAsyncEventHandler(view);

            view.RunEvent();

            Assert.Equal(0, controller.EventCalled);
        }

        /// <summary>
        /// Verify that a controller's async event handler is called by a 
        /// view's event twice
        /// </summary>
        [Fact]
        public void CallAsyncEventHandlerTwice()
        {
            var view = new ViewObjectWithEventHandler();
            var controller = new ControllerWithAsyncEventHandler(view);

            view.RunEvent();
            view.RunEvent();

            Assert.Equal(2, controller.EventCalled);
        }
    }
}
