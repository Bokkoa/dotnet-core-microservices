using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace ShopServices.Api.Book.Tests
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {

        // adding another constructor
        public AsyncEnumerable(IEnumerable<T> enumerable): base(enumerable){}
        // adding a constructor
        public AsyncEnumerable(Expression expression):base(expression){}
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider {
            get { return new AsyncQueryProvider<T>(this);}
        }
    }
}