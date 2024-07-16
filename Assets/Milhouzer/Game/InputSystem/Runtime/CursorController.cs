using Milhouzer.Common.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Milhouzer.InputSystem
{

    [System.Serializable]
    public class CursorsDataTable
    {
        [System.Serializable]
        public struct CursorData
        {
            public InteractableType interactableType;
            public Texture2D texture;
        }

        [SerializeField]
        List<CursorData> table = new();
        public List<CursorData> GetTable() => table;

        public CursorData GetCursorData(InteractableType interactableType)
        {
            foreach(CursorData data in table)
            {
                if(data.interactableType == interactableType)
                    return data;
            }

            return table[0];
        }
    }

    public class CursorController : MonoBehaviour
    {
        public static bool IsCursorOverUI = false;
        public CursorsDataTable cursorsDataTable;

        void Start()
        {
            PlayerInputManager.OnPointedObjectChanged += PlayerInputManager_OnPointedObjectChanged;
        }

        void Update()
        {
            IsCursorOverUI = EventSystem.current.IsPointerOverGameObject();
        }

        void PlayerInputManager_OnPointedObjectChanged(Transform pointedObject)
        {
            if(pointedObject == null)
            {
                Cursor.SetCursor(cursorsDataTable.GetTable()[0].texture, Vector2.zero, CursorMode.ForceSoftware); 
                return;
            }

            IInteractable interactable = pointedObject.GetComponent<IInteractable>();
            if(interactable != null)
            {
                SetCursor(InteractableType.Interactable);
            }
            
            ISelectable selectable = pointedObject.GetComponent<ISelectable>();
            if(selectable != null)
            {
                SetCursor(InteractableType.Selectable);
            }
            else
            {
                Cursor.SetCursor(cursorsDataTable.GetTable()[0].texture, Vector2.zero, CursorMode.ForceSoftware);
            }
        }

        private void SetCursor(InteractableType interactableType)
        {
            CursorsDataTable.CursorData data = cursorsDataTable.GetCursorData(interactableType);
            Cursor.SetCursor(data.texture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
