using System;
using System.Collections.Generic;
using Milhouzer.CameraSystem;
using Milhouzer.Common.Interfaces;
using Milhouzer.InventorySystem.CraftingSystem;
using Milhouzer.UI.InventorySystem;
using UnityEngine;

namespace Milhouzer.UI.Modules.InventorySystem.CraftingSystem
{
    [Obsolete]
    public class EnergyBasedCrafterPanel : UIPanel<EnergyBasedCrafter>
    {
        protected class EnergyBasedCrafterPanelProperties : PanelProperties<EnergyBasedCrafter>
        {
            string panelID;
            public override void SetCallbacks(EnergyBasedCrafter _inspectable)
            {
                CameraController camera = Camera.main.GetComponent<CameraController>();
                camera.SetTarget(_inspectable.WorldTransform);
                camera.Zoom();
                
                // panel.AfterHide += () => {
                //     GameManager.Instance.ToggleInspectMode(_inspectable);
                // };
            }
        }

        protected new EnergyBasedCrafterPanelProperties _properties = new();

        [SerializeField]
        InventoryPanel fuelPanel;
        [SerializeField]
        InventoryPanel ingredientsPanel;
        [SerializeField]
        InventoryPanel outputPanel;

        protected override void Awake()
        {
            if(string.IsNullOrEmpty(_id))
            {
                _id = UIManager.Settings.INVENTORY_PANEL_ID;
            }

            base.Awake();
        }

        protected override void OnInitialize(IUIDataSerializer data)
        {
            // fuelPanel.Initialize(data.InputFuel);
            // ingredientsPanel.Initialize(data.InputIngredients);
            // outputPanel.Initialize(data.Output);

            // fuelPanel.Refresh();
            // ingredientsPanel.Refresh();
            // outputPanel.Refresh();
        }
    }
}
