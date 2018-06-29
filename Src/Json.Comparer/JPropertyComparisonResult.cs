using NullGuard;
using System.Collections.Generic;

namespace Json.Comparer
{
    /// <summary>
    /// The result of a JToken comparison.
    /// </summary>
    public class JPropertyComparisonResult : JTokenComparisonResult, ICompareResult
    {
        /// <summary>
        /// The type of JToken compared
        /// </summary>
        public override ComparedTokenType Type { get; } = ComparedTokenType.Property;

        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The comparisonresult of the value of the property.
        /// </summary>
        [AllowNull]
        public JTokenComparisonResult PropertyValueComparisonResult { get; set; }

        /// <summary>
        /// Returns this object as friendly string
        /// </summary>
        /// <returns></returns>
        public override List<string> AsStringList()
        {
            return AsStringList(false);
        }
        /// <summary>
        /// Returns this as the friendly formatted astring.
        /// </summary>
        /// <param name="onlyDifferences">if set to <c>true</c> [only differences].</param>
        /// <returns></returns>
        public override List<string> AsStringList(bool onlyDifferences)
        {
            if (PropertyValueComparisonResult != null)
            {
                return PropertyValueComparisonResult.AsStringList(onlyDifferences);
            }
            else
            {
                var lst = new List<string>();
                //no need to return anything if this is identical and we only want to see differences
                if ((this.ComparisonResult == ComparisonResult.Identical) && (onlyDifferences)) return lst;
                if (this.ComparisonResult == ComparisonResult.Identical)
                    lst.Add(string.Format("{0} didn't change.", this.Path));
                else if (this.ComparisonResult == ComparisonResult.Different)
                    lst.Add(string.Format("{0} changed.", this.Path));
                else if (this.ComparisonResult == ComparisonResult.Filtered)
                    lst.Add(string.Format("{0} is filtered.", this.Path));
                else if (this.ComparisonResult == ComparisonResult.MissingInSource1)
                    lst.Add(string.Format("{0} is missing in source 1.", this.Path));
                else if (this.ComparisonResult == ComparisonResult.MissingInSource2)
                    lst.Add(string.Format("{0} is missing in source 2.", this.Path));
                else
                    lst.Add(string.Format("{0} result is {1}... .", this.Path, this.ComparisonResult.ToString()));
                return lst;
            }
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
                return PropertyValueComparisonResult.DifferenceCount;
            }
        }
    }
}