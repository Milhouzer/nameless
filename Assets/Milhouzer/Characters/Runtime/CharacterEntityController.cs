using Milhouzer.Entities;
using Milhouzer.InputSystem;
using Milhouzer.AI;
using Milhouzer.Common.Interfaces;
using UnityEngine;

namespace Milhouzer.Characters
{
    public class CharacterEntityController : EntityControllerBase, IPossessable, ISelectable
    {
        GenericMovement _movementComponent;
        
        #region ISelectable

        bool isSelected = false;
        public bool IsSelected => isSelected;

        public event ISelectable.SelectEvent OnSelected;
        public event ISelectable.UnSelectEvent OnUnSelected;

        protected override void Awake() {
            _movementComponent = GetComponent<GenericMovement>();
            base.Awake();
        }
        
        public bool Select()
        {
            if ((object)ISelectable.Current == this)
                return true;

            if (ISelectable.Current != null)
            {
                ISelectable.Current.UnSelect();
            }

            OnSelected?.Invoke();
            ISelectable.Current = this;
            isSelected = true;
            
            Possess();

            return true;
        }

        public bool UnSelect()
        {
            OnUnSelected?.Invoke();
            isSelected = false;
            
            UnPossess();

            return true;
        }

        #endregion

        #region IPossessable
            
        bool isPossessed = false;

        public GameObject GameObjectRef => gameObject;
        public bool IsPossessed => isPossessed;        
        
        public event IPossessable.PossessEvent OnPossessed;
        public event IPossessable.UnPossessEvent OnUnPossessed;
        
        public bool Possess()
        {
            EntitiesManager.Instance.PossessEntity(this);
            
            OnPossessed?.Invoke();
            
            PlayerInputManager.OnGroundClicked += PlayerInputManager_OnGroundClicked;
            PlayerInputManager.OnInteractableClicked += PlayerInputManager_InteractableClicked;
            
            isPossessed = true;
            return true;
        }

        public bool UnPossess()
        {
            EntitiesManager.Instance.UnPossessEntity(this);

            OnUnPossessed?.Invoke();
            
            PlayerInputManager.OnGroundClicked -= PlayerInputManager_OnGroundClicked;
            PlayerInputManager.OnInteractableClicked -= PlayerInputManager_InteractableClicked;

            isPossessed = false;
            return true;
        }
        
        #endregion

        #region Callbacks
            
        void PlayerInputManager_OnGroundClicked(Vector3 destination)
        {
            MoveTask task = new MoveTask();
            task.Initialize(this, gameObject, new MoveTaskData(destination, GetComponent<GenericMovement>()));
            AddTask(task);
        }   

        private void PlayerInputManager_InteractableClicked(IInteractable interactable)
        {
            _movementComponent.LookAt(interactable.Owner.transform);
        }     

        #endregion
    }
}

