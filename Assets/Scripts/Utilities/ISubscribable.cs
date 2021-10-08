namespace Utilities
{
    /// <summary>
    /// Інтерфейс, для підписання/відписання змінних
    /// </summary>
    interface ISubscribable
    {
        /// <summary>
        /// Скидає значення необхідних змінних
        /// </summary>
        public void Unsubscribe();
        /// <summary>
        /// Визначає значення необіхдних змінних
        /// </summary>
        public void Subscribe();
    }
}