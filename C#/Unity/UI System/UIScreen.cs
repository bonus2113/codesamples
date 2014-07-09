using UnityEngine;
using System.Collections;

public class UIScreen : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// Gets if the screen is currently drawn.
    /// </summary>
    public bool IsActive { get; private set; }
    #endregion

    #region Fields
    /// <summary>
    /// This screens ScreenManager.
    /// </summary>
    protected ScreenManager Manager { get; private set; }
    #endregion

    #region Public Methods
    /// <summary>
    /// Sets the active value for the screen. If it's inactive, it won't recieve any input.
    /// </summary>
    /// <param name="_active">Value to the the active flag to.</param>
    public void SetInputActive(bool _active)
    {
        var colliders = GetComponentsInChildren<Collider>(true);
        for (int i = colliders.Length - 1; i >= 0; i--)
            colliders[i].enabled = _active;

        if (_active)
        {
            OnInputEnable();
        }
    }

    /// <summary>
    /// Closes this screen.
    /// </summary>
    public void CloseScreen()
    {
        if (Manager == null)
        {
            Manager = GameObject.FindObjectOfType<ScreenManager>();

            if (Manager != null)
            {
                Debug.LogWarning("ScreenManager reference was null. Found it using FindObjectOfType", this);
            }
            else
            {
                Debug.LogError("Did not find a ScreenManager in the scene.");
                return;
            }

        }
        Manager.HideScreen(GetType());
    }

    /// <summary>
    /// Called when the screen is shown. Do things like transitions in here.
    /// </summary>
    public virtual void OnShow()
    {
        IsActive = true;
    }

    /// <summary>
    /// Called when the screen is hidden. Do things like transitions in here.
    /// </summary>
    public virtual void OnHide()
    {
        IsActive = false;
    }
    #endregion

    #region Unity Methods
    /// <summary>
    /// Get the manager for this screen.
    /// </summary>
    protected virtual void OnEnable()
    {
        Manager = FindObjectOfType<ScreenManager>();
    }

    protected virtual void OnDisable()
    {

    }
    #endregion

    #region Private Methods
    protected virtual void OnInputEnable()
    {

    }
    #endregion
}
