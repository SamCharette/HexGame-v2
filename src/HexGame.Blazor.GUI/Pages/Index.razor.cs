﻿using System.Collections.Generic;
using Engine;
using Engine.Players;
using Engine.ValueTypes;

namespace HexGame.Blazor.GUI.Pages
{
    public partial class Index
    {
        public List<Move> Moves { get; set; } = new List<Move>();
        public PlayerNumber Winner { get; private set; } = PlayerNumber.Unowned;

        public void StartGame()
        {
            var boardSize = 11;

            var player1 = PlayerBuilder
                .New()
                .OfType("RandomPlayer")
                .AsPlayerOne()
                .ForBoardSize(boardSize)
                .WithConfiguration(null)
                .Build();

            var player2 = PlayerBuilder
                .New()
                .OfType("RandomPlayer")
                .AsPlayerTwo()
                .ForBoardSize(boardSize)
                .WithConfiguration(null)
                .Build();

            var game = new Game(boardSize, player1, player2);

            game.StartGame();

            Moves = game.Moves;
            Winner = game.Winner;
        }
    }
}
