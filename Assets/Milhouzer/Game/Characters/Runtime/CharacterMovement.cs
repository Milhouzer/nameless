using Milhouzer.Game.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Milhouzer.Game.Characters
{
    public class CharacterMovement : GenericMovement
    {
        Animator animator;
        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public override void Move(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public override void Move(Transform target)
        {
            agent.SetDestination(target.position);
        }

        public override void LookAt(Transform target)
        {
            
        }
    }
}
