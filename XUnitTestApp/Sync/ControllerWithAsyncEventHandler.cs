using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace XUnitTestApp.Sync
{
    /// <summary>
    /// The controller for a model 
    /// </summary>
    public class ControllerWithAsyncEventHandler
    {
        /// <summary>
        /// Field to allow the sensing of the event handler
        /// run state
        /// </summary>
        public int EventCalled { get; set; }
        private ViewObjectWithEventHandler view;

        /// <summary>
        /// Construct the obect and tie the view's event
        /// to the controller's handler
        /// </summary>
        /// <param name="view"></param>
        public ControllerWithAsyncEventHandler(ViewObjectWithEventHandler view)
        {
            this.view = view;

            view.OnEvent += HandleEvent;
        }

        /// <summary>
        /// Non async event handler that is run when the event is triggered.
        /// This is necessary because events by there nature are void returning concepts
        /// that you normally would not want to block the main thread on.
        /// </summary>
        /// <param name="sender">the object that triggered the event</param>
        /// <param name="e">the event args</param>
        private void HandleEvent(object sender, EventArgs e)
        {
            HandleEventAsync(sender, e);
        }

        /// <summary>
        /// The async event handler that is called by the non async event handler.
        /// This function is very difficult to unit test effectively but necessary
        /// because some events require that their thread be perform async activities.
        /// </summary>
        /// <param name="sender">the object that fired the event</param>
        /// <param name="e">the event args</param>
        private async void HandleEventAsync(object sender, EventArgs e)
        {
            await EventThreadAsync();
        }

        /// <summary>
        /// The actual async method that is used to perform the work.
        /// This function can be effectively unit tested.
        /// </summary>
        /// <returns>a Task</returns>
        private async Task<int> EventThreadAsync()
        {
            return await Task.Run(() => EventCalled++);
        }
    }
}
