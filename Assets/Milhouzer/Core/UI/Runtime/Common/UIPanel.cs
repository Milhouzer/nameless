using System;
using Milhouzer.Common.Interfaces;
using UnityEngine;

namespace Milhouzer.Core.UI
{
    /// <TODO>
    /// Remove, UIPanel could be non generic.
    /// </TODO>
    public abstract class PanelProperties<T> 
    {
        public abstract void SetCallbacks(T owner);
    }
    
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel<T> : MonoBehaviour, IPanelController
    {
        protected PanelProperties<T> _properties;
        public PanelProperties<T> Properties => _properties;
        
        protected CanvasGroup canvasGroup;

        public string _id;
        public string ID => _id;

        protected bool _isVisible;

        public bool IsVisible => _isVisible;

        [SerializeField]
        private bool _destroyOnHide = true;
        public bool DestroyOnHide => _destroyOnHide;

        public event Action BeforeShow;
        public event Action AfterShow;
        public event Action BeforeHide;
        public event Action AfterHide;

        protected virtual void Awake() 
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Initialize(IUIDataSerializer data)
        {
            OnInitialize(data);
        }

        protected virtual void OnInitialize(IUIDataSerializer data)
        {

        }

        public virtual void Refresh()
        {

        }

        protected virtual void SetVisibility(bool value)
        {
            _isVisible = value;
            canvasGroup.alpha = value ? 1f : 0f;
            canvasGroup.blocksRaycasts = value;
            canvasGroup.interactable = value;
        }
        
        public virtual void Show() 
        {
            if(_isVisible)
                return;
                
            BeforeShow?.Invoke();

            SetVisibility(true);

            AfterShow?.Invoke();
        }

        public virtual void Hide() 
        {
            if(!_isVisible)
                return;

            BeforeHide?.Invoke();

            SetVisibility(false);
            
            AfterHide?.Invoke();

            if(DestroyOnHide)
            {
                Destroy(gameObject);
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
