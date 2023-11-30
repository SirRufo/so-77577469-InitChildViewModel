
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class ChildControlViewModel : ViewModelBase
    {
        private readonly ObservableAsPropertyHelper<bool> _isAvailable;
        private readonly ObservableAsPropertyHelper<IEnumerable<string>?> _searchResults;
        private readonly ISearchService _searchService;

        public bool IsAvailable => _isAvailable.Value;
        public IEnumerable<string>? SearchResults => _searchResults.Value;
        [Reactive] public string? SearchTerm { get; set; }

        public ChildControlViewModel( ISearchService searchService )
        {
            _searchResults = this
                .WhenAnyValue( x => x.SearchTerm )
                .Throttle( TimeSpan.FromMilliseconds( 800 ) )
                .Select( term => term?.Trim() )
                .DistinctUntilChanged()
                .SelectMany( SearchData )
                .ObserveOn( RxApp.MainThreadScheduler )
                .ToProperty( this, x => x.SearchResults );
            _searchResults.ThrownExceptions.Subscribe( error => { /* Handle errors here */ } );
            _isAvailable = this
                .WhenAnyValue( x => x.SearchResults )
                .Select( searchResults => searchResults != null )
                .ToProperty( this, x => x.IsAvailable );
            _searchService = searchService;
        }

        protected override Task OnInitializeAsync( CancellationToken cancellationToken )
        {
            // do we have somthing that needs to be initialized?
            // f.i. preload some lists
            return base.OnInitializeAsync( cancellationToken );
        }

        private async Task<IEnumerable<string>?> SearchData( string? arg, CancellationToken cancellationToken )
        {
            IsBusy = true;
            try
            {
                if ( string.IsNullOrWhiteSpace( arg ) ) return null;
                return await _searchService.SearchAsync( arg, cancellationToken );
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

}