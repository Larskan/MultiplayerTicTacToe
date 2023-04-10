using MultiplayerTicTacToe.ViewModel;

namespace MultiplayerTicTacToe.View;

public partial class PlayerPage : ContentPage
{
	public PlayerPage(PlayerPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}