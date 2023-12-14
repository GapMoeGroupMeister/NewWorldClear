using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public abstract class StateBase
    {
        protected Enemy enemy;

        public StateBase(Enemy initenemy)
        {
            enemy = initenemy;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
