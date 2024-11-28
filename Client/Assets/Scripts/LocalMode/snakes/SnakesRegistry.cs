﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.SnakeLogic;

namespace LocalMode.Snakes
{
    public class LocalSnakesRegistry
    {
        private readonly Dictionary<string, SnakeInfo> _snakes;
        //private readonly Dictionary<string, Snake> _snakes;

        public LocalSnakesRegistry() => 
            _snakes = new Dictionary<string, SnakeInfo>();

        public SnakeInfo this[string key] => _snakes[key];

        public event Action<string> Added;
        public event Action<string> Removed;
        public event Action Updated;

        public IEnumerable<(string, SnakeInfo)> All() => 
            _snakes.Select(pair => (pair.Key, pair.Value));

        public void Add(string key, SnakeInfo snake)
        {
            _snakes[key] = snake;
            Updated?.Invoke();
            Added?.Invoke(key);
        }

        public bool Remove(string key)
        {
            var result = _snakes.Remove(key);

            if (result)
            {
                Updated?.Invoke();
                Removed?.Invoke(key);
            }

            return result;
            //return true;
        }

        public bool Contains(string key) => 
            _snakes.ContainsKey(key);
    }
}