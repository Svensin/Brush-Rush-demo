using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Базовий клас для усіх панелей, які відкривають та закриваються.
/// </summary>
public abstract class BaseUi : MonoBehaviour
{
    /// <summary>
    /// Панель яка закривається/відкривається.
    /// </summary>
    [SerializeField] protected Canvas panelCanvas;
    
    /// <summary>
    /// Кнопка яка відкриває/закриває панель.
    /// </summary>
    [SerializeField] protected Button closePanelButton;

    /// <summary>
    /// Відкриває <see cref="panelCanvas"/>.
    /// </summary>
    public abstract void Close();

    /// <summary>
    /// Закриває <see cref="panelCanvas"/>.
    /// </summary>
    public abstract void Open();

}