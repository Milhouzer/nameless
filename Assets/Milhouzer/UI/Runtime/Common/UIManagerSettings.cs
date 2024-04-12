using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.UI
{
    [System.Serializable]
    public class UIManagerSettings
    {
        public string INSPECTABLE_INFOS_PANEL_ID = "inspectable_info_panel";
        public string INVENTORY_PANEL_ID = "Inventory";
        public string COOK_STATION_PANEL_ID = "cook_station_panel";
        public string TASKPLANNER_PANEL_ID = "TaskPlannerBase";
        public string INSPECT_PANEL_ID = "inspect_panel";
        public string TASK_CARD_ID = "TaskCard";

        [SerializeField]
        private List<PanelReference> PanelReferences = new();

        public PanelReference GetPanelReference(string ID)
        {
            return PanelReferences.Find(x => x.ID == ID);
        }
    }
}
