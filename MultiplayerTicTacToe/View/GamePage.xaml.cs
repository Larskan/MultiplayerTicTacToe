using MultiplayerTicTacToe.ViewModel;
namespace MultiplayerTicTacToe.View;

public partial class GamePage : ContentPage
{
    private string playerName;

    public GamePage(GamePageVM vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}

    
}