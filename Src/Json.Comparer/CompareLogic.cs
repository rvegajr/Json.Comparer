using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Json.Comparer
{
    /// <summary>
    /// A helper class that makes it easy to compare 2 different objects of any type
    /// </summary>
    public class CompareLogic
    {
        public ComparisonConfig Config { get; set; } = new ComparisonConfig();
        public ComparisonResults Compare<S, T>(S object1, T object2)
        {
            var results = new ComparisonResults();
            var jobject = JToken.FromObject(object1);
            var jobject2 = JToken.FromObject(object2);
            var ret = (JObjectComparisonResult)new JTokenComparer(new IndexArrayKeySelector()).CompareTokens(Config.CompareTokenKey, jobject, jobject2);
            results.Result = ret.ComparisonResult;
            results.DifferencesComparison = ret.AsStringList(true);
            results.PropertyComparison = ret.AsStringList();
            return results;
        }
    }
}
