using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Milhouzer.UI 
{
    public class MenuOption : MonoBehaviour, IPointerClickHandler
    {
        public Action SelectAction;

        [SerializeField]
        private TextMeshProUGUI Label;

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectAction.Invoke();
        }

        public void SetLabel(string label)
        {
            Label.text = label;
        }
    }
}
