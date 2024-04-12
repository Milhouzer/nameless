using System.Collections.Generic;
using Milhouzer.Characters;
using UnityEngine;

namespace Milhouzer.UI
{
    public static class UIFactory
    {
        // // Marrant d'avoir codé ça parce que chiant à mettre en place avec la struct et pas l'interface mais ça sert
        // // à rien au final parce qu'on a pas les références aux objets propres au Panel genre textes, images, etc vu
        // // que le GO est vide.
        // // Du coup est ce que la struct est vraiment utile ? public struct CharacterProperties : ICharacterProperties

        // public static TPanel CreatePanel<TPanel, TProperties>(TProperties props)
        //     where TPanel : UIPanel<TProperties>
        //     where TProperties : IPanelProperties
        // {
        //     GameObject panelObject = new GameObject();
        //     TPanel panel = panelObject.AddComponent<TPanel>();

        //     panel.SetProperties(props);

        //     return panel;
        // }

        // ////////////////////////////////////////////////////////


        // /// <summary>
        // /// Creates a window of the desired type.
        // /// </summary>
        // /// <param name="ID">Name and ID of the window.</param>
        // /// <typeparam name="TWindow">Type of the window.</typeparam>
        // /// <returns>The window created.</returns>
        // public static TWindow CreateWindow<TWindow>(string ID)
        //     where TWindow : UIWindow
        // {
        //     GameObject windowObject = new GameObject
        //     {
        //         name = ID
        //     };
        //     TWindow window = windowObject.AddComponent<TWindow>();

        //     return window;
        // } 

        /// <summary>
        /// Creates a panel of the desired type and the desired property. The property should be coherent with
        /// the type of the panel created otherwise it may not function properly.
        /// </summary>
        /// <param name="prefab">Prefab of the panel</param>
        /// <param name="parent">Parent window of the panel</param>
        /// <param name="props">Properties of the panel</param>
        /// <typeparam name="TPanel">Type of the panel.</typeparam>
        /// <typeparam name="TProperties">Type of the properties.</typeparam>
        /// <returns>The panel created.</returns>
        // public static TPanel CreatePanel<TPanel, TProperties>(GameObject prefab, TProperties props)
        //     where TPanel : UIPanel<TProperties>
        //     where TProperties : IObjectProperty
        // {
        //     GameObject panelObject = GameObject.Instantiate(prefab);
        //     TPanel panel = panelObject.GetComponent<TPanel>();

        //     panel.SetProperties(props);

        //     return panel;
        // }

        /// <summary>
        /// Creates a panel of the desired type and the desired property. The property should be coherent with
        /// the type of the panel created otherwise it may not function properly.
        /// </summary>
        /// <param name="prefab">Prefab of the panel</param>
        /// <param name="parent">Parent window of the panel</param>
        /// <param name="props">Properties of the panel</param>
        /// <typeparam name="TPanel">Type of the panel.</typeparam>
        /// <typeparam name="TProperties">Type of the properties.</typeparam>
        /// <returns>The panel created.</returns>
        // public static TPanel CreateWorldSpacePanel<TPanel, TProperties>(GameObject prefab, TProperties props, Transform target)
        //     where TPanel : UIPanel<TProperties>
        //     where TProperties : IObjectProperty
        // {
        //     GameObject panelObject = GameObject.Instantiate(prefab);
        //     TPanel panel = panelObject.GetComponent<TPanel>();
        //     UIPanelModifier followTarget = panelObject.AddComponent<UIPanelModifier>();
        //     followTarget.Target = target;

        //     panel.SetProperties(props);

        //     return panel;
        // }
    }
}
