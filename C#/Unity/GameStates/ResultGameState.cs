using UnityEngine;
using System.Collections;

public class ResultGameState : GameStateBase
{
    [SerializeField] private float successfulThreshold = 0.5f;
    [SerializeField] private UILabel spellLabel;

    public override GameStateType Type
    {
        get { return GameStateType.ShowResult; }
    }

    protected override void OnEnterState()
    {
        StartCoroutine(WaitForZoom());
    }

    private IEnumerator WaitForZoom()
    {
        yield return new WaitForSeconds(2.0f);
        spellLabel.gameObject.SetActive(true);
        if (GameManager.ActiveModel.SuccededCasting)
        {
            SpellSuccessful();
        }
        else
        {
            SpellFailed();
        }
    }

    private void SpellSuccessful()
    {
        spellLabel.text = "SPELL SUCCESSFUL!";
    }

    private void SpellFailed()
    {
        spellLabel.text = "SPELL FAILED!";
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
