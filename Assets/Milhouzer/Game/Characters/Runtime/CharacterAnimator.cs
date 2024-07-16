using Milhouzer.Common.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Milhouzer.Game.Characters{
    public class CharacterAnimator : MonoBehaviour, IAnimator
    {
        [SerializeField]
        Animator animator;
        public Animator Animator => animator;
        
        [SerializeField]
        NavMeshAgent agent;

        void Update()
        {
            animator.SetFloat("Speed", agent.velocity.magnitude/agent.speed);
        }

        public void SetState(string stateName)
        {
            animator.CrossFadeInFixedTime(stateName, 0.5f, 1, 0f, 0f);
        }
    }
}
