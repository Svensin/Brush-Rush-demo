namespace Interactables
{
   /// <summary>
   /// ���������, �� ������� �� ������ ��� ��'����, � ����� ���� ��������� �������   
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

