using System;
using Cysharp.Threading.Tasks;
using GameBase.FiniteStateMachine;

namespace GameLogic
{
    public class CgState : BasicTransitionState
    {
        public CgState(Func<UniTask> onEnter, Func<UniTask> onUpdate, Func<UniTask> onExit) : base(onEnter, onUpdate, onExit)
        {
        }
    }
}