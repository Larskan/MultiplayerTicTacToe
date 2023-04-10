using MultiplayerTicTacToe.ViewModel;

namespace MultiplayerTicTacToe.View;

public partial class HiScorePage : ContentPage
{
	public HiScorePage(HiscorePageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}