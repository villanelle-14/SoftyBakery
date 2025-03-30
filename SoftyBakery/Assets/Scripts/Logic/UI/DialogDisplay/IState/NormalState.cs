using System;
using Cysharp.Threading.Tasks;
using GameBase.FiniteStateMachine;

namespace GameLogic
{
    public class NormalState : BasicTransitionState
    {
        public NormalState(Func<UniTask> onEnter, Func<UniTask> onUpdate, Func<UniTask> onExit) : base(onEnter, onUpdate, onExit)
        {
        }
    }
}