using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XUnitTestApp.Sync
{
    /// <summary>
    /// A view object that has an event handler
    /// </summary>
    public class ViewObjectWithEventHandler
    {
        /// <summary>
        /// Event for mapping actions to the event
        /// </summary>
        public event EventHandler<EventArgs> OnEvent = (sender, e) => { };

        /// <summary>
        /// Method used to allow unity to run the OnClicked event
        /// </summary>
        public void RunEvent()
        {
            OnEvent(this, new EventArgs());
        }
    }
}
