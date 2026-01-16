using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Avalonia.Routing.Navigation;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Avalonia.Routing.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    ////Навигационное дерево(далее роутер)////
    
    public LeftScreen LeftRouter { get; } //независимый роутер для левой части
    public RightScreen RightRouter { get; } //независимый роутер для правой части

    //команды навигации по левому роутеру
    public ReactiveCommand<Unit, IRoutableViewModel> LeftGoNextCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> LeftGoBackCommand => LeftRouter.Router.NavigateBack;

    //команды навигации по правому роутеру
    public ReactiveCommand<Unit, IRoutableViewModel> RightGoNextCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> RightGoBackCommand => RightRouter.Router.NavigateBack;

    //DI контейнер, используемый для получения моделей представления.
    private readonly IServiceProvider _serviceProvider;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        //указываем к какому Screen относится роутеры
        LeftRouter = _serviceProvider.GetRequiredService<LeftScreen>();
        RightRouter = _serviceProvider.GetRequiredService<RightScreen>();

        //выполнение начальной навигации после ожидания инициализации всех моделей представления
        RxApp.MainThreadScheduler.Schedule(() =>
        {
            LeftRouter.Router.Navigate.Execute(Get<InstrumentsFirstViewModel>());
            RightRouter.Router.Navigate.Execute(Get<PanelsFirstViewModel>());
        });

        //CanExecute для левого роутера
        var leftCanGoNext = LeftRouter.Router.CurrentViewModel.Select(LeftCanGoNext);
        //реактивная команда для переключения view на следующую для левого роутера
        LeftGoNextCommand = ReactiveCommand.CreateFromObservable(() =>
        {
            var currentVm = LeftRouter.Router.GetCurrentViewModel();
            return LeftRouter_CreateNext(currentVm);
        }, leftCanGoNext);

        //CanExecute для правого роутера
        var rightCanGoNext = RightRouter.Router.CurrentViewModel.Select(RightCanGoNext);
        //реактивная команда для переключения view на следующую для левого роутера
        RightGoNextCommand = ReactiveCommand.CreateFromObservable(() =>
        {
            var currentVm = RightRouter.Router.GetCurrentViewModel();
            return RightRouter_CreateNext(currentVm);
        }, rightCanGoNext);
    }

    /// <summary>
    /// Упрощённый доступ к сервисам из DI контейнера.
    /// Используется для получения ViewModel при навигации.
    /// </summary>
    /// <typeparam name="T">На вход должна поступать ViewModel для получения ее из DI контейнеров</typeparam>
    /// <returns>Возвращаем экземпляр модели представления из DI контейнера</returns>
    private T Get<T>() where T : notnull => _serviceProvider.GetRequiredService<T>();

    /// <summary>
    /// Выбор следующего View относительно полученной модели представления для левого роутера
    /// </summary>
    /// <param name="currentVm">Текущая модель представления</param>
    /// <returns>View для переключения на следующую</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private IObservable<IRoutableViewModel> LeftRouter_CreateNext(IRoutableViewModel? currentVm)
    {
        switch (currentVm)
        {
            case InstrumentsFirstViewModel:
                return LeftRouter.Router.Navigate.Execute(Get<InstrumentsSecondViewModel>());
            default:
                throw new InvalidOperationException("GoNext executed when it should be disabled");
        }
    }
    
    /// <summary>
    /// Выбор следующего View относительно полученной модели представления для правого роутера
    /// </summary>
    /// <param name="currentVm">Текущая модель представления</param>
    /// <returns>View для переключения на следующую</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private IObservable<IRoutableViewModel> RightRouter_CreateNext(IRoutableViewModel? currentVm)
    {
        switch (currentVm)
        {
            case PanelsFirstViewModel:
                return RightRouter.Router.Navigate.Execute(Get<PanelsSecondViewModel>());
            default:
                throw new InvalidOperationException("GoNext executed when it should be disabled");
        }
    }
    
    //функции для проверки последней модели представления, для CanExecute(условие доступности)
    private bool LeftCanGoNext(IRoutableViewModel? current) => current is InstrumentsFirstViewModel;
    private bool RightCanGoNext(IRoutableViewModel? current) => current is PanelsFirstViewModel;
}