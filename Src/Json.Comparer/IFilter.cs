using Newtonsoft.Json.Linq;
using NullGuard;
using System.Collections.Generic;

namespace Json.Comparer
{
    public interface IComparisonFilter
    {
        bool ShouldBeFiltered(string key, JToken token1, JToken token2);
    }

    public interface ICompareResult
    {
        List<string> AsStringList();
        List<string> AsStringList(bool onlyDifferences);
        int DifferenceCount { get; }
    }

}