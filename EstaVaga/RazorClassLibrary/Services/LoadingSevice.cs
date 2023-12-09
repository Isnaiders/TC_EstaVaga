public class LoadingService
{
    public event Action<bool> OnLoadingChanged;

    private bool isLoading = false;

    public bool IsLoading => isLoading;

    public void SetLoading(bool loading)
    {
        isLoading = loading;
        OnLoadingChanged?.Invoke(isLoading);
    }
}
