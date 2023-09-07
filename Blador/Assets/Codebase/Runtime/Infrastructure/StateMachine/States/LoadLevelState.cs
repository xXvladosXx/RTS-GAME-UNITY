using Codebase.Runtime.Infrastructure.Factory;
using Codebase.Runtime.Infrastructure.SceneManagement;
using Codebase.Runtime.Infrastructure.StateMachine.States.Core;
using Codebase.Runtime.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.Infrastructure.StateMachine.States
{
    public class LoadLevelState : ILoadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;

        private string _saveName;
        private string _sceneName;

        private LevelStaticData _levelData;

        public LoadLevelState(IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            IGameFactory gameFactory,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }


        public UniTask Load(string save)
        {
            _sceneName = "Main";

            return _sceneLoader.Load(_sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private async void OnLoaded()
        {
            /*_levelData = _staticDataService.ForLevel(_sceneName);

            await _gameFactory.CreatePlayer();
            await _gameFactory.CreateUI();
            _gameFactory.CreateResources(_levelData.Resources);

            foreach (var enemySpawner in _levelData.EnemySpawners)
            {
                _gameFactory.CreateEnemySpawner(enemySpawner.EnemyTypeID, enemySpawner.Position,
                    enemySpawner.TimeToSpawn, enemySpawner.IsLooped, enemySpawner.PortalParticleData);
            }
            
            foreach (var buildingSpawner in _levelData.BuildingSpawners)
            {
                await _gameFactory.CreateProductionSpawner(buildingSpawner.BuildingTypeID, buildingSpawner.Position);
            }*/
            _gameFactory.CreateUnitsCreator();
            _gameFactory.CreateAllyUnitsCreator();
            Debug.Log("Loaded");
            _gameStateMachine.Enter<LoadProgressState, string>(_saveName);
        }


        public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}