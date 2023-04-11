namespace MultiplayerTicTacToe;
using MultiplayerTicTacToe.View;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();
		

		Routing.RegisterRoute(nameof(PlayerPage),typeof(PlayerPage));
	}
}
