using System;
using System.Collections.Generic;
using Milhouzer.Entities;
using Milhouzer.InputSystem;
using Milhouzer.Common.Interfaces;
using Milhouzer.Common.Utility;
using UnityEngine;
using Milhouzer.InventorySystem;

namespace Milhouzer.GameManagement
{

    [System.Serializable]
    
    [CreateAssetMenu(fileName = "GameStateSwitchConfig", menuName = "Milhouzer/Game/GameStateSwitchConfig", order = 0)]
    public class GameStateSwitchConfig : ScriptableObject 
    {
        public List<GameSwitchAuth> Config = new();
    }

    [System.Serializable]
    public struct GameSwitchAuth
    {
        public GameStateID ID;
        public List<GameStateID> AllowedTargetStates;
    }


    public class GameStateChangeEventArgs : EventArgs
    {
        public GameState OldState;
        public GameState NewState;

        public GameStateChangeEventArgs(GameState oldState, GameState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
    
    public class GameManager : Singleton<GameManager>
    {
        public delegate void GameStateChange(GameStateChangeEventArgs e);
        public static event GameStateChange OnGameStateChanged;

        [SerializeField]
        GameStateSwitchConfig SwitchConfig;


        private GameState _gameState;
        public GameState CurrentGameState => _gameState;
        private GameState _lastGameState;
        public GameState LastGameState => _lastGameState;

        protected override void Awake() 
        {
            SetGameState(new GameState_Playing());
        }

        private void Start() 
        {
            LoadGame();
        }

        void LoadGame()
        {
            // Load Managers.
            EntitiesManager.Instance.Load();
        }

        public void SetGameState(GameState gameState)
        {
            if(!CanSetGameState(gameState))
                return;

            if(CurrentGameState != null)
            {
                CurrentGameState.ExitState();
            }
            
            GameStateChangeEventArgs e = new GameStateChangeEventArgs(CurrentGameState, gameState);
            _lastGameState = _gameState;
            _gameState = gameState;
            _gameState.EnterState();
            
            OnGameStateChanged?.Invoke(e);
        }

        private bool CanSetGameState(GameState gameState)
        {
            if(CurrentGameState == null)
                return true;

            return SwitchConfig.Config.Exists(x => x.ID == CurrentGameState.ID && x.AllowedTargetStates.Contains(gameState.ID));
        }
    }
}
