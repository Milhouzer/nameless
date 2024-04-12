using System.Collections.Generic;
using UnityEngine;
using Milhouzer.Common.Interfaces;

namespace Milhouzer.Characters
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
        private CharacterInfos Infos;
        public CharacterInfos GetInfos() { return Infos; }

        /** Inspectable **/

        public Transform WorldTransform => transform;

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Type","Character"},
                {"CharacterInfos", Infos},
            };
        }
    }               
}
