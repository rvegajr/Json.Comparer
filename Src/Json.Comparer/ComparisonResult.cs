using System.Collections.Generic;

namespace Json.Comparer
{
    /// <summary>
    /// 
    /// </summary>
    public class ComparisonResults
    {
        public ComparisonResults()
        {
        }
        public ComparisonResult Result { get; set; } = ComparisonResult.Unknown;
        public bool AreEqual
        {
            get
            {
                return this.Result == ComparisonResult.Identical;
            }
        }
        public List<string> PropertyComparison { get; set; } = new List<string>();
        public List<string> DifferencesComparison { get; set; } = new List<string>();

        public string DifferencesString
        {
            get
            {
                return string.Join("\n", DifferencesComparison.ToArray());
            }
        }

        public int DifferencesCount
        {
            get
            {
                return DifferencesComparison.Count;
            }
        }
    }
    /// <summary>
    /// The result of a comparison.
    /// </summary>
    public enum ComparisonResult
    {
        /// <summary>
        /// The values contained in this token and any child tokens are identical
        /// </summary>
        Identical,

        /// <summary>
        /// The value in this token or values in child elements are different, or missing.
        /// </summary>
        Different,

        /// <summary>
        /// The element is missing in source1.
        /// </summary>
        MissingInSource1,

        /// <summary>
        /// The element is missing in source2.
        /// </summary>
        MissingInSource2,

        /// <summary>
        /// The element is of a different type in source and target. This indicates there is an incorrect comparison.
        /// </summary>
        DifferentTypes,

        /// <summary>
        /// The tokens comparison was skipped and filtered out.
        /// </summary>
        Filtered,
        /// <summary>
        /// Not evaluated yet
        /// </summary>
        Unknown

    }
}