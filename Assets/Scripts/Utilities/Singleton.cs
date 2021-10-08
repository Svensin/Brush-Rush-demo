using UnityEngine;

public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    /// <summary>
    /// Повертає посилання на єдний об'єкт класу
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
        //шукаємо чи є ще gameobject'и з таким компонентом
        var curObjectScripts = FindObjectsOfType<T>();

        if (curObjectScripts.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }
}
