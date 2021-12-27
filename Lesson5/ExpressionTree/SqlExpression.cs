
using System.Linq.Expressions;

namespace Lesson5.ExpressionTree
{
    public class SqlExpression
    {
        public string SqlRequest(Func<IEnumerable<Customer>, IEnumerable<Customer>> enumerable)
        {
            return "sql";
        }
    }
}
