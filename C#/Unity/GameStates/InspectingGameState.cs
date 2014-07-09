using UnityEngine;
using System.Collections;

public class InspectingGameState : GameStateBase 
{
    [SerializeField]
    private Collider2D workingAreaCollider;

    public override GameStateType Type
    {
        get { return GameStateType.InspectingVillager; }
    }

    protected override void OnExitState()
    {
        workingAreaCollider.enabled = false;
    }
}
