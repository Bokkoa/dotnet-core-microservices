using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopServices.Api.Book.Tests
{
    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;
        public T Current => enumerator.Current;

        public AsyncEnumerator(IEnumerator<T> enumerator ) => this.enumerator = enumerator ?? throw new ArgumentNullException();

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await  Task.FromResult(enumerator.MoveNext());
        }
    }
}