namespace Utilities
{
    /// <summary>
    /// Interface for subscribing/unsubscribing of variables
    /// </summary>
    interface ISubscribable
    {
        /// <summary>
        /// Set value of wanted values to default
        /// </summary>
        public void Unsubscribe();
        /// <summary>
        /// Defines value of wanted variables
        /// </summary>
        public void Subscribe();
    }
}