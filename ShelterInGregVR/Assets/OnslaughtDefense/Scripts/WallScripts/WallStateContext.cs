using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class WallStateContext
    {
        public IWallState CurrentState
        {
            get; set;
        }

        private readonly WallController wallController;

        public WallStateContext(WallController controller)
        {
            wallController = controller;
        }

        public void Transition()
        {
            CurrentState.Handle(wallController);
        }

        public void Transition(IWallState state)
        {
            CurrentState = state;
            CurrentState.Handle(wallController);
        }
    }
}
