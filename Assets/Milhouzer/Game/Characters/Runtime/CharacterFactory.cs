using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.Game.Characters
{
    public static class CharacterFactory
    {
        public const string DEFAULT_CHARACTER_PATH = "Colobus";

        public static GameObject CreateCharacter(string path)
        {
            GameObject character = Resources.Load<GameObject>(path == "" ? DEFAULT_CHARACTER_PATH : path);
            character = GameObject.Instantiate(character, Vector3.zero, Quaternion.identity, null);

            return character;
        }

        public static GameObject CreateCharacter(string path, Vector3 position)
        {
            GameObject character = Resources.Load<GameObject>(path == "" ? DEFAULT_CHARACTER_PATH : path);
            character = GameObject.Instantiate(character, position, Quaternion.identity, null);

            return character;
        }

        public static GameObject CreateCharacter(string path, Vector3 position, Quaternion rotation)
        {
            GameObject character = Resources.Load<GameObject>(path == "" ? DEFAULT_CHARACTER_PATH : path);
            character = GameObject.Instantiate(character, position, rotation, null);

            return character;
        }

        public static GameObject CreateCharacter(string path, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject character = Resources.Load<GameObject>(path == "" ? DEFAULT_CHARACTER_PATH : path);
            character = GameObject.Instantiate(character, position, rotation, parent);

            return character;
        }
    }
}
