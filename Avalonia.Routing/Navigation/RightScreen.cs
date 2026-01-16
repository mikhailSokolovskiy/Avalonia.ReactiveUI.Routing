using ReactiveUI;

namespace Avalonia.Routing.Navigation;

//Контекст для навигации левого роутера, для независимой навигации
public class RightScreen : IScreen
{
    public RoutingState Router { get; } = new();
}