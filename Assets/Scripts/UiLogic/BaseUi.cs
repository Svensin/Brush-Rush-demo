using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base class for all panels, which can be opened/closed
/// </summary>
public abstract class BaseUi : MonoBehaviour
{
    /// <summary>
    /// Panel which can be opened/closed
    /// </summary>
    [SerializeField] protected Canvas panelCanvas;
    
    /// <summary>
    /// Button which opens/closes the panel
    /// </summary>
    [SerializeField] protected Button closePanelButton;

    /// <summary>
    /// Closes <see cref="panelCanvas"/>.
    /// </summary>
    public abstract void Close();

    /// <summary>
    /// Opens <see cref="panelCanvas"/>.
    /// </summary>
    public abstract void Open();

}