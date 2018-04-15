using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockTestSuite
{
    /// <summary>
    /// Contains all the interfaces and classes needed to verify Moq
    /// Basic Moq tests
    /// </summary>
    public class MockShould
    {
        /// <summary>
        /// Custom event argument object for testing
        /// </summary>
        public class TestEventArgs : EventArgs { }

        /// <summary>
        /// Interfaces mocked in all tests
        /// </summary>
        public interface TestMock
        {
            /// <summary>
            /// Associated class to mock
            /// </summary>
            TestClass Class { get; set; }

            /// <summary>
            /// Property mocked during testing
            /// </summary>
            int TestProperty { get; set; }

            /// <summary>
            /// Function mocked during testing
            /// </summary>
            /// <returns>an integer</returns>
            int TestReturn();

            /// <summary>
            /// Event mocked during testing
            /// </summary>
            event EventHandler<TestEventArgs> OnEvent;
        }

        /// <summary>
        /// Class associated with the interface during testing
        /// </summary>
        public class TestClass
        {
            /// <summary>
            /// Class associated with the base class for testing hierarchy mocking
            /// </summary>
            public virtual TestAssociatedClass AssociatedClass { get; set; }

            /// <summary>
            /// Used for testing hierarchy mocking of functions
            /// </summary>
            /// <returns>false</returns>
            public virtual bool Submit() { return false; }

            /// <summary>
            /// Used for testing mocking of calling functions with arguments
            /// on concrete objects
            /// </summary>
            /// <param name="value">The test argument</param>
            /// <returns></returns>
            public virtual bool Submit(bool value) { return value; }
        }

        /// <summary>
        /// An associated class of a class for hierarchy mocking
        /// </summary>
        public class TestAssociatedClass
        {
            /// <summary>
            /// Used for testing hierarchy mocking of properties
            /// </summary>
            /// <returns>an integer</returns>
            public virtual int TestProperty { get; set; }
        }

        /// <summary>
        /// Test that moq will set a functions return value
        /// </summary>
        [Fact]
        public void SetFunctionToReturn5()
        {
            var mock = new Mock<TestMock>();
            mock.Setup(testMock => testMock.TestReturn())
                .Returns(5);

            Assert.Equal(5, mock.Object.TestReturn());
        }

        /// <summary>
        /// Test that moq will set a properties return value
        /// </summary>
        [Fact]
        public void SetPropertyToReturn5()
        {
            var mock = new Mock<TestMock>();
            mock.Setup(testMock => testMock.TestProperty).Returns(5);

            Assert.Equal(5, mock.Object.TestProperty);
        }

        /// <summary>
        /// Test that moq will set the return value of a base object's property
        /// </summary>
        [Fact]
        public void SetAssociatedClassPropertyToReturn5()
        {
            var mock = new Mock<TestMock>();
            mock.Setup(testMock => testMock.Class.AssociatedClass.TestProperty).Returns(5);

            Assert.Equal(5, mock.Object.Class.AssociatedClass.TestProperty);
        }

        /// <summary>
        /// Test that Moq will initialize a properties value
        /// </summary>
        [Fact]
        public void InitializePropertyTo5()
        {
            var mock = new Mock<TestMock>();
            mock.SetupProperty(testMock => testMock.TestProperty, 5);

            TestMock _mock = mock.Object;

            Assert.Equal(5, _mock.TestProperty);
        }

        /// <summary>
        /// Test that Moq will count number of method calls
        /// with any argument values passed
        /// </summary>
        [Fact]
        public void CountMethodCalledWithAnyArguments()
        {
            var mock = new Mock<TestClass>();
            mock.Setup(testMock => testMock.Submit());

            mock.Object.Submit(true);
            mock.Object.Submit(false);

            mock.Verify(testMock => testMock.Submit(It.IsAny<bool>()), Times.Exactly(2));
        }

        /// <summary>
        /// Test that Moq will count number of method calls
        /// with only specific argument values passed
        /// </summary>
        [Fact]
        public void CountMethodCalledWithSpecificArguments()
        {
            var mock = new Mock<TestClass>();
            mock.Setup(testMock => testMock.Submit());

            mock.Object.Submit(true);
            mock.Object.Submit(false);

            mock.Verify(testMock => testMock.Submit(false), Times.Exactly(1));
        }

        private bool eventRaised = false;
        private void EventHandlerMethod(object sender, TestEventArgs e) { eventRaised = true; }

        [Fact]
        public void MockEventHandler()
        {
            var mock = new Mock<TestMock>();

            mock.Object.OnEvent += EventHandlerMethod;
            mock.Raise(x => x.OnEvent += null, new TestEventArgs());

            Assert.True(eventRaised);
        }
    }
}
