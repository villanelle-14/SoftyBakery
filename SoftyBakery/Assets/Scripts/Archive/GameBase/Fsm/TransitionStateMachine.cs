using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameBase.FiniteStateMachine
{
    public class TransitionStateMachine
    {
        #region Private Fields

        ITransitionState currentState;

        private bool isOnExit;
        private bool isOnEnter;
        
        private bool isAwaitUpdate;
        #endregion

        #region Public Methods

        /// <summary>
        /// Change state to the desired state
        /// </summary>
        /// <param name="newState">state to be changed</param>
        public async UniTask ChangeState(ITransitionState newState)
        {
            if (currentState != null)
            {
                isOnExit = true;
                await currentState.OnStateExit();
                isOnExit = false;
            }


            currentState = newState;
            Debug.Log(currentState);
            isOnEnter = true;
            await currentState.OnStateEnter();
            isOnEnter = false;
        }

        /// <summary>
        /// Update state machine
        /// </summary>
        public async UniTask OnMachineUpdate()
        {
            if (currentState != null && !isOnExit 
                && !isOnEnter && !isAwaitUpdate)
            {
                isAwaitUpdate = true;
                await currentState.OnStateUpdate();
                isAwaitUpdate = false;
            }
        }

        #endregion
    }
}