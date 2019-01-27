using System;
using System.Collections.Generic;
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
        public Transform GameObjectsContainer;

        public BricksContainerView BricksContainerPrefab;
        public FieldView FieldViewPrefab;
        public BrickView BrickViewPrefab;
        public RacketView RacketViewPrefab;
        public BallView BallViewPrefab;

        private BricksContainerView _bricksContainerInstance;
        private FieldView _fieldViewInstance;
        private List<BrickView> _brickViewInstances = new List<BrickView>();
        private RacketView _racketViewInstance;
        private BallView _ballViewInstance;

        public void Initialize()
        {
            var bricksCountPerType = 10;

            _fieldViewInstance = InstantiateElement<FieldView>(FieldViewPrefab, GameObjectsContainer);

            _bricksContainerInstance = InstantiateElement<BricksContainerView>(BricksContainerPrefab, GameObjectsContainer, new Vector3(0, 2.5f, 0));
            var brickTypes = Enum.GetValues(typeof(BrickType)).Cast<BrickType>().ToList();
            for (var i = 0; i < brickTypes.Count; i++)
            {
                for (var j = 0; j < bricksCountPerType; j++)
                {
                    var brickViewInstance = InstantiateElement<BrickView>(BrickViewPrefab, _bricksContainerInstance.transform);
                    _brickViewInstances.Add(brickViewInstance);
                } 
            }

            _racketViewInstance = InstantiateElement<RacketView>(RacketViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -4f, 0));

            _ballViewInstance = InstantiateElement<BallView>(BallViewPrefab, _fieldViewInstance.gameObject.transform);
        }

        public void Uninitialize()
        {
            //
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
