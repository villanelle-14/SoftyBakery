using Cysharp.Threading.Tasks;

namespace GameBase.FiniteStateMachine
{
    public interface ITransitionState
    {
        /// <summary>
        /// Calling when entering state
        /// </summary>
        UniTask OnStateEnter();

        /// <summary>
        /// Calling when updating state
        /// </summary>
        UniTask OnStateUpdate();

        /// <summary>
        /// Calling when leaving state
        /// </summary>
        UniTask OnStateExit();
    }
}