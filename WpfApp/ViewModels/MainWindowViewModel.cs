
namespace WpfApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ChildControlViewModel Child { get; }

        public MainWindowViewModel( ChildControlViewModel child )
        {
            Child = child;
        }

        protected override async Task OnInitializeAsync( CancellationToken cancellationToken )
        {
            await base.OnInitializeAsync( cancellationToken );
            await Child.InitializeAsync( cancellationToken );
        }
    }
}