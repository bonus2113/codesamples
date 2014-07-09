using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/// <summary>
/// The ScreenManager is responsible for displaying different scenes in the scene.
/// </summary>
public class ScreenManager : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// The screen which gets enabled when the scene is loaded.
    /// </summary>
    [SerializeField] private UIScreen defaultScreen;

    /// <summary>
    /// All active (drawn) screens which are not overlays.
    /// </summary>
    private List<UIScreen> activeScreens = new List<UIScreen>(); 

    /// <summary>
    /// All overlays.
    /// </summary>
    private Stack<UIScreen> overlayScreens = new Stack<UIScreen>();

    /// <summary>
    /// All screens in the scene.
    /// </summary>
    private Dictionary<Type, UIScreen> screens = new Dictionary<Type, UIScreen>();

    public event Action<int> OverlayRemoved;
    public event Action<int> OverlayAdded;

    #endregion

    #region Public Methods
    /// <summary>
    /// Shows the screen with the given type.
    /// </summary>
    /// <param name="_screenType">Type of the screen to show.</param>
    /// <param name="_asOverlay">Define if the screen is supposed to be shown as an overlay.</param>
    public void ShowScreen(Type _screenType, bool _asOverlay = false)
    {
        UIScreen screenToShow = GetScreen(_screenType);

        if (screenToShow == null)
        {
            Debug.LogWarning(String.Format("Can not show screen of type '{0}'. It doesn't exist!", _screenType));
            return;
        }

        if (activeScreens.Contains(screenToShow) || overlayScreens.Contains(screenToShow))
        {
            return;
        }

        if (_asOverlay)
        {
            PushOverlay(screenToShow);
        }
        else
        {
            activeScreens.Add(screenToShow);
        }

        screenToShow.SetInputActive(true);
        screenToShow.gameObject.SetActive(true);
        screenToShow.OnShow();
    }

    /// <summary>
    /// Hide the screen with the given type. If the screen is the top most overlay, pop it.
    /// </summary>
    /// <param name="_screenType">Type of the screen to hide.</param>
    public void HideScreen(Type _screenType)
    {
        UIScreen screenToHide = GetScreen(_screenType);

        if (screenToHide == null)
        {
            Debug.LogWarning(String.Format("Can not hide screen of type '{0}'. It doesn't exist!", _screenType));
            return;
        }

        bool isUnderActive = activeScreens.Contains(screenToHide);
        bool isOverlay = overlayScreens.Count != 0 && overlayScreens.Peek() == screenToHide;

        if (!isUnderActive && !isOverlay)
        {
            Debug.LogWarning(String.Format("Can not hide screen of type '{0}'. It's not active or it is an overlay!", _screenType));
            return;
        }

        if (isOverlay)
        {
            PopOverlay();
        }
        else
        {
            activeScreens.Remove(screenToHide);
            SetInactive(screenToHide);
            screenToHide.gameObject.SetActive(false);
            screenToHide.OnHide();
        }
    }

    /// <summary>
    /// Pop the top most overlay.
    /// </summary>
    public void PopOverlay()
    {
        if (overlayScreens.Count == 0)
        {
            Debug.LogError("Can not pop an overlay, there are no active ovelays!");
            return;
        }

        UIScreen screenToHide = overlayScreens.Pop();
        if (overlayScreens.Count > 0)
        {
            overlayScreens.Peek().SetInputActive(true);
        }
        else
        {
            activeScreens.ForEach(s => s.SetInputActive(true));
        }

        SetInactive(screenToHide);
        screenToHide.gameObject.SetActive(false);
        screenToHide.OnHide();

        if (OverlayRemoved != null)
        {
            OverlayRemoved(this.overlayScreens.Count);
        }
    }
    #endregion

    #region Unity Methods
    /// <summary>
    /// Find all screens and show the default one.
    /// </summary>
    private void Awake()
    {
        var foundScreens = GetComponentsInChildren<UIScreen>(true);
        foreach (var screen in foundScreens)
        {
            screen.gameObject.SetActive(false);
            screens.Add(screen.GetType(), screen);
        }

        ShowScreen(defaultScreen.GetType());
    }

    /// <summary>
    /// Use a unified key to go back (Escape is the back button on Android)
    /// </summary>
    private void Update()
    {
        if(overlayScreens.Count > 0 && Input.GetKeyDown(KeyCode.Escape))
            PopOverlay();

    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Get the screen with the given type.
    /// </summary>
    /// <param name="_screenType">Type of the screen to get.</param>
    /// <returns>The screen with the given type, null if it doesn't exist.</returns>
    private UIScreen GetScreen(Type _screenType)
    {
        if (screens.ContainsKey(_screenType))
            return screens[_screenType];
        return null;
    }

    /// <summary>
    /// Push a screen to the overlay stack.
    /// </summary>
    /// <param name="_screen"></param>
    private void PushOverlay(UIScreen _screen)
    {
        if (overlayScreens.Count == 0)
        {
            activeScreens.ForEach(s => s.SetInputActive(false));
        }
        else
        {
            overlayScreens.Peek().SetInputActive(false);
        }

        overlayScreens.Push(_screen);

        if (this.OverlayAdded != null)
        {
            OverlayAdded(this.overlayScreens.Count);
        }
    }

    /// <summary>
    /// Disable input for the given screen and remove it from the list of active screen.
    /// </summary>
    /// <param name="_screen">Screen to set inactive.</param>
    private void SetInactive(UIScreen _screen)
    {
        _screen.SetInputActive(false);
        activeScreens.Remove(_screen);
    }

    #endregion
}
