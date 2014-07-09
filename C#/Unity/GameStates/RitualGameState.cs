using UnityEngine;
using System.Collections;

public class RitualGameState : GameStateBase
{
    [SerializeField] private GameObject gameplayRoot;
    [SerializeField]
    private AudioSource humSource;

    private RitualGameManager ritualGameManager;

    public override GameStateType Type
    {
        get { return GameStateType.Ritual; }
    }

    protected override void OnEnterState()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ZoomIn");
        StartCoroutine(WaitForZoom());
        humSource.volume = 0.15f;
    }

    protected override void OnExitState()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("ZoomOut");
        CandleManager candleManager = gameplayRoot.GetComponentInChildren<CandleManager>();
        candleManager.transform.parent = ritualGameManager.m_PersistantRitual.transform;
        candleManager.ClearButton();
        gameplayRoot.SetActive(false);
        GameManager.ActiveModel.PlacedCandlesCount = ritualGameManager.PlacedCandlesCount;
        GameManager.ActiveModel.SpellHeuristicValue = ritualGameManager.GetHeuristicValue();
        humSource.volume = 0.0f;
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.ritualGameManager != null)
        {
            GameManager.GoTo(GameStateType.CastingSpell);
        }
    }

    private IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(2.0f);
        gameplayRoot.SetActive(true);
        ritualGameManager = gameplayRoot.GetComponentInChildren<RitualGameManager>();
    }
}
