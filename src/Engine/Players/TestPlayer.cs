﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.ValueTypes;
using Engine;

namespace Engine.Players
{
    public class TestPlayer : MustInitialize<PlayerConstructorArguments>, IPlayer
    {
        public Queue<Coordinates> Moves { get; set; } = new Queue<Coordinates>();
        public PlayerNumber Number { get; }
        public string Type { get; protected set; } = "Test";

        public TestPlayer(PlayerConstructorArguments parameters) : base(parameters)
        {
            Number = parameters.PlayerNumber;
        }

        public TestPlayer AddMove(Coordinates coordinstes)
        {
            Moves.Enqueue(coordinstes);
            return this;
        }

        public TestPlayer AddMove(int x, int y)
        {
            return AddMove(new Coordinates(x, y));
        }

        public Coordinates ViewNextMove()
        {
            return Moves.Any() ? Moves.Peek() : new Coordinates(-1,-1);
        }

        public Move MakeMove(Coordinates opponentMove)
        {
            if (!Moves.Any())
            {
                return new Move(new Coordinates(-1,-1), Number);
            }

            var move = new Move(Moves.Dequeue(), Number);
            
            return move;
        }

    }
}
