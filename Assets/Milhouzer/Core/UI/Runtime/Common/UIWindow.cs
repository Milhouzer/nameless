// using System.Collections.Generic;
// using UnityEngine;

// namespace Milhouzer.Core.UI
// {
//     [RequireComponent(typeof(CanvasGroup))]
//     public abstract class UIWindow : MonoBehaviour, IWindowController
//     {
//         protected CanvasGroup canvasGroup;

//         [SerializeField]
//         protected string _id;

//         public string ID => _id;

//         [SerializeField]
//         protected bool _isVisible = true;

//         public bool IsVisible => _isVisible;


//         public event System.Action BeforeShow;
//         public event System.Action AfterShow;
//         public event System.Action BeforeHide;
//         public event System.Action AfterHide;

//         protected virtual void Awake() 
//         {
//             canvasGroup = GetComponent<CanvasGroup>();

//             UIManager.Instance.RegisterWindow(ID, this);
//         }

//         protected virtual void SetVisibility(bool value)
//         {
//             _isVisible = value;
//             canvasGroup.alpha = value ? 1f : 0f;
//             canvasGroup.blocksRaycasts = value;
//             canvasGroup.interactable = value;
//         }

//         public virtual void Show() 
//         {
//             BeforeShow?.Invoke();

//             SetVisibility(true);

//             AfterShow?.Invoke();
//         }

//         public abstract void Show<T>(T data);

//         public virtual void Hide() 
//         {
//             BeforeHide?.Invoke();

//             SetVisibility(false);
            
//             AfterHide?.Invoke();
//         }
//     }
// }
