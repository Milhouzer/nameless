namespace Milhouzer.Characters
{
    [System.Serializable]
    public struct CharacterInfos
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
