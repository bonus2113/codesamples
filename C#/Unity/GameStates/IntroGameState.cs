using UnityEngine;
using System.Collections;

public class IntroGameState : GameStateBase
{
    public override GameStateType Type
    {
        get { return GameStateType.Intro; }
    }

    private IllnessContainer illnessContainer;
    private Villager villager;

    [SerializeField] private Collider2D workingAreaCollider;

    protected override void OnAwake()
    {
        illnessContainer = FindObjectOfType<IllnessContainer>();
        villager = FindObjectOfType<Villager>();
    }

    protected override void OnEnterState()
    {
        CreateVillager();
        workingAreaCollider.enabled = false;
    }

    protected override void OnExitState()
    {
        workingAreaCollider.enabled = true;
    }

    private void CreateVillager()
    {
        GameManager.ActiveModel.CurrentIllness = illnessContainer.GetRandomIllness();
        GameManager.ActiveModel.IllBodyPart = GetRandomBodyPartType();
        villager.SetIllness(GameManager.ActiveModel.CurrentIllness, GameManager.ActiveModel.IllBodyPart);
        villager.StartMove(OnIntroFinished);
    }

    private BodyPartType GetRandomBodyPartType()
    {
        return (BodyPartType)Random.Range(0, (int)BodyPartType.ENUM_COUNT);
    }

    private void OnIntroFinished()
    {
        GameManager.GoTo(GameStateType.InspectingVillager);
    }
}
