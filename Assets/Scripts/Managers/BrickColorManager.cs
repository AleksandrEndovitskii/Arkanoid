using System;
using System.Collections.Generic;
using System.Linq;
using GameObjects.Brick;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class BrickColorManager : MonoBehaviour, IInitializable, IUninitializable
    {
        [Serializable]
        public class BrickTypeColor
        {
            public BrickType BrickType;
            public Color Color;
        }

        public List<BrickTypeColor> BrickTypesColors = new List<BrickTypeColor>();

        public void Initialize()
        {
            //
        }

        public void Uninitialize()
        {
            //
        }

        public Color GetColorForBrickType(BrickType brickType)
        {
            var brickTypeColor = BrickTypesColors.FirstOrDefault(x => x.BrickType == brickType);
            if (brickTypeColor == null)
            {
                Debug.LogError(String.Format("Color value not specified for {0} brick type", brickType));
                throw new ArgumentException();
            }

            return brickTypeColor.Color;
        }
    }
}
