using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Json.Comparer
{
    public class ComparisonConfig
    {
        public string CompareTokenKey { get; set; } = "root";
        public TypeNameHandling TypeNameHandling { get; set; } = TypeNameHandling.None;
        public PreserveReferencesHandling PreserveReferencesHandling { get; set; } = PreserveReferencesHandling.None;
    }
}
