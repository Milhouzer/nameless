using System;
using System.Collections.ObjectModel;
using Milhouzer.Core.AI;
using Milhouzer.Core.Entities;
using Milhouzer.Common.Interfaces;
using UnityEngine;
using Milhouzer.Core.UI;
using Milhouzer.Game.AI;

namespace Milhouzer.Game.UI
{
    public class InteractionSequencePickerPanel : UIPanel<IInteractable>, IOptionPicker<int>, ITrackObjectUI
    {
        [SerializeField]
        MenuOption[] _options;
        [SerializeField]
        Transform optionsContainer;

        IInteractable _interactable;

        protected override void OnInitialize(IUIDataSerializer data)
        {
            _interactable = (IInteractable)data.SerializeUIData()["Interactable"];

            ReadOnlyCollection<InteractionSequence> options = _interactable.Options;
            _options = optionsContainer.GetComponentsInChildren<MenuOption>();
            DisplayOptions(options);
            TrackObject(_interactable.Owner.transform.position, 1f);
            Show();
        }

        private void Update() {
            TrackObject(_interactable.Owner.transform.position, 0.2f);
        }
        
        private void DisplayOptions(ReadOnlyCollection<InteractionSequence> options)
        {
            for (int i = 0; i < _options.Length; i++)
            {
                int index = i;

                if(i < options.Count)
                {
                    InteractionSequence sequence = options[i];

                    _options[i].gameObject.SetActive(true);
                    _options[i].SetLabel(sequence.Name);

                    _options[i].SelectAction += () => 
                    {
                        int picked = Pick(index);
                        if(picked == -1)
                            return;

                        _options[index].gameObject.SetActive(false);
                    };
                }else
                {
                    _options[i].gameObject.SetActive(false);
                }
            }
        }

        public int Pick(int index)
        {
            // Debug.Log()
            if(EntitiesManager.Instance.PossessedEntity is not ITaskRunner runnerEntity)
                return -1;

            // Add interact task with picked option to runner
            InteractTask interactTask = Activator.CreateInstance<InteractTask>();
            interactTask.Initialize(runnerEntity, _interactable.Owner, new InteractTaskData(_interactable, index));

            runnerEntity.AddTask(interactTask);

            return index;
        }

        public void TrackObject(Vector3 wPos, float speed)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(wPos);
            transform.position = Vector3.Lerp(transform.position, pos, speed);
        }
    }
}
