using System;
using System.Collections.Generic;
using Zenject;

namespace Codebase.Runtime.GameplayCore
{
    public class GameLoopHandler : ITickable, IDisposable
    {
        private readonly List<IGameLoop> _gameBehaviours = new();

        public void Add(IGameLoop gameBehaviour)
        {
            _gameBehaviours.Add(gameBehaviour);
        }
        
        public void Tick()
        {
            for (int i = 0; i < _gameBehaviours.Count; i++)
            {
                if (!_gameBehaviours[i].GameUpdate())
                {
                    int lastIndex = _gameBehaviours.Count - 1;
                    _gameBehaviours[i] = _gameBehaviours[lastIndex];
                    _gameBehaviours.RemoveAt(lastIndex);
                    i -= 1;
                }
            }   
        }

        public void Dispose()
        {
        }
    }
}