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

        public TicTacToe()
        {
            ResetBoard();
            _currentPlayer = 'X';
            _currentState = GameState.NotStarted;
            _highscores.Add(("Player 1", 0));
            _highscores.Add(("Player 2", 0));

        }

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

        public void StartGame()
        {
            ResetBoard();
            _currentPlayer = 'X';
            _currentState = GameState.InProgress;
        }

        public void PlayMove(int row, int column)
        {
            if(_currentState != GameState.InProgress)
            {
                return;
            }

            if(row < 0 || row >= 3 || column < 0 || column >= 3)
            {
                throw new ArgumentException("Invalid Move: Out of bounds");
            }

            if (_board[row,column] != CellState.Empty)
            {
                return;
            }

            
            _board[row, column] = _currentPlayer == 'X' ? CellState.X : CellState.O;
            CheckForWinner();
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
