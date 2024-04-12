
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.Common.Interfaces
{

    public interface IInspectable : IUIDataSerializer
    {
        Transform WorldTransform { get; }
    }
}