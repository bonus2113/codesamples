using UnityEngine;

/// <summary>
/// Shows a set screen when Execute is called.
/// </summary>
public class UIShowScreen : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// The screen that will be shown.
    /// </summary>
    public UIScreen ScreenToShow;

    /// <summary>
    /// The screen that will be hidden. Can be null.
    /// </summary>
    public UIScreen ScreenToHide;

    /// <summary>
    /// Define if the screen is supposed to be shown as an overlay.
    /// </summary>
    public bool AsOverlay = false;
    #endregion

    #region Fields
    /// <summary>
    /// Current screen manager.
    /// </summary>
    private ScreenManager screenManager;
    #endregion

    #region Public Methods
    /// <summary>
    /// Shows the set screen.
    /// </summary>
    public void Execute()
    {
        if(ScreenToHide != null)
            screenManager.HideScreen(ScreenToHide.GetType());

        if(ScreenToShow != null)
            screenManager.ShowScreen(ScreenToShow.GetType(), AsOverlay);
    }
    #endregion

    #region Unity Methods
    /// <summary>
    /// Get the current ScreenManager
    /// </summary>
    private void Start()
    {
        screenManager = FindObjectOfType<ScreenManager>();
    }
    #endregion
}
