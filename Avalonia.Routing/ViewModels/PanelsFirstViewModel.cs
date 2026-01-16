using Avalonia.Routing.Navigation;
using ReactiveUI;

namespace Avalonia.Routing.ViewModels;

public class PanelsFirstViewModel: ViewModelBase, IRoutableViewModel
{
    //Экран к которому привязан правый роутер
    public IScreen HostScreen { get; }
    
    //не нужно
    public string? UrlPathSegment { get; }

    /// <summary>
    /// Получаем правый навигационный контекст (RightScreen),
    /// чтобы этот ViewModel мог участвовать в навигации левой панели.
    /// </summary>
    public PanelsFirstViewModel(RightScreen screen)
    {
        HostScreen = screen;
    }
}