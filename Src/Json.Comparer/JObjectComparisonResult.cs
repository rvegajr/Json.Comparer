using System.Collections.Generic;
using System.Text;

namespace Json.Comparer
{
    /// <summary>
    /// A ComparisonResult for JObjectToken elements.
    /// </summary>
    public class JObjectComparisonResult : JTokenComparisonResult, ICompareResult
    {
        /// <summary>
        /// The type of JToken compared
        /// </summary>
        public override ComparedTokenType Type { get; } = ComparedTokenType.Object;

        /// <summary>
        /// The comparisonResults for all properties in the object.
        /// </summary>
        public IEnumerable<JTokenComparisonResult> PropertyComparisons { get; set; } = new List<JTokenComparisonResult>();

        /// <summary>
        /// Returns this object as a list of friendly property comparison
        /// </summary>
        /// <returns></returns>
        public override List<string> AsStringList()
        {
            return AsStringList(false);
        }
        /// <summary>
        /// returns as a friendly friendly formatted string
        /// </summary>
        /// <param name="onlyDifferences">if set to <c>true</c> [only differences].</param>
        /// <returns></returns>
        public override List<string> AsStringList(bool onlyDifferences)
        {
            var lst = new List<string>(); 
            foreach (var item in PropertyComparisons)
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
                foreach (var item in PropertyComparisons)
                    differenceCount += item.DifferenceCount;
                return differenceCount;
            }
        }
    }
}