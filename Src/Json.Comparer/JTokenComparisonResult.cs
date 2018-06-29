using System.Collections.Generic;

namespace Json.Comparer
{
    /// <summary>
    /// The result of a comparison.
    /// </summary>
    public class JTokenComparisonResult : ICompareResult
    {
        public string Key { get; set; }

        public string Path { get; set; }

        /// <summary>
        /// The type of JToken compared
        /// </summary>
        public virtual ComparedTokenType Type { get; }

        /// <summary>
        /// The result of the comparison. Different if the token or any of the child elements are different.
        /// </summary>
        public ComparisonResult ComparisonResult { get; set; }

        /// <summary>
        /// Gets the difference count.
        /// </summary>
        /// <value>
        /// The difference count.
        /// </value>
        public virtual int DifferenceCount {
            get
            {
                return ((this.ComparisonResult == ComparisonResult.Identical) ? 0 : 1 );
            }
        }

        /// <summary>
        /// Returns Comparison Result as a friendly string
        /// </summary>
        /// <returns></returns>
        public virtual List<string> AsStringList()
        {
            return AsStringList(false);
        }
        public virtual List<string> AsStringList(bool onlyDifferences)
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
}