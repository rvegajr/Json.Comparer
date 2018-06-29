using NullGuard;
using System.Collections.Generic;

namespace Json.Comparer
{
    /// <summary>
    /// The result of a comparison of JToken.
    /// </summary>
    public class JValueComparisonResult : JTokenComparisonResult, ICompareResult
    {
        /// <summary>
        /// The type of JToken compared
        /// </summary>
        public override ComparedTokenType Type { get; } = ComparedTokenType.Value;

        /// <summary>
        /// The value from source1 used for the comparison
        /// </summary>
        [AllowNull]
        public string Source1Value { get; set; }

        /// <summary>
        /// The value from source2 used for the comparison
        /// </summary>
        [AllowNull]
        public string Source2Value { get; set; }

        /// <summary>
        /// Ases the string.
        /// </summary>
        /// <returns></returns>
        public override List<string> AsStringList()
        {
            return AsStringList(false);
        }
        /// <summary>
        /// Ases the string.
        /// </summary>
        /// <param name="onlyDifferences">if set to <c>true</c> [only differences].</param>
        /// <returns></returns>
        public override List<string> AsStringList(bool onlyDifferences)
        {
            var lst = new List<string>();

            //no need to return anything if this is identical and we only want to see differences
            if ((this.ComparisonResult == ComparisonResult.Identical) && (onlyDifferences)) return lst;

            if (this.ComparisonResult == ComparisonResult.Identical)
                lst.Add(string.Format("{0} didn't change [{1}].", this.Path, this.Source1Value));
            else if (this.ComparisonResult == ComparisonResult.Different)
                lst.Add(string.Format("{0} changed from [{1}] to [{2}].", this.Path, this.Source1Value, this.Source2Value));
            else if (this.ComparisonResult == ComparisonResult.Filtered)
                lst.Add(string.Format("{0} is filtered, value is [{1}].", this.Path, this.Source1Value));
            else if (this.ComparisonResult == ComparisonResult.MissingInSource1)
                lst.Add(string.Format("{0} is missing in source 1, value in source 2 is [{1}].", this.Path, this.Source2Value));
            else if (this.ComparisonResult == ComparisonResult.MissingInSource2)
                lst.Add(string.Format("{0} is missing in source 2, value in source 1 is [{1}].", this.Path, this.Source1Value));
            else
                lst.Add(string.Format("{0} result is {1}... source 1=[{2}], source 2=[{3}].", this.Path, this.ComparisonResult.ToString(), this.Source1Value, this.Source2Value));
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
                return ((this.ComparisonResult == ComparisonResult.Identical) ? 0 : 1);
            }
        }

    }

}