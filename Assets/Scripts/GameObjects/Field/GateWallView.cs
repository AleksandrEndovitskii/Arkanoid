using GameObjects.Ball;
using Managers;
using UnityEngine;

namespace GameObjects.Field
{
    [RequireComponent(typeof(BoxCollider))]
    public class GateWallView : WallView, IBallOnCollisionEnterReactor
    {
        public void ReactOnCollisionEnter(BallView ballView)
        {
            GameManager.Instance.GameFlowManager.CurrentGameStatus = GameFlowManager.GameStatus.Loss;
        }
    }
}
