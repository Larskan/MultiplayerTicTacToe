using MultiplayerTicTacToe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerTicTacToe.ViewModel
{
    public class HiscorePageVM
    {
        public ObservableCollection<Hiscore> Hiscores { get; }
        public HiscorePageVM()
        {
            //Initialize the collection with some dummy data
            Hiscores = new ObservableCollection<Hiscore>
            {
                new Hiscore {PlayerName = "Lars", Score = 10},
                new Hiscore {PlayerName = "Bob", Score = 5},
                new Hiscore {PlayerName = "Frank", Score = 2}
            };
        }
    }
}
