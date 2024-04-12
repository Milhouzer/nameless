using System.Collections.Generic;
using Milhouzer.InputSystem;
using Milhouzer.Common.Utility;
using UnityEngine;
using Milhouzer.Common.Interfaces;
using System;
using Milhouzer.InventorySystem;
using Milhouzer.Entities;
using Milhouzer.AI;

namespace Milhouzer.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private UIManagerSettings _settings;

        public static UIManagerSettings Settings => Instance._settings;

        [SerializeField]
        private Canvas Canvas;
        private Transform CanvasTransform;

        protected Dictionary<string, IPanelController> registeredPanels = new();
        protected Dictionary<string, IPanelController> Panelss =>  registeredPanels;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnValidate() {
            if(Canvas != null)
                CanvasTransform = Canvas.GetComponent<Transform>();

        }

        private void OnEnable() {
            PlayerInputManager.OnInteractableClicked += PlayerInputManager_InteractableClicked;
            PlayerInputManager.OnInspectableClicked += PlayerInputManager_InspectableClicked;
            PlayerInputManager.OnOpenInventory += PlayerInputManager_OpenInventory;
            PlayerInputManager.OnTogglePlanningMode += PlayerInputManager_TogglePlanningMode;

            /// <TODO>
            /// Handle OpenInventory by creating panel, not with game state change, same for inspectable clicked.
            /// Too many game states otherwise, overcomplicated.
            /// Only gamestate for pause should be present
            /// QUESTION : Handle callbacks ? How to prevent camera movement/
            /// </TODO>
        }


        private void OnDisable() {
            PlayerInputManager.OnInteractableClicked -= PlayerInputManager_InteractableClicked;
            PlayerInputManager.OnOpenInventory -= PlayerInputManager_OpenInventory;
        }

        private void PlayerInputManager_TogglePlanningMode()
        {
            IPossessable entity = EntitiesManager.Instance.PossessedEntity;
            if(entity != null)
            {
                if(!entity.GameObjectRef.TryGetComponent<ITaskRunner>(out var runner))
                    return;

                TaskPlannerBase _planner = new TaskPlannerBase(runner);
                
                CreatePanel(_planner);
            }
        }
        
        private void PlayerInputManager_OpenInventory()
        {
            if(registeredPanels.ContainsKey(_settings.INVENTORY_PANEL_ID))
            {
                _ = registeredPanels[_settings.INVENTORY_PANEL_ID].IsVisible ? 
                        HidePanel(_settings.INVENTORY_PANEL_ID) : ShowPanel(_settings.INVENTORY_PANEL_ID);
            }
            else
            {
                InventoryBase inventory = EntitiesManager.Instance.PossessedEntity?.GameObjectRef.GetComponent<InventoryBase>();
                if(inventory != null)
                {
                    CreatePanel(inventory);
                }
            }
        }


        void PlayerInputManager_InteractableClicked(IInteractable interactable)
        {
            CreatePanel<IUIDataSerializer>(interactable, "InteractionSequencePickerPanel");
            // CreatePanel(interactable);
        }

        private void PlayerInputManager_InspectableClicked(IInspectable inspectable)
        {
            CreatePanel(inspectable);
        }

        public void RegisterPanel(string ID, IPanelController panel)
        {
            if(!string.IsNullOrEmpty(ID) && !registeredPanels.ContainsKey(ID))
            {
                registeredPanels.Add(ID, panel);
            }
        }

        public void UnRegisterPanel(string ID)
        {
            if(registeredPanels.ContainsKey(ID))
            {
                registeredPanels.Remove(ID);
            }
        }

        private void CreatePanel(IUIDataSerializer obj)
        {
            Dictionary<string, object> data = obj.SerializeUIData();
            if(!data.ContainsKey("Type"))
                return;
            
            string panelID = (string)data["Type"];

            if(string.IsNullOrEmpty(panelID))
                return;

            IPanelController panel = CreatePanel<IUIDataSerializer>(obj, panelID);
            if(panel == null)
                return;
        }

        public IPanelController CreatePanel<T>(T properties, string ID)
        {
            PanelReference reference = _settings.GetPanelReference(ID);
            if(reference.Equals(default(PanelReference)))
                return null;

            GameObject panel = Instantiate(reference.Prefab, CanvasTransform);
            
            if(!panel.TryGetComponent<IPanelController>(out var panelController))
            {
                Destroy(panel);
                return null;
            }

            RegisterPanel(reference.ID, panelController);

            panelController.Initialize(properties);
            panelController.Show();

            return panelController;
        }


        public bool ShowPanel<T>(T properties, string ID)
        {
            registeredPanels.TryGetValue(ID, out IPanelController panelController);
            if(panelController != null)
            {
                panelController.Initialize(properties);
                registeredPanels[ID].Show();
                return true;
            }
            return false;
        }

        public bool ShowPanel(string ID)
        {
            if(string.IsNullOrEmpty(ID))
                return false;

            if(registeredPanels.ContainsKey(ID))
            {
                registeredPanels[ID].Show();
                return true;
            }
            return false;
        }

        public bool HidePanel(string ID)
        {
            if(string.IsNullOrEmpty(ID))
                return false;

            if(registeredPanels.ContainsKey(ID))
            {
                registeredPanels[ID].Hide();
                return true;
            }
            return false;
        }
    }
}
