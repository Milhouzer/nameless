using System;
using UnityEngine;

namespace Milhouzer.UI
{
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
        private bool DestroyOnHide;

        public event Action BeforeShow;
        public event Action AfterShow;
        public event Action BeforeHide;
        public event Action AfterHide;

        protected virtual void Awake() 
        {
            canvasGroup = GetComponent<CanvasGroup>();            
            UIManager.Instance.RegisterPanel(ID, this);
        }

        private void OnDestroy() {
            UIManager.Instance.UnRegisterPanel(ID);
        }

        public void Initialize(object data)
        {
            if(data is not T typedData)
                return;

            // _id = Guid.NewGuid().ToString();
            OnInitialize(typedData);
        }

        protected virtual void OnInitialize(T data)
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
    }
}
