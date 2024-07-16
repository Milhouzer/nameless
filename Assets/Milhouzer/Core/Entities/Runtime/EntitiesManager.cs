using UnityEngine;
using Milhouzer.Common.Utility;
using System.Collections.Generic;
using Milhouzer.Common.Interfaces;

namespace Milhouzer.Core.Entities
{
    public class EntitiesManager : Singleton<EntitiesManager>
    {
        public delegate void EntitiesLoaded();
        public static event EntitiesLoaded OnEntitiesLoaded;
        public delegate void EntityPossessed(IPossessable entity);
        public static event EntityPossessed OnEntityPossessed;
        public delegate void EntityUnPossessed(IPossessable entity);
        public static event EntityUnPossessed OnEntityUnPossessed;
        
        List<IEntityController> entities = new();
        IPossessable _possessedEntity;
        public IPossessable PossessedEntity => _possessedEntity;
        
        bool isLoaded;
        bool IsLoaded => isLoaded;

        public void Load()
        {
            bool hasSave = false;
            if(hasSave)
            {
                LoadSave();
            }else
            {
                LoadForFirstTime();
                LoadForFirstTime();
            }

            isLoaded = true;

            OnEntitiesLoaded?.Invoke();

            // Should be bound to unselect rather than unposses + name of event isn't really clear
            // PlayerInputManager.OnToggleCameraMoveMode += UnPossessCurrentEntity;
        }

        void LoadForFirstTime()
        {
            IEntityController entity = CreateEntity("Colobus", entities.Count.ToString());
            if(entity == null)
                return;

            entities.Add(entity);            
            PossessEntity(entity);
        }

        /// <TODO>
        /// Deleegate to dedicated factory component
        /// </TODO>
        public static IEntityController CreateEntity(string path, string name)
        {
            GameObject entityObject = Resources.Load<GameObject>("Colobus");
            if(!entityObject.TryGetComponent<IEntityController>(out IEntityController entity))
                return null;

            entityObject = GameObject.Instantiate(entityObject, Vector3.zero, Quaternion.identity, null);
            entityObject.name = name;

            return entity;
        }

        void LoadSave()
        {
            
        }

        private void UnPossessCurrentEntity()
        {
            if(_possessedEntity != null)
                _possessedEntity.UnPossess();
        }

        public void PossessEntity(IEntityController entity)
        {
            if(entity == null || entity is not IPossessable possessableEntity)
            {
                Debug.LogError("IPossessable is null");
                return;
            }
            
            _possessedEntity = possessableEntity;

            OnEntityPossessed?.Invoke(possessableEntity);
        }

        public void UnPossessEntity(IEntityController entity)
        {
            if(entity is not IPossessable possessableEntity)
                return;

            if(_possessedEntity == entity)
            {
                _possessedEntity = null;
                OnEntityUnPossessed?.Invoke(possessableEntity);
            }
        }
    }
}
