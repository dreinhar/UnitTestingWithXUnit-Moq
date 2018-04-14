using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XUnitTestApp.Sync
{
    /// <summary>
    /// Class that has async and non async methods
    /// </summary>
    public class AsyncObject
    {
        private List<string> strings = new List<string>();

        /// <summary>
        /// All methods write text to this list. It is 
        /// used by the test harness and to print to 
        /// stdout the order in which statments are made
        /// </summary>
        public IList<string> StringsList => strings;
        public ICollection<string> StringsCollection => strings;
        public IEnumerable<string> StringsEnumerable => strings;
        public Queue<string> StringsQueue => new Queue<string>(strings);

        private int Id = 0;
        private int PreWaitId = 0;
        private int WaitId = 0;
        private int PostWaitId = 0;
        private int CallId = 0;

        private void WriteVoidAsyncNoArgsMessageOne()
        {
            strings.Add($"{Id++}) Pre Wait VoidAsyncNoArgs run: CallId: {CallId} PrintId: {PreWaitId++}");
        }

        private void WriteVoidAsyncNoArgsMessageTwo()
        {
            strings.Add($"{Id++}) After wait VoidAsyncNoArgs run: CallId: {CallId} PrintId: {PostWaitId++}");
        }

        private void WaitForWriteVoidAsyncNoArgsMessage()
        {
            strings.Add($"{Id++}) Waited for VoidAsyncNoArgs run: CallId: {CallId} PrintId: {WaitId++}");
        }

        /// <summary>
        /// An async function that takes no arguments and returns void
        /// </summary>
        public async void VoidAsyncNoArgs()
        {
            CallId++;
            WriteVoidAsyncNoArgsMessageOne();
            await Task.Run(() => WaitForWriteVoidAsyncNoArgsMessage());
            WriteVoidAsyncNoArgsMessageTwo();
        }
    }

    public sealed class SystemUnderTest
    {
        public static async Task SimpleAsync()
        {
            await Task.Delay(10);
            throw new Exception("Should Fail.");
        }
    }
}
