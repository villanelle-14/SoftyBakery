using System;
namespace GameBase.FiniteStateMachine
{
    public class BasicState : IState
    {
        private Action OnEnter;
        private Action OnUpdate;
        private Action OnExit;
        public BasicState(Action onEnter, Action onUpdate, Action onExit)
        {
            OnEnter = onEnter;
            OnUpdate = onUpdate;
            OnExit = onExit;
        }
        public void OnStateEnter()
        {
            OnEnter();
        }

        public void OnStateUpdate()
        {
            OnUpdate();
        }

        public void OnStateExit()
        {
            OnExit();
        }
    }
}