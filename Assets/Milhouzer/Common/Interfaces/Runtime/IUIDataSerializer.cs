

using System.Collections.Generic;

namespace Milhouzer.Common.Interfaces
{
    /// <TODO>
    /// Rename for something more generic : IComponentDataSerializer
    /// OR
    /// Move this to UI namespace and use it only in logic classes
    /// </TODO>
    public interface IUIDataSerializer
    {
        /// <summary>
        /// Serialize data to be displayed in UI
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> SerializeUIData();
    }
}