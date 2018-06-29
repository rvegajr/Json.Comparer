using System.Dynamic;
using System.Linq;
using Json.Comparer.ValueConverters;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

namespace Json.Comparer.Tests
{
    public class ComplexTestObject : ComplexTestItem
    {
        public List<ComplexTestItem> ListofComplexItems { get; set; } = new List<ComplexTestItem>();
        public Dictionary<string, ComplexTestItem> DictionaryofComplexItems { get; set; } = new Dictionary<string, ComplexTestItem>();
        public ComplexTestObject()
        {
            this.ListofComplexItems.Add(new ComplexTestItem() { StringValue = "STR1", DecimalValue = 4.3M, IntValue = 99 });
            this.ListofComplexItems.Add(new ComplexTestItem() { StringValue = "STR2", DecimalValue = 2.6M, IntValue = 14 });
            this.DictionaryofComplexItems.Add("Key 1", new ComplexTestItem() { StringValue = "STR3", DecimalValue = 1213.31M, IntValue = 31231 });
            this.DictionaryofComplexItems.Add("Key 2", new ComplexTestItem() { StringValue = "STR4", DecimalValue = 963.992M, IntValue = 913 });
        }
    }
    public class ComplexTestItem
    {
        public int IntValue { get; set; } = 2;
        public string StringValue { get; set; } = "VALUE";
        public decimal DecimalValue { get; set; } = 1.2M;
    }
    public class ComplexTestItem2 : ComplexTestItem
    {
        public DateTime DateValue { get; set; } = DateTime.Now;
    }
    public class CompareDifferentStructuresTests
    {
        [Fact]
        public void Compare2EqualObjectsShouldHaveDifference()
        {
            dynamic simpleObject1 = new ExpandoObject();
            simpleObject1.AAA = "";
            dynamic simpleObject2 = new ExpandoObject();
            simpleObject2.BBB = "";
            var compareResult = new JTokenComparer(new IndexArrayKeySelector(), Enumerable.Empty<IComparisonFilter>(), new ComparisonResult[] { ComparisonResult.MissingInSource2 }, new NonConvertingConverter()).Compare(simpleObject1, simpleObject2);
            Assert.True(((JObjectComparisonResult)compareResult).ComparisonResult.Equals(ComparisonResult.Different), "The JObjects have different key names");
        }

        [Fact]
        public void Compare2EqualObjectsShouldNotHaveDifference()
        {
            dynamic simpleObject1 = new ExpandoObject();
            simpleObject1.AAA = "";
            dynamic simpleObject2 = new ExpandoObject();
            simpleObject2.AAA = "";
            var compareResult = new JTokenComparer(new IndexArrayKeySelector(), Enumerable.Empty<IComparisonFilter>(), new ComparisonResult[] { ComparisonResult.MissingInSource2 }, new NonConvertingConverter()).Compare(simpleObject1, simpleObject2);
            Assert.True(((JObjectComparisonResult)compareResult).ComparisonResult.Equals(ComparisonResult.Identical), "The JObjects have the same key names");
        }

        [Fact]
        public void CompareLogicCompareObjectsSame()
        {
            ComparisonResults res = (new CompareLogic()).Compare(new ComplexTestObject(), new ComplexTestObject());
            Assert.True(res.DifferencesCount.Equals(0), "The JObjects should not have any differences and the count should be zero");
            Assert.True(!res.AreEqual, "The JObjects have different key names");
        }

        [Fact]
        public void CompareLogicCompareObjectsDifferent()
        {
            var o1 = new ComplexTestObject();
            var o2 = new ComplexTestObject();
            o2.ListofComplexItems.RemoveAt(1);
            o2.ListofComplexItems.Add(new ComplexTestItem2());

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResults res = compareLogic.Compare(o1, o2);
            Assert.True(!res.AreEqual, "The JObjects are different");
        }
    }
}