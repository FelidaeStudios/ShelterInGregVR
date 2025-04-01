using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class WallBrokenState : MonoBehaviour, IWallState
    {
        private WallController wallController;

        public void Handle(WallController controller)
        {
            if (!wallController)
                wallController = controller;

            wallController.CurrentHealth = 0;
        }
    }
}
