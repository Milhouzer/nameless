//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Pointer_InputMap"",
            ""id"": ""d98c9661-26f5-4fe1-8058-fc9fb03a553f"",
            ""actions"": [
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Value"",
                    ""id"": ""b3234d84-6de7-49ad-83a6-21e55b08e41f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""67e66534-cb2d-42cb-9280-a0ca1e9b807e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ffaa51a1-f076-4233-be32-dd15a600f6ec"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cd90ad3-998a-470c-b804-90fc54a1f5bb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera_InputMap"",
            ""id"": ""5fa4e473-2a68-40a8-870a-4113b13b20a1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""60ad7d7a-ec8c-476d-a5a0-3a9a4ad47e3d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DeZoom"",
                    ""type"": ""Button"",
                    ""id"": ""55382aa6-133a-40ee-9d5d-0b3a36a3e7f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""755debcd-6af3-48b8-9f2b-ff5209441080"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f94fa4ce-ed2b-42f5-8714-1f975fbb34b3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""98fac9ea-083f-4451-a640-da0df7028237"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""63dcb6d3-d9e9-4f2d-812b-5e328baaa83b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dd6e5325-0afd-4633-898c-c6edb19b4122"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97325b4d-aebd-4bf8-8101-b98e5676dd43"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9e0785e6-2cca-4067-9a2e-4ed9e85eb169"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae9b3ee1-54fe-40ff-baa0-75f323e2d4f1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard_InputMap"",
            ""id"": ""d5da3521-ad2e-4701-aac8-3e87ff4bec8b"",
            ""actions"": [
                {
                    ""name"": ""TogglePlanningMode"",
                    ""type"": ""Button"",
                    ""id"": ""2a8643d7-3597-4e74-90d5-f746ed4b2b02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleCameraMoveMode"",
                    ""type"": ""Button"",
                    ""id"": ""a0001d30-d984-4173-b4ec-837e442c86ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""a214dfd6-a6c7-495c-877a-89b8efc05e3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""75117152-979d-4e3a-a5e7-eb4aff8d4426"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePlanningMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29025dc2-632b-4a7a-9168-8b4e2b52986c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCameraMoveMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""390a29ab-17b6-4e05-8344-3efabe3c2c95"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Pointer_InputMap
        m_Pointer_InputMap = asset.FindActionMap("Pointer_InputMap", throwIfNotFound: true);
        m_Pointer_InputMap_RightClick = m_Pointer_InputMap.FindAction("RightClick", throwIfNotFound: true);
        m_Pointer_InputMap_LeftClick = m_Pointer_InputMap.FindAction("LeftClick", throwIfNotFound: true);
        // Camera_InputMap
        m_Camera_InputMap = asset.FindActionMap("Camera_InputMap", throwIfNotFound: true);
        m_Camera_InputMap_Move = m_Camera_InputMap.FindAction("Move", throwIfNotFound: true);
        m_Camera_InputMap_DeZoom = m_Camera_InputMap.FindAction("DeZoom", throwIfNotFound: true);
        m_Camera_InputMap_Zoom = m_Camera_InputMap.FindAction("Zoom", throwIfNotFound: true);
        // Keyboard_InputMap
        m_Keyboard_InputMap = asset.FindActionMap("Keyboard_InputMap", throwIfNotFound: true);
        m_Keyboard_InputMap_TogglePlanningMode = m_Keyboard_InputMap.FindAction("TogglePlanningMode", throwIfNotFound: true);
        m_Keyboard_InputMap_ToggleCameraMoveMode = m_Keyboard_InputMap.FindAction("ToggleCameraMoveMode", throwIfNotFound: true);
        m_Keyboard_InputMap_OpenInventory = m_Keyboard_InputMap.FindAction("OpenInventory", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Pointer_InputMap
    private readonly InputActionMap m_Pointer_InputMap;
    private List<IPointer_InputMapActions> m_Pointer_InputMapActionsCallbackInterfaces = new List<IPointer_InputMapActions>();
    private readonly InputAction m_Pointer_InputMap_RightClick;
    private readonly InputAction m_Pointer_InputMap_LeftClick;
    public struct Pointer_InputMapActions
    {
        private @PlayerInput m_Wrapper;
        public Pointer_InputMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightClick => m_Wrapper.m_Pointer_InputMap_RightClick;
        public InputAction @LeftClick => m_Wrapper.m_Pointer_InputMap_LeftClick;
        public InputActionMap Get() { return m_Wrapper.m_Pointer_InputMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Pointer_InputMapActions set) { return set.Get(); }
        public void AddCallbacks(IPointer_InputMapActions instance)
        {
            if (instance == null || m_Wrapper.m_Pointer_InputMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Pointer_InputMapActionsCallbackInterfaces.Add(instance);
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
        }

        private void UnregisterCallbacks(IPointer_InputMapActions instance)
        {
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
        }

        public void RemoveCallbacks(IPointer_InputMapActions instance)
        {
            if (m_Wrapper.m_Pointer_InputMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPointer_InputMapActions instance)
        {
            foreach (var item in m_Wrapper.m_Pointer_InputMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Pointer_InputMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Pointer_InputMapActions @Pointer_InputMap => new Pointer_InputMapActions(this);

    // Camera_InputMap
    private readonly InputActionMap m_Camera_InputMap;
    private List<ICamera_InputMapActions> m_Camera_InputMapActionsCallbackInterfaces = new List<ICamera_InputMapActions>();
    private readonly InputAction m_Camera_InputMap_Move;
    private readonly InputAction m_Camera_InputMap_DeZoom;
    private readonly InputAction m_Camera_InputMap_Zoom;
    public struct Camera_InputMapActions
    {
        private @PlayerInput m_Wrapper;
        public Camera_InputMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Camera_InputMap_Move;
        public InputAction @DeZoom => m_Wrapper.m_Camera_InputMap_DeZoom;
        public InputAction @Zoom => m_Wrapper.m_Camera_InputMap_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera_InputMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Camera_InputMapActions set) { return set.Get(); }
        public void AddCallbacks(ICamera_InputMapActions instance)
        {
            if (instance == null || m_Wrapper.m_Camera_InputMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Camera_InputMapActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @DeZoom.started += instance.OnDeZoom;
            @DeZoom.performed += instance.OnDeZoom;
            @DeZoom.canceled += instance.OnDeZoom;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(ICamera_InputMapActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @DeZoom.started -= instance.OnDeZoom;
            @DeZoom.performed -= instance.OnDeZoom;
            @DeZoom.canceled -= instance.OnDeZoom;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(ICamera_InputMapActions instance)
        {
            if (m_Wrapper.m_Camera_InputMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICamera_InputMapActions instance)
        {
            foreach (var item in m_Wrapper.m_Camera_InputMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Camera_InputMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Camera_InputMapActions @Camera_InputMap => new Camera_InputMapActions(this);

    // Keyboard_InputMap
    private readonly InputActionMap m_Keyboard_InputMap;
    private List<IKeyboard_InputMapActions> m_Keyboard_InputMapActionsCallbackInterfaces = new List<IKeyboard_InputMapActions>();
    private readonly InputAction m_Keyboard_InputMap_TogglePlanningMode;
    private readonly InputAction m_Keyboard_InputMap_ToggleCameraMoveMode;
    private readonly InputAction m_Keyboard_InputMap_OpenInventory;
    public struct Keyboard_InputMapActions
    {
        private @PlayerInput m_Wrapper;
        public Keyboard_InputMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TogglePlanningMode => m_Wrapper.m_Keyboard_InputMap_TogglePlanningMode;
        public InputAction @ToggleCameraMoveMode => m_Wrapper.m_Keyboard_InputMap_ToggleCameraMoveMode;
        public InputAction @OpenInventory => m_Wrapper.m_Keyboard_InputMap_OpenInventory;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard_InputMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Keyboard_InputMapActions set) { return set.Get(); }
        public void AddCallbacks(IKeyboard_InputMapActions instance)
        {
            if (instance == null || m_Wrapper.m_Keyboard_InputMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Keyboard_InputMapActionsCallbackInterfaces.Add(instance);
            @TogglePlanningMode.started += instance.OnTogglePlanningMode;
            @TogglePlanningMode.performed += instance.OnTogglePlanningMode;
            @TogglePlanningMode.canceled += instance.OnTogglePlanningMode;
            @ToggleCameraMoveMode.started += instance.OnToggleCameraMoveMode;
            @ToggleCameraMoveMode.performed += instance.OnToggleCameraMoveMode;
            @ToggleCameraMoveMode.canceled += instance.OnToggleCameraMoveMode;
            @OpenInventory.started += instance.OnOpenInventory;
            @OpenInventory.performed += instance.OnOpenInventory;
            @OpenInventory.canceled += instance.OnOpenInventory;
        }

        private void UnregisterCallbacks(IKeyboard_InputMapActions instance)
        {
            @TogglePlanningMode.started -= instance.OnTogglePlanningMode;
            @TogglePlanningMode.performed -= instance.OnTogglePlanningMode;
            @TogglePlanningMode.canceled -= instance.OnTogglePlanningMode;
            @ToggleCameraMoveMode.started -= instance.OnToggleCameraMoveMode;
            @ToggleCameraMoveMode.performed -= instance.OnToggleCameraMoveMode;
            @ToggleCameraMoveMode.canceled -= instance.OnToggleCameraMoveMode;
            @OpenInventory.started -= instance.OnOpenInventory;
            @OpenInventory.performed -= instance.OnOpenInventory;
            @OpenInventory.canceled -= instance.OnOpenInventory;
        }

        public void RemoveCallbacks(IKeyboard_InputMapActions instance)
        {
            if (m_Wrapper.m_Keyboard_InputMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyboard_InputMapActions instance)
        {
            foreach (var item in m_Wrapper.m_Keyboard_InputMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Keyboard_InputMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Keyboard_InputMapActions @Keyboard_InputMap => new Keyboard_InputMapActions(this);
    public interface IPointer_InputMapActions
    {
        void OnRightClick(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
    }
    public interface ICamera_InputMapActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDeZoom(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IKeyboard_InputMapActions
    {
        void OnTogglePlanningMode(InputAction.CallbackContext context);
        void OnToggleCameraMoveMode(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
    }
}
