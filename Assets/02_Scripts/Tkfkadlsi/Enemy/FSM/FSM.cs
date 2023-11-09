using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tkfkadlsi
{
    public class FSM
    {
        public FSM(StateBase initState)
        {
            ChangeState(initState);
        }

        private StateBase currentState;

        public void ChangeState(StateBase nextState)
        {
            if (nextState == currentState) return;

            if (currentState != null)
                currentState.OnStateExit();

            currentState = nextState;
            currentState.OnStateEnter();
        }

        public void UpdateState()
        {
            if (currentState == null) return;
            currentState.OnStateUpdate();
        }
    }
}
