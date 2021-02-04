﻿using System;
using System.Linq;
using Engine.ValueTypes;
using Engine;

namespace Engine.Players
{
    public class RandomPlayer : IPlayer
    {
        private Board Board;
        public PlayerNumber Number { get; private set; }
        public string Type { get; protected set; } = "Random";

        public void Configure(PlayerConstructorArguments args)
        {
            Board = new Board(args.BoardSize);
            Number = args.PlayerNumber;
        }

        public Move MakeMove(Coordinates opponentMove)
        {
            try
            {
                SetOpponentPosition(opponentMove);
                var randomGenerator = new Random();
                var openHexes =
                    Board
                        .Hexes
                        .Where(x => x.Owner == PlayerNumber.Unowned)
                        .ToList();
                var indexToChoose = randomGenerator.Next(openHexes.Count());
                var moveToMake = openHexes[indexToChoose];
                if (moveToMake != null)
                {
                    moveToMake.SetOwner(Number);
                    return new Move(moveToMake.Coordinates, Number);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ConcedeGame();
        }

        private Move ConcedeGame()
        {
            return new Move(new Coordinates(-1, -1), Number);
        }

        private void SetOpponentPosition(Coordinates opponentMove)
        {
            if (!opponentMove.Equals(new Coordinates(-1, -1)))
            {
                Board
                    .Hexes
                    .FirstOrDefault(x =>
                        x.Coordinates.Equals(opponentMove))?
                    .SetOwner(PlayerNumber.FirstPlayer);
            }
        }
    }
}
