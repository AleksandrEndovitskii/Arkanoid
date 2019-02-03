using UnityEngine;
using Utilities;

namespace Managers
{
    public class GameFlowManager : MonoBehaviour, IInitializable, IUninitializable
    {
        public void Initialize()
        {
            GameManager.Instance.GameObjectsManager.AllBrickViewInstancesWasDestroyed += GameObjectsManagerOnAllBrickViewInstancesWasDestroyed;
        }

        public void Uninitialize()
        {
            GameManager.Instance.GameObjectsManager.AllBrickViewInstancesWasDestroyed -= GameObjectsManagerOnAllBrickViewInstancesWasDestroyed;
        }

        public void GameWon()
        {
            //
        }

        public void GameLost()
        {
            //
        }

        private void GameObjectsManagerOnAllBrickViewInstancesWasDestroyed()
        {
            GameWon();
        }
    }
}
