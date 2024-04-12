using UnityEngine;

namespace Milhouzer.Common.Interfaces
{
    public interface IAnimator
    {
        Animator Animator { get; }
        void SetState(string state);
    }
}
