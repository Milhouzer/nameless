using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Milhouzer.Common.Interfaces;

namespace Milhouzer.AI
{
    /// <summary>
    /// Base class for interactable objects.
    /// Holds references to interaction sequences that represents the possible interaction with this object.
    /// </summary>
    public class InteractableBase : MonoBehaviour, IInteractable, IUIDataSerializer
    {
        [SerializeField]
        float _interactionRadius = 1f;
        public float InteractionRadius => _interactionRadius;

        [SerializeField]
        InteractableType _interactableType = InteractableType.Interactable;
        public InteractableType InteractableType => _interactableType;

        [Obsolete("Property will be removed because it imposes a single interactor at a time.")]
        private GameObject _currentInteractor;

        [Obsolete("Property will be removed because it imposes a single interactor at a time.")]
        public GameObject CurrentInteractor => _currentInteractor;

        public bool IsRunning => throw new NotImplementedException();

        public GameObject Owner => gameObject;

        [SerializeField]
        List<InteractionSequence> _options;
        public ReadOnlyCollection<InteractionSequence> Options => _options.AsReadOnly();
        
        public bool Interact(ITaskRunner interactor, int index)
        {
            if(CanInteract(interactor.Owner))
            {
                InteractTask interactTask = Activator.CreateInstance<InteractTask>();
                interactTask.Initialize(interactor, gameObject, new InteractTaskData(this, index));

                interactor.AddTask(interactTask);
                return true;
            }

            return false;
        }

        public bool StopInteract()
        {
            return true;
        }

        public bool CanInteract(GameObject interactor)
        {
            try
            {
                if(interactor == null)
                    return false;

                return Vector3.Distance(transform.position, interactor.transform.position) < _interactionRadius;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.yellow; // Couleur du gizmo
            Gizmos.DrawWireSphere(transform.position, _interactionRadius);
        }
        
        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel","InteractableBase"},
                {"Interactable", this},
                {"Options", Options},
            };
        }
    }
}
