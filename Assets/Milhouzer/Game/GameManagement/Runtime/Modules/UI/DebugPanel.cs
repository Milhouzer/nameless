using System.Text;
using Milhouzer.Core.Entities;
using Milhouzer.Core.UI;
using TMPro;
using UnityEngine;

namespace Milhouzer.GameManagement.Modules.UI
{
    public class DebugPanel : UIPanel<object>
    {
        [SerializeField]
        private TextMeshProUGUI properties;

        protected override void Awake()
        {
            _id = "Debug";
            base.Awake();

        }
        private void Update() 
        {
            properties.text = FormatProperties();
        }

        private string FormatProperties()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Debug Panel : \n");
            stringBuilder.AppendLine("GameState :" + GameManager.Instance.CurrentGameState.ID.ToString());
            stringBuilder.AppendLine("Entity :" + EntitiesManager.Instance.PossessedEntity);
            
            float fps = 1f / Time.deltaTime;
            stringBuilder.AppendLine("FPS: " + Mathf.Round(fps).ToString());

            return stringBuilder.ToString();
        }
    }
}
