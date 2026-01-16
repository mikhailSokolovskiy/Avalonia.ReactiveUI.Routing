using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Routing.Navigation;
using ReactiveUI;

namespace Avalonia.Routing.ViewModels;

public class PanelsSecondViewModel : ViewModelBase, IRoutableViewModel
{
    //Экран к которому привязан правый роутер
    public IScreen HostScreen { get; }
    
    //не нужно
    public string? UrlPathSegment { get; }

    private bool _isCheck1;

    public bool IsCheck1
    {
        get => _isCheck1;
        set => this.RaiseAndSetIfChanged(ref _isCheck1, value);
    }

    private bool _isCheck2;

    public bool IsCheck2
    {
        get => _isCheck2;
        set => this.RaiseAndSetIfChanged(ref _isCheck2, value);
    }

    public List<string> TextList { get; } =
    [
        "123123",
        "asdasd",
        "1232sdsad2323",
        "asdasdasd"
    ];

    /// <summary>
    /// Получаем правый навигационный контекст (RightScreen),
    /// чтобы этот ViewModel мог участвовать в навигации левой панели.
    /// </summary>
    public PanelsSecondViewModel(RightScreen screen)
    {
        HostScreen = screen;
    }
}