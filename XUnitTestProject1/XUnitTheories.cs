﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestSuite
{
    /// <summary>
    /// Simple complex test object 
    /// </summary>
    public class TestObject
    {
        /// <summary>
        /// Complex object value
        /// </summary>
        public int Property_1 { get; set; }
    };

    public class XUnitInLineDataTheories
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
    }

    public class XUnitClassDataTheories
    {
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

        private class ClassDataComplexTypeTestGenerator : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var obj = new TestObject { Property_1 = 1 };
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

    public class XUnitMemberDataTheories
    {
        private class MemberDataTestGenerator
        {
            public static IEnumerable<object[]> GetBasicTypeEnumerator()
            {
                yield return new object[] { 5, 1, 6 };
                yield return new object[] { 7, 2, 9 };
            }

            public static IEnumerable<object[]> GetComplexTypeEnumerator()
            {
                var obj = new TestObject { Property_1 = 1 };
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
        };


        public static IEnumerable<object[]> GetBasicTypeEnumeratorFromMethod()
        {
            yield return new object[] { 5, 1, 6 };
            yield return new object[] { 7, 2, 9 };
        }

        /// <summary>
        /// Constant and basic type paramaterized test
        /// </summary>
        /// <param name="value_1">first constant</param>
        /// <param name="value_2">second constant</param>
        /// <param name="sum">expected result</param>
        [Theory]
        [MemberData(nameof(GetBasicTypeEnumeratorFromMethod))]
        public void InjectConstantsTheory(int value_1, int value_2, int sum)
        {
            Assert.Equal(sum, value_1 + value_2);
        }

        /// <summary>
        /// Constant and basic type paramaterized test using generator class
        /// </summary>
        /// <param name="value_1">first constant</param>
        /// <param name="value_2">second constant</param>
        /// <param name="sum">expected result</param>
        [Theory]
        [MemberData(nameof(MemberDataTestGenerator.GetBasicTypeEnumerator), MemberType = typeof(MemberDataTestGenerator))]
        public void InjectBasicTypeConstantWithTestGenerator(int value_1, int value_2, int sum)
        {
            Assert.Equal(sum, value_1 + value_2);
        }

        /// <summary>
        /// Complex type paramaterized test using generator class
        /// </summary>
        /// <param name="result">result object</param>
        /// <param name="expected">expected object</param>
        [Theory]
        [MemberData(nameof(MemberDataTestGenerator.GetComplexTypeEnumerator), MemberType = typeof(MemberDataTestGenerator))]
        public void InjectComplexTypeConstantWithTestGenerator(TestObject result, TestObject expected)
        {
            Assert.Equal(expected, result);
        }
    }
}
