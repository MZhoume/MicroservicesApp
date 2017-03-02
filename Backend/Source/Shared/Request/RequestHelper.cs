namespace Shared.Request
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Helper class for handling request
    /// </summary>
    public static class RequestHelper
    {
        private static Dictionary<SearchOperator, string> operatorMapping = new Dictionary<SearchOperator, string>()
        {
            [SearchOperator.EQ] = " = ",
            [SearchOperator.GT] = " > ",
            [SearchOperator.GE] = " >= ",
            [SearchOperator.LT] = " < ",
            [SearchOperator.LE] = " <= ",
            [SearchOperator.NE] = " != "
        };

        /// <summary>
        /// Compose the expression used in where method from the search terms
        /// </summary>
        /// <param name="searchTerms"> Search Terms from the request </param>
        /// <returns> Composed expression </returns>
        public static string ComposeWhereExp(params SearchTerm[] searchTerms)
        {
            if (searchTerms.Length == 0)
            {
                return null;
            }

            StringBuilder searchStr = new StringBuilder();
            for (int i = 0; i < searchTerms.Length; i++)
            {
                if (i > 0)
                {
                    searchStr.Append(" AND ");
                }

                var term = searchTerms[i];
                if (term.Operator == SearchOperator.LIKE)
                {
                    searchStr.Append(term.Field + ".Contains(\"" + term.Value + "\")");
                }
                else
                {
                    decimal val;
                    if (decimal.TryParse(term.Value, out val))
                    {
                        searchStr.Append(term.Field + operatorMapping[term.Operator] + val);
                    }
                    else
                    {
                        searchStr.Append(term.Field + operatorMapping[term.Operator] + "DateTime.Parse(\"" + term.Value + "\")");
                    }
                }
            }

            return searchStr.ToString();
        }
    }
}