using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Milhouzer.Common.Utility
{
    public static class UIUtils
    {
        public static void SetAlpha(this Image image, float value)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, value);
        }
    }
}
