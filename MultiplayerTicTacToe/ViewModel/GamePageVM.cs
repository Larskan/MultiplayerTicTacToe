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
        //Implements Singleton pattern, making sure only 1 instance is created
        private static GamePageVM instance;
        //Model for the game
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

        /// <summary>
        /// Returns a message telling which players turn it is
        /// </summary>
        public string CurrentPlayerMessage
        {
            get { return $"Current player: {game.GetCurrentPlayer()}"; }
        }

        /// <summary>
        /// Handles input from View
        /// 
        /// </summary>
        public ICommand PlayMoveCommand { get; private set; }

        private GamePageVM()
        {
            game = new TicTacToe();
            PlayMoveCommand = new Command<string>(PlayMove);
        }

        //Represents the coordinates for the cell being used like "0,1"
        private void PlayMove(string coordinates)
        {
            //Parses coordinates of cell
            int x = int.Parse(coordinates.Split(',')[0]);
            int y = int.Parse(coordinates.Split(',')[1]);
            //Registers the cell for the game
            game.PlayMove(x, y);
            //Update view
            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(CurrentPlayerMessage));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
