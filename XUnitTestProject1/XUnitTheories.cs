using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestSuite
{
    public class XUnitTheories
    {
        /// <summary>
        /// Constant and basic type paramaterized test
        /// </summary>
        /// <param name="value_1">first constant</param>
        /// <param name="value_2">second constant</param>
        /// <param name="sum">expected result</param>
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(11, 22, 33)]
        public void InjectConstantsTheory(int value_1, int value_2, int sum)
        {
            Assert.Equal(sum, value_1 + value_2);
        }

        private class ClassDataBasicTypeTestGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {

                new object[] {5, 1, 6},

                new object[] {7, 1, 8}

            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        };

        /// <summary>
        /// Constant and basic type paramaterized test using generator class
        /// </summary>
        /// <param name="value_1">first constant</param>
        /// <param name="value_2">second constant</param>
        /// <param name="sum">expected result</param>
        [Theory]
        [ClassData(typeof(ClassDataBasicTypeTestGenerator))]
        public void InjectBasicTypeConstantWithTestGenerator(int value_1, int value_2, int sum)
        {
            Assert.Equal(sum, value_1 + value_2);
        }

        public class TestObject
        {
            public int Property_1 { get; set; }
            public int Property_2 { get; set; }
        };

        private class ClassDataComplexTypeTestGenerator : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var obj = new TestObject { Property_1 = 1, Property_2 = 2 };
                yield return new object[]
                {
                    obj,
                    obj,
                };

                yield return new object[]
                {
                    obj,
                    obj,
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        };

        /// <summary>
        /// Complex type paramaterized test using generator class
        /// </summary>
        /// <param name="result">result object</param>
        /// <param name="expected">expected object</param>
        [Theory]
        [ClassData(typeof(ClassDataComplexTypeTestGenerator))]
        public void InjectComplexTypeConstantWithTestGenerator(TestObject result, TestObject expected)
        {
            Assert.Equal(expected, result);
        }
    }
}
