using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace MultiplayerTicTacToe.Model
{
    public class SignalRHub : Hub //Hub Class of SignalR library
    {
        private static string player1Id = "";
        private static string player2Id = "";

        //Overwritten to handle new connections
        //Checks if player slot is empty, if yes, new connected is assigned
        public override async Task OnConnectedAsync()
        {
            //For hardcoded IP: Check the connection ID of incoming connection against hardcoded list of connected IDs
            if (player1Id == "")
            {
                player1Id = Context.ConnectionId;
                await Clients.Client(player1Id).SendAsync("Playernumber", 1);
            }
            else if (player2Id == "")
            {
                player2Id = Context.ConnectionId;
                await Clients.Client(player2Id).SendAsync("PlayerNumber", 2);
                await Clients.All.SendAsync("GameStarted");
            }
            else
            {
                await Clients.Caller.SendAsync("GameFull");
            }
            await base.OnConnectedAsync();
        }

        //Incoming move messages from the different clients, updates the played move to the other player
        //Then broadcasts the moved play to all clients except the one that made the move
        public async Task PlayMove(int row, int col, char player)
        {
            await Clients.Others.SendAsync("MovePlayed",row,col,player);
        }
    }
}
