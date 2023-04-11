using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerTicTacToe.Model
{
    //Enums to make it look cleaner and more organised
    public enum CellState { Empty, X, O }
    public enum GameState { NotStarted, InProgress, Tie, XWins, OWins }

    public class TicTacToe
    {
        private CellState[,] _board = new CellState[3, 3];
        private char _currentPlayer;
        private GameState _currentState;
        private List<(string, int)> _highscores = new List<(string, int)> ();

        /// <summary>
        /// For calling the entire Class from other classes, it is default
        /// </summary>
        public TicTacToe()
        {
            ResetBoard();
            _currentPlayer = 'X';
            _currentState = GameState.NotStarted;
            _highscores.Add(("Player 1", 0));
            _highscores.Add(("Player 2", 0));

        }

        /// <summary>
        /// Sets all cells to be empty
        /// Iterates through all rows and columns and makes them empty
        /// </summary>
        public void ResetBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    _board[row,column] = CellState.Empty;
                }
            }
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            //If there is for some reason random cells filled, it resets the entire thing first
            ResetBoard();
            _currentPlayer = 'X'; //X goes first, because I say so
            _currentState = GameState.InProgress; //Update GameState so it is in progress
        }

        /// <summary>
        /// The gameplay itself
        /// </summary>
        /// <param name="row">Grid</param>
        /// <param name="column">Grid</param>
        /// <exception cref="ArgumentException">Error handling</exception>
        public void PlayMove(int row, int column)
        {
            //Checks if game has stopped for some reason
            if(_currentState != GameState.InProgress)
            {
                return;
            }

            //If the O and X for some reason are outside the board
            if(row < 0 || row >= 3 || column < 0 || column >= 3)
            {
                throw new ArgumentException("Invalid Move: Out of bounds");
            }

            //Checks if the cell you want to place your X in already has something in it
            if (_board[row,column] != CellState.Empty)
            {
                return;
            }

            //If none of the errors happen: Updates game board by setting the cell at specified row/col to X or O depending on turn
            //Whose turn is tracked with the _currentPlayer
            _board[row, column] = _currentPlayer == 'X' ? CellState.X : CellState.O;
            //If someone wins, _currentState will be updated to show XWin, OWin or Tie.
            CheckForWinner();
            //Switch Players changes _currentPlayer to the other people, assuming no one has won yet
            SwitchPlayers();
            
        }

        private void SwitchPlayers()
        {
            _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';
        }

        private void CheckForWinner()
        {
            // Check rows for winner
            for (int row = 0; row < 3; row++)
            {
                if (_board[row, 0] != CellState.Empty && _board[row, 0] == _board[row, 1] && _board[row, 1] == _board[row, 2])
                {
                    _currentState = _board[row, 0] == CellState.X ? GameState.XWins : GameState.OWins;
                    UpdateHighScores(_currentState);
                    return;
                }
            }

            // Check columns for winner
            for (int column = 0; column < 3; column++)
            {
                if (_board[0, column] != CellState.Empty && _board[0, column] == _board[1, column] && _board[1, column] == _board[2, column])
                {
                    _currentState = _board[0, column] == CellState.X ? GameState.XWins : GameState.OWins;
                    UpdateHighScores(_currentState);
                    return;
                }
            }

            // Check diagonals for winner

            if (_board[1, 1] != CellState.Empty && ((_board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2]) 
                ||
               (_board[2, 0] == _board[1, 1] && _board[1, 1] == _board[0, 2])))
            {
                _currentState = _board[1, 1] == CellState.X ? GameState.XWins : GameState.OWins;
                UpdateHighScores(_currentState);
                return;
            }



            //Check for tie
            if (IsBoardFull())
            {
                _currentState = GameState.Tie;
                UpdateHighScores(_currentState);
                return;
            }

            //Game still in progress
            _currentState = GameState.InProgress;

        }
        private void UpdateHighScores(GameState winner)
        {
            if(winner == GameState.XWins)
            {
                _highscores[0] = (_highscores[0].Item1, _highscores[0].Item2+1);
            }
            else if(winner == GameState.OWins)
            {
                _highscores[1] = (_highscores[1].Item1, _highscores[1].Item2 + 1);
            }
        }

        private bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (_board[row, column] == CellState.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public CellState[,] GetBoardState()
        {
            return (CellState[,])_board.Clone();
        }

        public char GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public GameState GetCurrentState()
        {
            return _currentState;
        }

        public List<(string, int)> GetHighScores()
        {
            return _highscores;
        }

        
    }
}
