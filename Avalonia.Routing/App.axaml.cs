using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Routing.Navigation;
using Avalonia.Routing.ViewModels;
using Avalonia.Routing.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Avalonia.Routing;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var servicesCollection = new ServiceCollection();

        // регистрируем основную VM
        servicesCollection.AddSingleton<MainWindowViewModel>();

        // два независимых IScreen для двух роутеров
        // Каждый IScreen содержит свой собственный RoutingState
        // что позволяет иметь два полностью независимых дерева навигации.
        servicesCollection.AddSingleton<LeftScreen>();
        servicesCollection.AddSingleton<RightScreen>();

        //регистрируем VMs для левого роутера
        servicesCollection.AddSingleton<InstrumentsFirstViewModel>();
        servicesCollection.AddSingleton<InstrumentsSecondViewModel>();

        //регистрируем VMs для правого роутера
        servicesCollection.AddSingleton<PanelsFirstViewModel>();
        servicesCollection.AddSingleton<PanelsSecondViewModel>();

        // Создаём DI контейнер.
        var serviceProvider = servicesCollection.BuildServiceProvider();

        var vm = serviceProvider.GetRequiredService<MainWindowViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}