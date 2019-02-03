using System;
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

        public Transform GameObjectsContainer;

        public BricksContainerView BricksContainerPrefab;
        public FieldView FieldViewPrefab;
        public BrickView BrickViewPrefab;
        public RacketView RacketViewPrefab;
        public BallView BallViewPrefab;

        [SerializeField]
        private int bricksCountPerType = 10;

        private BricksContainerView _bricksContainerInstance;
        private FieldView _fieldViewInstance;
        private ObservableCollection<BrickView> _brickViewInstances = new ObservableCollection<BrickView>();
        private RacketView _racketViewInstance;
        private BallView _ballViewInstance;

        public void Initialize()
        {
            _brickViewInstances.CollectionChanged += BrickViewInstancesOnCollectionChanged;

            _fieldViewInstance = InstantiateElement<FieldView>(FieldViewPrefab, GameObjectsContainer);

            _bricksContainerInstance = InstantiateElement<BricksContainerView>(BricksContainerPrefab, GameObjectsContainer, new Vector3(0, 2.5f, 0));
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

            _racketViewInstance = InstantiateElement<RacketView>(RacketViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -4f, 0));

            _ballViewInstance = InstantiateElement<BallView>(BallViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -1f, 0));
        }

        public void Uninitialize()
        {
            foreach (var brickViewInstance in _brickViewInstances)
            {
                brickViewInstance.WasDestroyed -= BrickViewInstanceOnWasDestroyed;
            }

            _brickViewInstances.CollectionChanged -= BrickViewInstancesOnCollectionChanged;
        }

        private void BrickViewInstanceOnWasDestroyed(BrickView brickView)
        {
            brickView.WasDestroyed -= BrickViewInstanceOnWasDestroyed;

            _brickViewInstances.Remove(brickView);
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
