﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Engine.ValueTypes;

namespace Engine.Players
{
    public class PlayerFactory
    {
        public List<PlayerType> PlayerTypes { get; private set; }
        private Assembly Assembly { get; set; }
        private PlayerBase _player;

        public PlayerBase Build() => _player;

        public PlayerFactory(Assembly assembly = null)
        {
            Assembly = assembly ?? Assembly.GetExecutingAssembly();
            GetPlayerTypes();
        }

        public static PlayerFactory Init()
        {
            return new PlayerFactory();
        }

        private void GetPlayerTypes()
        {
            var pType = typeof(PlayerBase);

            PlayerTypes = Assembly
                    .GetTypes()
                    .Where(t => 
                        t.IsClass 
                        && t != pType
                        && pType.IsAssignableFrom(t))
                    .Select(t => new PlayerType( t.Name, t))
                    .ToList();
        }

        public PlayerFactory NewOfType(string playerType)
        {
            var type = PlayerTypes.FirstOrDefault(x => x.Name == playerType)?.Type;
            _player = (PlayerBase)Activator.CreateInstance(type);
            return this;
        }
        public PlayerFactory ForBoardSize(int size)
        {
            _player.SetBoardSize(size);
            return this;
        }

        public PlayerFactory WithConfiguration(Configuration config)
        {
            _player?.Configure(config);
            return this;
        }

        public PlayerFactory AsPlayerOne()
        {
            _player?.SetPlayer(PlayerNumber.FirstPlayer);
            return this;
        }

        public PlayerFactory AsPlayerTwo()
        {
            _player?.SetPlayer(PlayerNumber.SecondPlayer);
            return this;
        }

        public PlayerBase CreatePlayerOfType(string playerType)
        {
            if (PlayerTypes.Any(x => x.Name == playerType))
            {
                var type = PlayerTypes.FirstOrDefault(x => x.Name == playerType)?.Type;
                if (type == null)
                {
                    return null;
                }
                var player = (PlayerBase)Activator.CreateInstance(type);
            }

            return _player;
        }

        public class PlayerType
        {
            public string Name { get; private set; }
            public Type Type { get; private set; }
            public PlayerType(string name, Type type)
            {
                Type = type;
                Name = name;
            }
        }

    }


}
