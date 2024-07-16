using UnityEngine;
using UnityEngine.InputSystem;
using Milhouzer.Common.Interfaces;
using Milhouzer.Common.Utility;

namespace Milhouzer.InputSystem
{
    public class PlayerInputManager : Singleton<PlayerInputManager>
    {
        PlayerInput inputActions;

        RaycastHit hit;
        public static Transform PointedObject => pointedObject;

        private static Transform pointedObject;

        protected override void Awake() 
        {
            base.Awake();

            inputActions = new PlayerInput();

            // Gameplay input events
            EnableGameplayMode();
        }

        public void EnableGameplayMode()
        {
            inputActions.Pointer_InputMap.RightClick.started += OnRightClickStarted;
            inputActions.Pointer_InputMap.RightClick.canceled += OnRightClickCanceled;
            inputActions.Pointer_InputMap.LeftClick.started += OnLeftClickStarted;
            inputActions.Pointer_InputMap.LeftClick.canceled += OnLeftClickCanceled;

            inputActions.Pointer_InputMap.Enable();

            inputActions.Camera_InputMap.Zoom.started += OnZoomStarted;
            inputActions.Camera_InputMap.Zoom.canceled += OnZoomCanceled;
            inputActions.Camera_InputMap.DeZoom.started += OnDeZoomStarted;
            inputActions.Camera_InputMap.DeZoom.canceled += OnDeZoomCanceled;

            inputActions.Camera_InputMap.Enable();

            inputActions.Keyboard_InputMap.TogglePlanningMode.started += TogglePlanningMode;
            inputActions.Keyboard_InputMap.ToggleCameraMoveMode.started += ToggleCameraMoveMode;
            inputActions.Keyboard_InputMap.OpenInventory.started += OpenInventory;

            inputActions.Keyboard_InputMap.Enable();
        }

        void DisableGameplayMode()
        {
            inputActions.Pointer_InputMap.RightClick.started -= OnRightClickStarted;
            inputActions.Pointer_InputMap.RightClick.canceled -= OnRightClickCanceled;
            inputActions.Pointer_InputMap.LeftClick.started -= OnLeftClickStarted;
            inputActions.Pointer_InputMap.LeftClick.canceled -= OnLeftClickCanceled;

            inputActions.Pointer_InputMap.Disable();

            inputActions.Camera_InputMap.Zoom.started -= OnZoomStarted;
            inputActions.Camera_InputMap.Zoom.canceled -= OnZoomCanceled;
            inputActions.Camera_InputMap.DeZoom.started -= OnDeZoomStarted;
            inputActions.Camera_InputMap.DeZoom.canceled -= OnDeZoomCanceled;

            inputActions.Camera_InputMap.Disable();

            inputActions.Keyboard_InputMap.TogglePlanningMode.started -= TogglePlanningMode;
            inputActions.Keyboard_InputMap.ToggleCameraMoveMode.started -= ToggleCameraMoveMode;
            inputActions.Keyboard_InputMap.OpenInventory.started -= OpenInventory;

            inputActions.Keyboard_InputMap.Enable();
        }

        private void Update() {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                SetPointedObject(objectHit);
            } else {
                SetPointedObject(null);
            }
        }

        public delegate void PointedObjectChange(Transform newPointedObject);
        public static event PointedObjectChange OnPointedObjectChanged;

        private void SetPointedObject(Transform newPointedObject)
        {
            pointedObject = newPointedObject;
            OnPointedObjectChanged?.Invoke(pointedObject);
        }

#region Pointer related inputs

        public delegate void GroundClicked(Vector3 position);
        public delegate void InteractableClicked(IInteractable interactable);
        public delegate void InspectableClicked(IInspectable inspectable);

        public static event GroundClicked OnGroundClicked;
        public static event InteractableClicked OnInteractableClicked;
        public static event InspectableClicked OnInspectableClicked;

        private void OnLeftClickStarted(InputAction.CallbackContext context)
        {

        }

        private void OnLeftClickCanceled(InputAction.CallbackContext context)
        {
            if(CursorController.IsCursorOverUI)
                return;

            if(PointedObject == null)
                return;

            if(PointedObject.gameObject.tag == "Ground")
            {
                OnGroundClicked?.Invoke(hit.point);
                return;
            }

            if(PointedObject.TryGetComponent<IInteractable>(out var interactable))
            {
                OnInteractableClicked?.Invoke(interactable);
                return;
            }

            if(PointedObject.TryGetComponent<ISelectable>(out var selectable))
            {
                selectable.Select();
                return;
            }
        }

        private void OnRightClickStarted(InputAction.CallbackContext context)
        {

        }

        private void OnRightClickCanceled(InputAction.CallbackContext context)
        {
            if(CursorController.IsCursorOverUI)
                return;

            if(PointedObject == null)
                return;

            if(PointedObject.TryGetComponent<IInspectable>(out var inspectable))
            {
                OnInspectableClicked?.Invoke(inspectable);
            }
        }
    
#endregion

#region Keyboard inputs

        public delegate void TogglePlanningModeInput();
        public static event TogglePlanningModeInput OnTogglePlanningMode;
        public delegate void ToggleCameraMoveModeInput();
        public static event ToggleCameraMoveModeInput OnToggleCameraMoveMode;
        public delegate void OpenInventoryInput();
        public static event OpenInventoryInput OnOpenInventory;

        private void TogglePlanningMode(InputAction.CallbackContext context)
        {
            OnTogglePlanningMode?.Invoke();
        }

        private void ToggleCameraMoveMode(InputAction.CallbackContext context)
        {
            OnToggleCameraMoveMode?.Invoke();
        }

        private void OpenInventory(InputAction.CallbackContext context)
        {
            OnOpenInventory?.Invoke();
        }

#endregion

#region Camera related inputs

        public delegate void ZoomInputStart_EventHandler();
        public delegate void ZoomInputCanceled_EventHandler();
        public delegate void DeZoomInputStarted_EventHandler();
        public delegate void DeZoomInputCanceled_EventHandler();

        public static event ZoomInputStart_EventHandler OnZoomInputStarted;
        public static event ZoomInputCanceled_EventHandler OnZoomInputCanceled;
        public static event DeZoomInputStarted_EventHandler OnDeZoomInputStarted;
        public static event DeZoomInputCanceled_EventHandler OnDeZoomInputCanceled;

        private void OnZoomStarted(InputAction.CallbackContext context)
        {
            OnZoomInputStarted?.Invoke();
        }

        private void OnZoomCanceled(InputAction.CallbackContext context)
        {
            OnZoomInputCanceled?.Invoke();
        }

        private void OnDeZoomStarted(InputAction.CallbackContext context)
        {
            OnDeZoomInputStarted?.Invoke();
        }

        private void OnDeZoomCanceled(InputAction.CallbackContext context)
        {
            OnDeZoomInputCanceled?.Invoke();
        }

        public Vector2 CameraMove => inputActions.Camera_InputMap.Move.ReadValue<Vector2>();
    
#endregion

    }
}
