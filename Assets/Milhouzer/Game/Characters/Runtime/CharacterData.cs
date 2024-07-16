using Milhouzer.Core.Characters;

namespace Milhouzer.Game.Characters
{
    [System.Serializable]
    public struct CharacterData : ICharacterData
    {
        public string Name;
        public int Age;
        public CharacterJobs Job;

    }

    public enum CharacterJobs
    {
        Unemployed,
        Student,
        Farmer,
        Professor,
        Mayor
    }
}
