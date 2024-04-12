

using System.Collections.Generic;

namespace Milhouzer.Common.Interfaces
{
    public interface IUIDataSerializer
    {
        Dictionary<string, object> SerializeUIData();
    }
}