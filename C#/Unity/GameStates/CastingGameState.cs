using UnityEngine;
using System.Collections;

public class CastingGameState : GameStateBase
{
    [SerializeField]
    private TimingManager timingManager;
    [SerializeField]
    private GameObject overlayLight;
    [SerializeField]
    private float survivalTime = 20.0f;

    private float timer = 0;

    private bool updating = false;

    public override GameStateType Type
    {
        get { return GameStateType.CastingSpell; }
    }

    protected override void OnEnterState()
    {
        StartCoroutine(WaitForZoom());
        overlayLight.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    protected override void OnExitState()
    {
        overlayLight.GetComponent<Animator>().SetTrigger("FadeIn");
        timingManager.GetComponent<UIPlayTween>().Play(false);
        GameManager.ActiveModel.SpellHeuristicValue += timingManager.GetHeuristicValue();
        timingManager.StopPlayback();
        updating = false;
    }

    protected override void OnUpdate()
    {
        if (updating)
        {
            base.OnUpdate();
            timer += Time.deltaTime;
            if (timer > survivalTime)
            {
                GameManager.ActiveModel.SuccededCasting = true;
                GameObject.FindObjectOfType<Villager>().Cure(GameManager.ActiveModel.IllBodyPart);
                GameManager.GoTo(GameStateType.ShowResult);
            }
            else if (timingManager.MissedSymbols > 0 && timingManager.MissedSymbols >= GameManager.ActiveModel.PlacedCandlesCount)
            {
                GameManager.ActiveModel.SuccededCasting = false;
                GameObject.FindObjectOfType<Villager>().DiseaseRandomPart(GameManager.ActiveModel.CurrentIllness);
                GameManager.GoTo(GameStateType.ShowResult);
            }
        }
    }

    private IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(1.0f);
        timingManager.gameObject.SetActive(true);
        timingManager.GetComponent<UIPlayTween>().Play(true);
        timingManager.PlaySequence(GameManager.ActiveModel.CurrentIllness.Sequence, 1.0f - GameManager.ActiveModel.SpellHeuristicValue);
        yield return new WaitForSeconds(1.0f);
        updating = true;
    }
}
