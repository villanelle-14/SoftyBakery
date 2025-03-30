using System;
using Cysharp.Threading.Tasks;
using GameBase.FiniteStateMachine;

namespace GameLogic
{
    public class ShopState : BasicTransitionState
    {
        public ShopState(Func<UniTask> onEnter, Func<UniTask> onUpdate, Func<UniTask> onExit) : base(onEnter, onUpdate, onExit)
        {
        }
    }
}