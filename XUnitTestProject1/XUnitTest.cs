using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using Moq;
using System.Linq;

namespace XUnitTestSuite
{
    public class Fixture
    {
        public void Method()
        {
            Assert.True(true);
        }
    }

    public class XUnitTestShould : IClassFixture<Fixture>, IDisposable
    {
        private readonly ITestOutputHelper output;

        public XUnitTestShould(Fixture fixture, ITestOutputHelper testOutputHelper)
        {
            Assert.True(true);
            fixture.Method();
            output = testOutputHelper;
            output.WriteLine("Constructing XUnitTest object");
        }

        public void Dispose()
        {
            Assert.True(true);
            output.WriteLine("Destroying XUnitTest object");
        }

        [Fact]
        public void BeTrue()
        {
            Assert.True(true);
            output.WriteLine("Running ShouldBeTrue test");
        }

        [Fact]
        public void Befalse()
        {
            Assert.False(false);
            output.WriteLine("Running ShouldBeFalse test");
        }

        [Fact]
        public void NotContainHi()
        {
            Assert.DoesNotContain("Hi", "Hello");
        }

        [Fact]
        public void NotContaing()
        {
            Assert.DoesNotContain("g", "Hello");
        }

        [Fact]
        public void NotContainp()
        {
            Assert.DoesNotContain("p", "Hello");
        }

        [Fact]
        public void ContainEmptyString()
        {
            Assert.Contains("", " ");
        }

        [Fact]
        public void NotContainSpace()
        {
            Assert.DoesNotContain(" ", "A");
        }

        [Fact]
        public void NotContainValue()
        {
            List<int> list = new List<int>() { 5, 6, 7 };
            Assert.DoesNotContain<int>(1, list);
        }

        [Fact]
        public void NotContainList()
        {
            List<List<int>> list = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6} };
            List<int> expected = new List<int>() { 5, 6, 7 };
            Assert.DoesNotContain<List<int>>(expected, list);
        }

        [Fact]
        public void Containll()
        {
            Assert.Contains("ll", "Hello");
        }

        [Fact]
        public void ContainValue()
        {
            List<int> list = new List<int>() { 5, 6, 7 };
            Assert.Contains<int>(5, list);
        }

        [Fact]
        public void ContainList()
        {
            List<List<int>> list = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 } };
            List<int> expected = new List<int>() { 4, 5, 6 };
            Assert.Contains<List<int>>(expected, list);
        }

        [Fact]
        public void BeEmpty()
        {
            Assert.Empty(new List<int>());
        }

        [Fact]
        public void NotBeEmpty()
        {
            Assert.NotEmpty(new List<int>() { 1 });
        }

        [Fact]
        public void EqualString()
        {
            Assert.Equal("A", "A");
        }

        [Fact]
        public void EqualChar()
        {
            Assert.Equal('A', 'A');
        }

        [Fact]
        public void EqualInt()
        {
            Assert.Equal(1, 1);
        }
    
        [Fact]
        public void EqualLong()
        {
            Assert.Equal(1L, 1L);
        }

        [Fact]
        public void EqualDoubleToPrecision3()
        {
            Assert.Equal(1.11111, 1.11112, 3);
        }

        [Fact]
        public void NotEqualDoubleToPrecision6()
        {
            Assert.NotEqual(1.11111, 1.11112, 6);
        }

        
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
//        [InlineData(6)]
        public void CheckValueIsOdd(int value)
        {
            Assert.True(IsOdd(value));
        }

        private bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        [Fact]
        public void ThrowNotImplementedException()
        {
            Exception ex = Assert.Throws<NotImplementedException>(() => NotImplementedException());
            output.WriteLine(ex.Message);
            Assert.Equal("The method or operation is not implemented.", ex.Message);
        }

        private void NotImplementedException()
        {
            throw new NotImplementedException();
        }

        private event EventHandler<EventArgs> OnEvent;
        private void EventHandlerMethod(object sender, EventArgs e) { }

        [Fact]
        public void AssertEventHandlerIsNotSet()
        {
            Assert.Null(OnEvent);
        }

        [Fact]
        public void AssertEventHandlerIsSet()
        {
            OnEvent += EventHandlerMethod;

            var eventHandler = OnEvent.GetInvocationList()[0];
            Assert.Equal("EventHandlerMethod", eventHandler.Method.Name);
        }

        // https://xunit.github.io/#documentation
        // Other asserts
        // InRange
        // IsNotType and IsNotType
        // IsType
        // NotInRange
        // NotNull
        // NotSame
        // Null
        // Same
        // Throws and Throws

    }
}
