using UnityEngine;

public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    /// <summary>
    /// ������� ��������� �� ����� ��'��� �����
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        //������ �� � �� gameobject'� � ����� �����������
        var curObjectScripts = FindObjectsOfType<T>();

        if (curObjectScripts.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
