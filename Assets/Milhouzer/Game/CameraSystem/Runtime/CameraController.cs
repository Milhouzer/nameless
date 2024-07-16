using System.Collections;
using Milhouzer.Core.Entities;
using Milhouzer.Common.Utility;
using Milhouzer.Common.Interfaces;
using UnityEngine;
using Milhouzer.InputSystem;
using System;

namespace Milhouzer.CameraSystem
{
    public class CameraController : Singleton<CameraController>
    {
        [Header("Offset")]
        [SerializeField]
        Vector3 trackedObjectOffset;
        
        [Header("Move Speed")]
        [SerializeField]
        float switchTargetSpeed = 0;
        [SerializeField]
        float followTargetSpeed = 0;
        [SerializeField]
        float freeMoveSpeed = 0;

        [Header("Rotation")]
        [SerializeField]
        float lookAtThreshold = 0.05f;
        [SerializeField]
        float rotationSpeed = 0f;

        [Header("Zoom")]
        [SerializeField]
        Vector2 zoomLimits = new Vector2(0.3f, 1.5f);
        [SerializeField]
        float zoomSpeed = 0f;
        
        bool _hasReachedNewTarget = false;
        float currentZoomLevel = 1f;
        Vector3 currentObjectOffset;
        Coroutine zoomRoutine;

        
        Transform _lastTarget;
        public Transform LastTarget => _lastTarget;
        Transform _target;
        public Transform Target => _target;

        public CameraMovementMode MovementMode = CameraMovementMode.Unrestricted;

        protected override void Awake() 
        {
            base.Awake();

            EntitiesManager.OnEntityPossessed += OnEntityPossessed;
            EntitiesManager.OnEntityUnPossessed += OnEntityUnPossessed;

            PlayerInputManager.OnZoomInputStarted += Zoom;
            PlayerInputManager.OnZoomInputCanceled += ResetZoom;

            PlayerInputManager.OnDeZoomInputCanceled += ResetZoom;
            PlayerInputManager.OnDeZoomInputStarted += DeZoom;

            PlayerInputManager.OnToggleCameraMoveMode += ToggleCameraMoveMode;

            currentObjectOffset = trackedObjectOffset * currentZoomLevel;
        }

        private void Update() {
            switch(MovementMode)
            {
                case CameraMovementMode.FollowTarget:
                    TrackTarget();
                    break;
                case CameraMovementMode.Unrestricted:
                    FreeMove();
                    break;
            }
        }

        void OnEntityPossessed(IPossessable entity)
        {
            SetMovementMode(CameraMovementMode.FollowTarget);
            SetTarget(entity.GameObjectRef.transform);
            
        }

        private void OnEntityUnPossessed(IPossessable entity)
        {
            SetMovementMode(CameraMovementMode.Unrestricted);
            SetTarget(null);
        }

        private void ToggleCameraMoveMode()
        {
            switch(MovementMode)
            {
                case CameraMovementMode.Unrestricted:
                    SetMovementMode(CameraMovementMode.FollowTarget);
                    SetTarget(_lastTarget);
                    break;
                case CameraMovementMode.FollowTarget:
                    SetMovementMode(CameraMovementMode.Unrestricted);
                    SetTarget(null);
                    break;
            }
        }

        private void SetMovementMode(CameraMovementMode mode)
        {
            MovementMode = mode;
        }

        public void SetTarget(Transform target)
        {
            _lastTarget = _target;
            _target = target;
            _hasReachedNewTarget = target == null ? true : false;
        }

        void TrackTarget()
        {
            Vector3 finalPos = _target.position + currentObjectOffset;
            transform.position = Vector3.Lerp(transform.position, finalPos, (_hasReachedNewTarget ? followTargetSpeed : switchTargetSpeed) * Time.deltaTime);
            
            float d = Vector3.Distance(transform.position, finalPos);
            if(d < lookAtThreshold)
            {
                SmoothLookAtTarget();
                float dinit = (_lastTarget == null || _hasReachedNewTarget )? 0 : Vector3.Distance(_lastTarget.transform.position, _target.transform.position);
                if((dinit - d) > dinit * 0.9f)
                    _hasReachedNewTarget = true;
            }
        }

        void SmoothLookAtTarget()
        {
            Vector3 targetDirection = _target.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        private void FreeMove()
        {
            // if(GameManager.Instance.CurrentGameState.BlocksCameraFreeMove)
            //     return;

            Vector3 proj = Vector3.ProjectOnPlane(transform.forward * PlayerInputManager.Instance.CameraMove.y, Vector3.up)
                            + transform.right * PlayerInputManager.Instance.CameraMove.x;

            transform.position = Vector3.Lerp(transform.position, transform.position + proj, freeMoveSpeed * Time.deltaTime);
        }

        public void ResetZoom()
        {
            // if(GameManager.Instance.CurrentGameState.BlocksCameraZoom)
            //     return;

            ZoomTowards(1f);
        }

        public void Zoom()
        {
            // if(GameManager.Instance.CurrentGameState.BlocksCameraZoom)
            //     return;

            Debug.Log($"Zoom towards {zoomLimits.x}");
            ZoomTowards(zoomLimits.x);
        }

        public void DeZoom()
        {
            // if(GameManager.Instance.CurrentGameState.BlocksCameraZoom)
            //     return;

            ZoomTowards(zoomLimits.y);
        }

        public void ZoomTowards(float value)
        {
            // if(GameManager.Instance.CurrentGameState.BlocksCameraZoom)
            //     return;

            if(zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
            }
            zoomRoutine = StartCoroutine(Zoom(value, zoomSpeed));
        }

        IEnumerator Zoom(float value, float zoomSpeed)
        {
            while(Mathf.Abs(currentZoomLevel - value) > 0.01f)
            {
                Debug.Log($"Zoom towards {Mathf.Lerp(currentZoomLevel, value, zoomSpeed)}: {currentZoomLevel} to {value} at {zoomSpeed}");
                currentZoomLevel = Mathf.Lerp(currentZoomLevel, value, zoomSpeed);
                currentObjectOffset = trackedObjectOffset * currentZoomLevel;
                yield return null;
            }
            Mathf.Lerp(currentZoomLevel, value, zoomSpeed);
            currentZoomLevel = value;
            currentObjectOffset = trackedObjectOffset * currentZoomLevel;
            zoomRoutine = null;
        }
    }
}
