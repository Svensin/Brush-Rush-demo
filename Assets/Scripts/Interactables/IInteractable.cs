namespace Interactables
{
   /// <summary>
   /// Інтерфейс, що відповідає за ефекти всіх об'єктів, з якими може взаємодіяти пензлик   
   /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Реалізація ефекту, який надається об'єктом
        /// </summary>
        void Effect();

        /// <summary>
        /// Реалізація методу, коли ефект об'єкту треба деактивувати
        /// </summary>
        void Disable();
    }
}

