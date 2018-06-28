using System.Dynamic;
using System.Linq;
using Json.Comparer.ValueConverters;
using Xunit;
using FluentAssertions;
namespace Json.Comparer.Tests
{
    public class CompareDifferentStructuresTests
    {
        [Fact]
        public void Compare2EqualObjectsShouldHaveDifference()
        {
            dynamic simpleObject1 = new ExpandoObject();
            simpleObject1.AAA = "";
            dynamic simpleObject2 = new ExpandoObject();
            simpleObject2.BBB = "";
            var compareResult = new JTokenComparer(new IndexArrayKeySelector(), Enumerable.Empty<IComparrisonFilter>(), new ComparisonResult[] { ComparisonResult.MissingInSource2 }, new NonConvertingConverter()).Compare(simpleObject1, simpleObject2);
            Assert.True(((JObjectComparrisonResult)compareResult).ComparrisonResult.Equals(ComparisonResult.Different), "The JObjects have different key names");
        }

        [Fact]
        public void Compare2EqualObjectsShouldNotHaveDifference()
        {
            dynamic simpleObject1 = new ExpandoObject();
            simpleObject1.AAA = "";
            dynamic simpleObject2 = new ExpandoObject();
            simpleObject2.AAA = "";
            var compareResult = new JTokenComparer(new IndexArrayKeySelector(), Enumerable.Empty<IComparrisonFilter>(), new ComparisonResult[] { ComparisonResult.MissingInSource2 }, new NonConvertingConverter()).Compare(simpleObject1, simpleObject2);
            Assert.True(((JObjectComparrisonResult)compareResult).ComparrisonResult.Equals(ComparisonResult.Identical), "The JObjects have the same key names");
        }
    }
}