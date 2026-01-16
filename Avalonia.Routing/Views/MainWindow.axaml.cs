using Avalonia.ReactiveUI;
using Avalonia.Routing.ViewModels;

namespace Avalonia.Routing.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}