using Newtonsoft.Json;
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
            var jsonSer = new JsonSerializer() { TypeNameHandling = Config.TypeNameHandling, PreserveReferencesHandling = Config.PreserveReferencesHandling };
            var jsonSer2 = new JsonSerializer() { TypeNameHandling = Config.TypeNameHandling, PreserveReferencesHandling = Config.PreserveReferencesHandling };
            var jobject = JToken.FromObject(object1, jsonSer);
            var jobject2 = JToken.FromObject(object2, jsonSer2);
            var ret = (JObjectComparisonResult)new JTokenComparer(new IndexArrayKeySelector()).CompareTokens(Config.CompareTokenKey, jobject, jobject2);
            results.Result = ret.ComparisonResult;
            results.DifferencesComparison = ret.AsStringList(true);
            results.PropertyComparison = ret.AsStringList();
            return results;
        }
    }
}
