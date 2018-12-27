using GameObjects.Ball;
using GameObjects.Field;
using GameObjects.Racket;
using UnityEngine;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour
    {
        public Transform GameObjectsContainer;

        public FieldView FieldViewPrefab;
        public RacketView RacketViewPrefab;
        public BallView BallViewPrefab;

        private FieldView _fieldViewInstance;
        private RacketView _racketViewInstance;
        private BallView _ballViewInstance;

        public void Initialize()
        {
            _fieldViewInstance = InstantiateElement(FieldViewPrefab, GameObjectsContainer).GetComponent<FieldView>();

            _racketViewInstance = InstantiateElement(RacketViewPrefab, _fieldViewInstance.gameObject.transform, new Vector3(0, -4.5f, 0));

            _ballViewInstance = InstantiateElement(BallViewPrefab, _fieldViewInstance.gameObject.transform).GetComponent<BallView>();
        }

        private MonoBehaviour InstantiateElement(MonoBehaviour prefab, Transform parentContainer)
        {
            var instance = Instantiate(prefab);

            instance.gameObject.transform.SetParent(parentContainer);

            return instance;
        }

        private RacketView InstantiateElement(RacketView prefab, Transform parentContainer, Vector3 position)
        {
            var instance = InstantiateElement(prefab, parentContainer).GetComponent<RacketView>();

            instance.transform.position = position;

            return instance;
        }
    }
}
