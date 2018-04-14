using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitTestApp.Sync;

namespace XUnitTestSuite.Sync
{
    /// <summary>
    /// Verify behavior of a view object event handler
    /// </summary>
    public class ViewObjectWithEventHandlerShould
    {
        private bool _eventCalled = false;

        /// <summary>
        /// Verify that the RunEvent method is connected
        /// to run a handler
        /// </summary>
        [Fact]
        public void CallEventHandler()
        {
            var view = new ViewObjectWithEventHandler();
            view.OnEvent += HandleEvent;

            view.RunEvent();

            Assert.True(_eventCalled);
        }

        private void HandleEvent(object sender, EventArgs e)
        {
            _eventCalled = true;
        }
    }
}
