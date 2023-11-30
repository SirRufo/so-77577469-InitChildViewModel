namespace WpfApp.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        [Reactive] public bool IsBusy { get; protected set; }
        [Reactive] public bool IsInitialized { get; private set; }
        public async Task InitializeAsync( CancellationToken cancellationToken = default )
        {
            IsInitialized = false;
            await OnInitializeAsync( cancellationToken );
            IsInitialized = true;
        }

        protected virtual Task OnInitializeAsync( CancellationToken cancellationToken ) => Task.CompletedTask;
    }

}