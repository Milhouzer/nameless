using UnityEngine;

namespace Milhouzer.Common.Interfaces
{
    public interface IPossessable
    {
        GameObject GameObjectRef { get; }

        bool Possess();
        bool UnPossess();

        public delegate void PossessEvent();
        public delegate void UnPossessEvent();

        public event PossessEvent OnPossessed;
        public event UnPossessEvent OnUnPossessed;
    }
}
