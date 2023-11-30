using System.Collections.Generic;

namespace WpfApp.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<string>> SearchAsync( string arg, CancellationToken cancellationToken = default );
    }
}
