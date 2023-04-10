using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace MultiplayerTicTacToe.Model
{
    public class Client
    {
        //Creates HubConnection using URL of SignalR hub on server
        private HubConnection connection;
        private int playerNumber;

        public Client()
        {
            connection = new HubConnectionBuilder().WithUrl("http://<Insert my server IP>/SignalRHub").Build();
            //Event handler for PlayerNumber that is sent from server
            connection.On<int>("PlayerNumber", (number) =>
            {
                playerNumber = number;
            });

            //Event handler for GameStarted sent from server
            connection.On("GameStarted", () =>
            {
                //TODO: Start game
            });

            //Event handler for GameFull sent from server
            connection.On("GameFull", () =>
            {
                //Display message that game is full
            });

            //Event handler for MovePlayed sent from server
            connection.On<int, int, char>("MovePlayed", (row, col, player) =>
            {
                //update game _board with new move
            });

        }

        /// <summary>
        /// Send message to server saying client wants to connect to player 1
        /// 
        /// </summary>
        /// <returns>Assigns the connection ID of client to player 1 if player slot is empty</returns>
        public async Task ConnectToPlayer1()
        {
            await connection.StartAsync();
            await connection.SendAsync("ConnectToPlayer1");
        }

        /// <summary>
        /// Send message to server saying client has made a move
        /// Handled by SignalRHub Class that broadcasts the move to all clients except the one that made the move
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public async Task PlayMove(int row, int col, char player)
        {
            await connection.SendAsync("PlayMove",row,col, player);
        }
    }
}
