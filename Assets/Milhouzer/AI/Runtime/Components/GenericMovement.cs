using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.AI
{
    /// <summary>
    /// Generic movement class. Mouvement components of any kind should derive from this class.
    /// </summary>
    public abstract class GenericMovement : MonoBehaviour
    {
        /// <summary>
        /// Move the object to a point.
        /// </summary>
        /// <param name="destination">Destination to move the object at.</param>
        public abstract void Move(Vector3 destination);

        /// <summary>
        /// Move the object towards a target.
        /// </summary>
        /// <param name="target">Target to move the object towards.</param>
        public abstract void Move(Transform target);
        
        /// <summary>
        /// Look at a target.
        /// </summary>
        /// <param name="target">Target to look at.</param>
        public abstract void LookAt(Transform target);
    }
}
