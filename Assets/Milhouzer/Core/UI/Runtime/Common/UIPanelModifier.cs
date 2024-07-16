using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Milhouzer.Game.UI
{
    public class UIPanelModifier : MonoBehaviour
    {
        public Transform Target;

        // Update is called once per frame
        void Update()
        {
            if(Target)
            {
                transform.position = Camera.main.WorldToScreenPoint(Target.position);
            }
        }
    }
}