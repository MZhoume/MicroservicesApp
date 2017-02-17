namespace Static.Request
{
    /// <summary>
    /// SearchTerm class contains essential parts used in a search operation
    /// </summary>
    public class SearchTerm
    {
        /// <summary>
        /// Gets the field to be search from
        /// </summary>
        /// <returns> The field to search </returns>
        public string Field { get; set; }

        /// <summary>
        /// Gets the operator used in the search term
        /// </summary>
        /// <returns> The search operator </returns>
        public SearchOperator Operator { get; set; }

        /// <summary>
        /// Gets the value used for such search
        /// </summary>
        /// <returns> The search value </returns>
        public string Value { get; set; }
    }
}