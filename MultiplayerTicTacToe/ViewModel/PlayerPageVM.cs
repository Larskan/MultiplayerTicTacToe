using MultiplayerTicTacToe.View;
using System.ComponentModel;
using System.Windows.Input;
using PropertyChangingEventArgs = System.ComponentModel.PropertyChangingEventArgs;
using PropertyChangingEventHandler = System.ComponentModel.PropertyChangingEventHandler;

namespace MultiplayerTicTacToe.ViewModel
{
    public class PlayerPageVM : INotifyPropertyChanging
    {
        public event PropertyChangingEventHandler PropertyChanging;


        //Stores Player name
        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {    
                _playerName = value;
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(PlayerName)));
            }
        }

        
        //Executes registerAction
        public ICommand StartGameCommand { get; }
        public PlayerPageVM()
        {
            StartGameCommand = new Command(OnStartGame);
        }

        private void OnStartGame()
        {
            //Navigate to the game page with selected player name
            if(!string.IsNullOrEmpty(PlayerName))
            {
                //Constructor Injection
                Application.Current.MainPage.Navigation.PushAsync(new GamePage(PlayerName));
            }
        }
    }
}
