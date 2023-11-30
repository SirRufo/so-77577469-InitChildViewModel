using System.Collections.Generic;

namespace WpfApp.Services
{
    public class FakeSearchService : ISearchService
    {
        public async Task<IEnumerable<string>> SearchAsync( string arg, CancellationToken cancellationToken = default )
        {
            await Task.Delay( 250, cancellationToken ).ConfigureAwait( false );
            return new[] { arg, arg, arg, arg, arg };
        }
    }
}
