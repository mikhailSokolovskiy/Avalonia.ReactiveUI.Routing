using System;
using ReactiveUI;

namespace Avalonia.Routing;

public class RoutingViewLocator : IViewLocator
{
    /// <summary>
    /// Универсальный ViewLocator для ReactiveUI.
    /// Он просто получает ViewModel и пытается создать View с таким же именем, заменяя "ViewModel" на "View".
    /// !!!ВАЖНО!!!
    /// Такой подход позволяет использовать один ViewLocator для любого количества независимых Router.
    /// </summary>
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        if (viewModel == null) return null;
        var vmType = viewModel.GetType();
        var viewTypeName = vmType.FullName!.Replace("ViewModel", "View");
        var viewType = Type.GetType(viewTypeName);
        return viewType != null ? (IViewFor?)Activator.CreateInstance(viewType) : null;
    }
}