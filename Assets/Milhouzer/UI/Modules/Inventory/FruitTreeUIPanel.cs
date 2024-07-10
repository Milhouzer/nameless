using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Milhouzer.InventorySystem;
using Milhouzer.Common.Interfaces;

namespace Milhouzer.UI.Modules.InventorySystem
{
    public class FruitTreeUIPanel : UIPanel<IInspectable>
    {
        [SerializeField]
        private Image Portrait;
        [SerializeField]
        private TextMeshProUGUI Name;
        [SerializeField]
        private TextMeshProUGUI Production;
        [SerializeField]
        private TextMeshProUGUI Size;

        private FruitTreeInfos Infos;

        protected override void Awake()
        {
            
            if(string.IsNullOrEmpty(_id))
            {
                _id = UIManager.Settings.INSPECTABLE_INFOS_PANEL_ID;
            }
            base.Awake();
        }
        
        public bool CanReadData(Dictionary<string, object> data)
        {
            return data.ContainsKey("Panel") 
                && (string)data["Panel"] == "FruitTree" 
                && data.ContainsKey("FruitTreeInfos");
        }

        protected override void OnInitialize(IUIDataSerializer inspectable)
        {
            _id = UIManager.Settings.INSPECT_PANEL_ID;
            
            Dictionary<string, object> data = inspectable.SerializeUIData();
            if(!CanReadData(data))
                return;


            Infos = (FruitTreeInfos)data["FruitTreeInfos"];

            Refresh();
        }

        public override void Refresh()
        {
            Name.text = Infos.Name;
            Production.text = Infos.Production;
            Size.text = Infos.Size.ToString();
        }
    }
}
