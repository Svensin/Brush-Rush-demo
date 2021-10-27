namespace Interactables
{
   /// <summary>
   /// Інтерфейс, що відповідає за ефекти всіх об'єктів, з якими може взаємодіяти пензлик   
   /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Activates effect of interactable
        /// </summary>
        void Effect();

        /// <summary>
        /// Starts interactable disappear animation 
        /// </summary>
        void Disable();
    }
}

