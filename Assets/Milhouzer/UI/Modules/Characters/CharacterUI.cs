using System.Collections;
using System.Collections.Generic;
using Milhouzer.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Milhouzer.Common.Interfaces;

namespace Milhouzer.Characters
{
    public class CharacterUI : UIPanel<IInspectable>
    {
        [SerializeField]
        private Image Portrait;
        [SerializeField]
        private TextMeshProUGUI Name;
        [SerializeField]
        private TextMeshProUGUI Age;
        [SerializeField]
        private TextMeshProUGUI Job;

        private CharacterInfos Infos;

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
                && (string)data["Panel"] == "Character" 
                && data.ContainsKey("CharacterInfos");
        }

        protected override void OnInitialize(IUIDataSerializer data)
        {
            _id = UIManager.Settings.INSPECT_PANEL_ID;

            Dictionary<string, object> dict = data.SerializeUIData();
            if(!CanReadData(dict))
                return;

            Infos = (CharacterInfos)dict["CharacterInfos"];

            Refresh();
        }

        public override void Refresh()
        {
            Name.text = Infos.Name;
            Age.text = Infos.Age.ToString();
            Job.text = Infos.Job.ToString();
        }
    }
}
