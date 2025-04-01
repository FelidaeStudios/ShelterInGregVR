using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class WallController : MonoBehaviour
    {
        public int maxHealth = 100;

        public float CurrentHealth { get; set; }

        public Health CurrentHealthCheckpoint
        {
            get; private set;
        }

        private IWallState
            fullHealthState, brokenState, damageState;

        private WallStateContext wallStateContext;

        void Start()
        {
            //States: Full health, 66% health, 33% health, broken
            wallStateContext =
                new WallStateContext(this);

            fullHealthState =
                gameObject.AddComponent<WallFullHealthState>();
            brokenState =
                gameObject.AddComponent<WallBrokenState>();
            damageState =
                gameObject.AddComponent<WallDamageState>();
            //secondDecayState =
                //gameObject.AddComponent<WallSecondDecayState>();
        }

        public void HealthyWall()
        {
            wallStateContext.Transition(fullHealthState);
        }

        public void BrokenWall()
        {
            wallStateContext.Transition(brokenState);
        }

        public void Damage(Health healthState)
        {
            CurrentHealthCheckpoint = healthState;
            wallStateContext.Transition(damageState);
        }
    }
}

