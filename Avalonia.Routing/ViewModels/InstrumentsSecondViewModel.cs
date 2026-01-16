using System;
using Avalonia.Routing.Navigation;
using ReactiveUI;

namespace Avalonia.Routing.ViewModels;

public class InstrumentsSecondViewModel : ViewModelBase, IRoutableViewModel
{
    //Экран к которому привязан левый роутер
    public IScreen HostScreen { get; }

    //не нужно
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    private int _incValue = 1;

    public int IncValue
    {
        get => _incValue;
        set => this.RaiseAndSetIfChanged(ref _incValue, value);
    }

    private bool _isToggleCheck1;

    public bool IsToggleCheck1
    {
        get => _isToggleCheck1;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck1, value);
    }

    private bool _isToggleCheck2;

    public bool IsToggleCheck2
    {
        get => _isToggleCheck2;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck2, value);
    }

    private bool _isToggleCheck3;

    public bool IsToggleCheck3
    {
        get => _isToggleCheck3;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck3, value);
    }

    private bool _isToggleCheck4;

    public bool IsToggleCheck4
    {
        get => _isToggleCheck4;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck4, value);
    }

    private bool _isToggleCheck5;

    public bool IsToggleCheck5
    {
        get => _isToggleCheck5;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck5, value);
    }

    private bool _isToggleCheck6;

    public bool IsToggleCheck6
    {
        get => _isToggleCheck6;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck6, value);
    }

    private bool _isToggleCheck7;

    public bool IsToggleCheck7
    {
        get => _isToggleCheck7;
        set => this.RaiseAndSetIfChanged(ref _isToggleCheck7, value);
    }

    /// <summary>
    /// Получаем левый навигационный контекст (LeftScreen),
    /// чтобы этот ViewModel мог участвовать в навигации левой панели.
    /// </summary>
    public InstrumentsSecondViewModel(LeftScreen screen)
    {
        HostScreen = screen;
    }
}