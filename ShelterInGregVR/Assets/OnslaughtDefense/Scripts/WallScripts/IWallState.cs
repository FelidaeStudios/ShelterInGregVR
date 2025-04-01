using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public interface IWallState
    {
        void Handle(WallController controller);
    }
}

