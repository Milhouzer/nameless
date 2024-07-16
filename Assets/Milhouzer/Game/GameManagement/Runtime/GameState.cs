namespace Milhouzer.GameManagement
{    
    public enum GameStateID
    {
        Base,
        Playing,
        Inspecting,
        TaskPlanning,
        InventoryMenu,
    }

    [System.Serializable]
    public abstract class GameState
    {
        public GameStateID ID = GameStateID.Base;

        public abstract void EnterState();

        public abstract void ExitState();
    }
}
