using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MultiplayerTicTacToe.Model;


namespace MultiplayerTicTacToe.ViewModel
{
    public class GamePageVM : INotifyPropertyChanged
    {
        private static GamePageVM instance;
        private TicTacToe game;

        public event PropertyChangedEventHandler PropertyChanged;

        public static GamePageVM Instance
        {
            get
            {
                instance ??= new GamePageVM();
                return instance;
            }
        }

        //Stores the value of Board property
        private CellState[,] _board;
        public CellState[,] Board
        {
            get { return (CellState[,])_board.Clone(); }
            set { _board = value; OnPropertyChanged(); }
        }

        public string CurrentPlayerMessage
        {
            get { return $"Current player: {game.GetCurrentPlayer()}"; }
        }

        public ICommand PlayMoveCommand { get; private set; }

        private GamePageVM()
        {
            game = new TicTacToe();
            PlayMoveCommand = new Command<string>(PlayMove);
        }

        private void PlayMove(string coordinates)
        {
            int x = int.Parse(coordinates.Split(',')[0]);
            int y = int.Parse(coordinates.Split(',')[1]);
            game.PlayMove(x, y);
            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(CurrentPlayerMessage));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
