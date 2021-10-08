namespace Interactables
{
   /// <summary>
   /// ���������, �� ������� �� ������ ��� ��'����, � ����� ���� ��������� �������   
   /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// ��������� ������, ���� �������� ��'�����
        /// </summary>
        void Effect();

        /// <summary>
        /// ��������� ������, ���� ����� ��'���� ����� ������������
        /// </summary>
        void Disable();
    }
}

