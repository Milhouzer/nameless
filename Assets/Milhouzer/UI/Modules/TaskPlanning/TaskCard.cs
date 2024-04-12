using Milhouzer.AI;
using TMPro;
using UnityEngine;

namespace Milhouzer.UI.Modules.TaskPlanning
{
    public class TaskCard : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI Label;

        public void Initialize(ITask task)
        {
            Debug.Log("Initialize task " + task);
            Label.text = task.GetData().Name;
        }
    }
}
