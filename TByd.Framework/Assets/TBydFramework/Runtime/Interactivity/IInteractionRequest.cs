using System;

namespace TBydFramework.Runtime.Interactivity
{
    /// <summary>
    /// Represents a request from user interaction.
    /// </summary>
    public interface IInteractionRequest
    {
        /// <summary>
        /// Fired when the interaction is needed.
        /// </summary>
        event EventHandler<InteractionEventArgs> Raised;
    }
}
