using System;
using Cysharp.Threading.Tasks;

namespace GameBase.FiniteStateMachine
{
    public class BasicTransitionState : ITransitionState
    {
        private Func<UniTask> OnEnter;
        private Func<UniTask> OnUpdate;
        private Func<UniTask> OnExit;
        public BasicTransitionState(Func<UniTask> onEnter, Func<UniTask> onUpdate, Func<UniTask> onExit)
        {
            OnEnter = onEnter;
            OnUpdate = onUpdate;
            OnExit = onExit;
        }
        public async UniTask OnStateEnter()
        {
            await OnEnter.Invoke();
        }

        public async UniTask OnStateUpdate()
        {
            await OnUpdate.Invoke();
        }

        public async UniTask OnStateExit()
        {
            await OnExit.Invoke();
        }
    }
}