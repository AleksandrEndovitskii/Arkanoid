using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using GameObjects.Ball;
using GameObjects.Brick;
using GameObjects.Field;
using GameObjects.Racket;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public event Action AllBrickViewInstancesWasDestroyed = delegate { };

        public Transform GameObjectsContainerPrefab;
        public BricksContainerView BricksContainerPrefab;
        public FieldView FieldViewPrefab;
        public BrickView BrickViewPrefab;
        public RacketView RacketViewPrefab;
        public BallView BallViewPrefab;

        [SerializeField]
        private int bricksCountPerType = 10;
        [SerializeField]
        private bool _advantageousPositioningOfTargetBrick = true;
        [SerializeField]
        private bool _randomLevelGeneration = true;

        private Transform _gameObjectsContainerInstance;
        public BricksContainerView _bricksContainerInstance;
        private FieldView _fieldViewInstance;
        private ObservableCollection<BrickView> _brickViewInstances = new ObservableCollection<BrickView>();
        private RacketView _racketViewInstance;
        private BallView _ballViewInstance;

        private List<BrickView> _brickViewInstancesForDestruction = new List<BrickView>();

        private static System.Random _random = new System.Random();

        public void Initialize()
        {
            _brickViewInstances.CollectionChanged += BrickViewInstancesOnCollectionChanged;

            _gameObjectsContainerInstance = Instantiate(GameObjectsContainerPrefab);

            _fieldViewInstance = InstantiateElement<FieldView>(FieldViewPrefab, _gameObjectsContainerInstance);

            _bricksContainerInstance = InstantiateElement<BricksContainerView>(BricksContainerPrefab, _gameObjectsContainerInstance, new Vector3(0, 2.5f, 0));
            _bricksContainerInstance.BrickPositioningCompleted += BricksContainerInstanceOnBrickPositioningCompleted;
            var brickTypes = Enum.GetValues(typeof(BrickType)).Cast<BrickType>().Reverse();
            foreach (var brickType in brickTypes)
            {
                for (var j = 0; j < bricksCountPerType; j++)
                {
                    var brickViewInstance = InstantiateElement<BrickView>(BrickViewPrefab, _bricksContainerInstance.transform);
                    brickViewInstance.Initialize(brickType);
                    brickViewInstance.WasDestroyed += BrickViewInstanceOnWasDestroyed;

                    _brickViewInstances.Add(brickViewInstance);
                } 
            }

            if (_randomLevelGeneration)
            {
                foreach (var brickViewInstance in _brickViewInstances)
                {
                    var destroyBrick = _random.Next(0, 2);
                    if (destroyBrick > 0)
                    {
                        _brickViewInstancesForDestruction.Add(brickViewInstance);
                    }
                }
            }

            if (_advantageousPositioningOfTargetBrick)
            {
                var randomBrick = _brickViewInstances.Last();
                randomBrick.Targeted = true;
            }
            else
            {
                var randomBrick = _brickViewInstances[_random.Next(_brickViewInstances.Count)];
                randomBrick.Targeted = true;
            }

            _racketViewInstance = InstantiateElement<RacketView>(RacketViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -4f, 0));

            _ballViewInstance = InstantiateElement<BallView>(BallViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -1f, 0));
        }

        public void Uninitialize()
        {
            _brickViewInstances.CollectionChanged -= BrickViewInstancesOnCollectionChanged;

            foreach (var brickViewInstance in _brickViewInstances)
            {
                brickViewInstance.WasDestroyed -= BrickViewInstanceOnWasDestroyed;
                if (brickViewInstance != null)
                {
                    Destroy(brickViewInstance.gameObject);
                }
            }
            _brickViewInstances.Clear();

            if (_bricksContainerInstance != null)
            {
                Destroy(_bricksContainerInstance.gameObject);
                _bricksContainerInstance = null;
            }

            if (_fieldViewInstance != null)
            {
                Destroy(_fieldViewInstance.gameObject);
                _fieldViewInstance = null;
            }

            if (_racketViewInstance != null)
            {
                Destroy(_racketViewInstance.gameObject);
                _racketViewInstance = null;
            }

            if (_ballViewInstance != null)
            {
                Destroy(_ballViewInstance.gameObject);
                _ballViewInstance = null;
            }

            if (_gameObjectsContainerInstance != null)
            {
                Destroy(_gameObjectsContainerInstance.gameObject);
                _gameObjectsContainerInstance = null;
            }
        }

        private void BricksContainerInstanceOnBrickPositioningCompleted()
        {
            if (_randomLevelGeneration)
            {
                foreach (var brickViewInstance in _brickViewInstancesForDestruction)
                {
                    brickViewInstance.WasDestroyed -= BrickViewInstanceOnWasDestroyed;
                    Destroy(brickViewInstance.gameObject);
                    _brickViewInstances.Remove(brickViewInstance);
                }
                _brickViewInstancesForDestruction.Clear();
            }
        }

        private void BrickViewInstanceOnWasDestroyed(BrickView brickView)
        {
            brickView.WasDestroyed -= BrickViewInstanceOnWasDestroyed;

            _brickViewInstances.Remove(brickView);

            if (brickView.Targeted)
            {
                GameManager.Instance.GameFlowManager.GameWon();
            }
        }

        private void BrickViewInstancesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_brickViewInstances.Count == 0)
            {
                AllBrickViewInstancesWasDestroyed.Invoke();
            }
        }

        private T InstantiateElement<T>(T prefab, Transform parentContainer) where T : MonoBehaviour
        {
            var instance = Instantiate(prefab);

            instance.gameObject.transform.SetParent(parentContainer);

            return instance;
        }

        private T InstantiateElement<T>(T prefab, Transform parentContainer, Vector3 position) where T : MonoBehaviour
        {
            var instance = InstantiateElement<T>(prefab, parentContainer);

            instance.transform.localPosition = position;

            return instance;
        }
    }
}
