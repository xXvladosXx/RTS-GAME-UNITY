using System;
using System.Collections.Generic;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.Selection.Actions;
using Codebase.Runtime.UI.Selection.Action;
using Codebase.Runtime.UI.Selection.Detailed;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.Runtime.UI.Selection
{
    public class SpawnButton : BaseButton
    {
        [SerializeField] private SpawnEntity _spawnEntityPrefab;
        [SerializeField] private DetailedActions _detailedActions;
        [SerializeField] private PossibleActions _possibleActions;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _button;
        
        private IObjectPool _objectPool;
        private List<SpawnEntity> _spawnedEntities = new();
        private SpawnData _data;
        private UIQueue _uiQueue;

        [Inject]
        public void Construct(IObjectPool objectPool,
            UIQueue uiQueue)
        {
            _objectPool = objectPool;
            _uiQueue = uiQueue;
        }

        private void Awake()
        {
            _button.onClick.AddListener(CreateSpawnButtons);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(CreateSpawnButtons);
        }

        private void CreateSpawnButtons()
        {
            _possibleActions.gameObject.SetActive(false);
            _uiQueue.PushPage(_detailedActions);
            
            foreach (var spawnedUnit in _data.SpawnedUnits)
            {
                var spawnableEntityButton = _objectPool.GetOrCreate(_spawnEntityPrefab);
                spawnableEntityButton.transform.SetParent(_parent);
                spawnableEntityButton.transform.localScale = Vector3.one;
                _spawnedEntities.Add(spawnableEntityButton);
                Debug.Log(spawnedUnit);
            }

            Deactivate();
        }

        public override void Activate(BaseAction action)
        {
            gameObject.SetActive(true);
            _data = action.Data as SpawnData;
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
            foreach (var spawnEntity in _spawnedEntities)
            {
                spawnEntity.DestroyAsPooledObject();
            }
            _spawnedEntities.Clear();
        }
    }
}