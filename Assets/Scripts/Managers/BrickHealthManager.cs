using System;
using System.Collections.Generic;
using System.Linq;
using GameObjects.Brick;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class BrickHealthManager : MonoBehaviour, IInitializable
    {
        [Serializable]
        public class BrickTypeHealth
        {
            public BrickType BrickType;
            public int Health;
        }

        public List<BrickTypeHealth> BrickTypesHealths = new List<BrickTypeHealth>();

        public void Initialize()
        {
            //
        }

        public int GetHealthForBrickType(BrickType brickType)
        {
            var brickTypeHealth = BrickTypesHealths.FirstOrDefault(x => x.BrickType == brickType);
            if (brickTypeHealth == null)
            {
                Debug.LogError(String.Format("Health value not specified for {0} brick type", brickType));
                throw new ArgumentException();
            }

            return brickTypeHealth.Health;
        }
    }
}
