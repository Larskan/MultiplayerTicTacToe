using MultiplayerTicTacToe.View;

namespace MultiplayerTicTacToe;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();
		Routing.RegisterRoute(nameof(PlayerPage),typeof(PlayerPage));
	}
}
