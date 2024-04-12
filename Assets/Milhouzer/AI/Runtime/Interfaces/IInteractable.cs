using System.Collections.ObjectModel;
using Milhouzer.AI;
using UnityEngine;
using System;
using Milhouzer.Common.Interfaces;

namespace Milhouzer
{
    /// <summary>
    /// Interactable interface.
    /// Interactable components should implement this interface to specify possible interactions with them.
    /// </summary>
    public interface IInteractable : IUIDataSerializer
    {
        /// <summary>
        /// Maximum interaction distance
        /// </summary>
        /// <value></value>
        float InteractionRadius { get; }

        /// <summary>
        /// Interaction type. Useful for cursor.
        /// </summary>
        /// <TODO>
        /// This parameter is only used to change the cursor when hovering object. Delegate this feature to another component.
        /// </TODO>
        InteractableType InteractableType { get; }

        [Obsolete("An interactable component should not be able to interact with only 1 interactor.")]
        GameObject CurrentInteractor { get; }

        [Obsolete("Change for IsInteracting.")]
        bool IsRunning { get; }
        
        /// <summary>
        /// Reference to the GameObject holding the interactable component.
        /// </summary>
        /// <value></value>
        GameObject Owner { get; }

        /// <summary>
        /// Possible interactions.
        /// </summary>
        /// <value></value>
        public ReadOnlyCollection<InteractionSequence> Options { get; }

        /// <summary>
        /// Interact with the object.
        /// </summary>
        /// <param name="interactor">Interactor that triggered the interaction.</param>
        /// <param name="index">Interaction sequence to run.</param>
        /// <returns></returns>
        bool Interact(ITaskRunner interactor, int index);

        /// <summary>
        /// Check if the interaction with an object is possible
        /// </summary>
        /// <param name="interactor">Interactor that requested the interaction,</param>
        /// <returns>True if the interaction is possible. False otherwise.</returns>
        bool CanInteract(GameObject interactor);

        /// <summary>
        /// Stop the interaction
        /// </summary>
        /// <returns>True is the interaction could be stopped. False otherwise</returns>
        /// <TODO>
        /// Remove. Not useful
        /// </TODO>
        bool StopInteract();
        
    }

    /// <summary>
    /// Remove because it restricts in a way the possibilities of interactable objects.
    /// Useful for cursors though
    /// </summary>
    public enum InteractableType
    {
        Interactable,
        Possessable,
        Selectable,
        Inspectable,
    }
}
