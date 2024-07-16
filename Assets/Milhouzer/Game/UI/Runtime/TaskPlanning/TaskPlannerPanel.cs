using Milhouzer.Core.AI;
using Milhouzer.Common.Interfaces;
using UnityEngine;
using Milhouzer.Game.AI;
using Milhouzer.Core.UI;

namespace Milhouzer.Game.UI
{
    public class TaskPlannerPanel : UIPanel<TaskPlannerBase>
    {
        [SerializeField]
        private Transform CardsContainer;

        protected TaskPlannerBase _planner;

        protected override void Awake()
        {
            if(string.IsNullOrEmpty(_id))
            {
                _id = UIManager.Settings.TASKPLANNER_PANEL_ID;
            }

            base.Awake();
        }

        private void Planner_TaskAdd(ITask task, ref bool Cancel)
        {
            GameObject TaskCardGO = Instantiate(UIManager.Settings.GetPanelReference(UIManager.Settings.TASK_CARD_ID).Prefab, CardsContainer);
            TaskCard card = TaskCardGO.GetComponent<TaskCard>();
            card.Initialize(task);
        }

        protected override void OnInitialize(IUIDataSerializer data)
        {
            
            _planner = (TaskPlannerBase)data.SerializeUIData()["Planner"];
            Debug.Log("Show taskplanner panel");
            _planner.TaskAddEvent += Planner_TaskAdd;
            Show();
        }
        
        protected class TaskPlannerBasePanelProperties : PanelProperties<TaskPlannerPanel>
        {
            string panelID;
            public override void SetCallbacks(TaskPlannerPanel panel)
            {
                // CameraController camera = Camera.main.GetComponent<CameraController>();
                // camera.SetTarget(_inspectable.WorldTransform);
                // camera.Zoom();
                
                panel.AfterHide += () => {
                    
                };
            }
        }
    }
}
