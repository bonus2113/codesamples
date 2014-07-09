using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public class GameStateBase : MonoBehaviour
{
    public bool IsActive { get; private set; }
    public GameManager GameManager { get; private set; }
    public virtual GameStateType Type { get { return GameStateType.Intro; } }

    public void EnterState()
    {
        IsActive = true;
        Debug.Log("Entered Game State: " + Type.ToString());
        OnEnterState();
    }

    public void ExitState()
    {
        IsActive = false;
        Debug.Log("Exited Game State: " + Type.ToString());
        OnExitState();
    }

    protected virtual void OnEnterState()
    {

    }

    protected virtual void OnExitState()
    {
        
    }

    protected virtual void OnUpdate()
    {
        
    }

    protected virtual void OnAwake()
    {
        
    }

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        GameManager.AddState(Type, this);
        OnAwake();
    }

    private void Update()
    {
        if (IsActive)
        {
            OnUpdate();
        }
    }
}
