using System.Collections.Generic;
using UnityEngine;
using Milhouzer.Common.Interfaces;
using Milhouzer.Core.Characters;

namespace Milhouzer.Game.Characters
{
    /// <summary>
    /// This class contains informations related to, a character :
    /// - Infos,
    /// - Stats,
    /// ...
    /// </summary>
    public class Character : MonoBehaviour, ICharacter, IInspectable
    {
        /** Character **/

        [SerializeField]
        private CharacterData Infos;
        public ICharacterData GetInfos() { return Infos; }

        /** Inspectable **/

        public Transform WorldTransform => transform;

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel","Character"},
                {"CharacterInfos", Infos},
            };
        }
    }               
}
