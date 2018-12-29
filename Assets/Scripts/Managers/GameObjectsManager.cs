using GameObjects.Ball;
using GameObjects.Brick;
using GameObjects.Field;
using GameObjects.Racket;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour, IInitializable
    {
        public Transform GameObjectsContainer;

        public FieldView FieldViewPrefab;
        public BrickView BrickViewPrefab;
        public RacketView RacketViewPrefab;
        public BallView BallViewPrefab;

        private FieldView _fieldViewInstance;
        private BrickView _brickViewInstance;
        private RacketView _racketViewInstance;
        private BallView _ballViewInstance;

        public void Initialize()
        {
            _fieldViewInstance = InstantiateElement<FieldView>(FieldViewPrefab, GameObjectsContainer);

            _brickViewInstance = InstantiateElement<BrickView>(BrickViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, 4f, 0));

            _racketViewInstance = InstantiateElement<RacketView>(RacketViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -4.5f, 0));

            _ballViewInstance = InstantiateElement<BallView>(BallViewPrefab, _fieldViewInstance.gameObject.transform);
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

            instance.transform.position = position;

            return instance;
        }
    }
}
