using System.Collections.Generic;
using System.Text;

namespace Json.Comparer
{
    /// <summary>
    /// ComparisonResult for JArrayTokens
    /// </summary>
    public class JArrayComparisonResult : JTokenComparisonResult, ICompareResult
    {
        /// <summary>
        /// The type of JToken compared
        /// </summary>
        public override ComparedTokenType Type { get; } = ComparedTokenType.Array;

        /// <summary>
        /// The comparisonResults for all elements in the array.
        /// </summary>
        public IEnumerable<JTokenComparisonResult> ArrayElementComparisons { get; set; } = new List<JTokenComparisonResult>();

        /// <summary>
        /// Returns this object as a list of friendly property comparison
        /// </summary>
        /// <returns></returns>
        public override List<string> AsStringList()
        {
            return AsStringList(false);
        }
        public override List<string> AsStringList(bool onlyDifferences)
        {
            var lst = new List<string>();
            foreach (var item in ArrayElementComparisons)
            {
                lst.AddRange(item.AsStringList(onlyDifferences));
            }
            return lst;
        }

        /// <summary>
        /// Gets the difference count.
        /// </summary>
        /// <value>
        /// The difference count.
        /// </value>
        public override int DifferenceCount
        {
            get
            {
                var differenceCount = 0;
                foreach (var item in ArrayElementComparisons)
                    differenceCount += item.DifferenceCount;
                return differenceCount;
            }
        }
    }
}