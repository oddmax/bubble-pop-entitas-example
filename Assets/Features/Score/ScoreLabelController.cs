using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreLabelController : MonoBehaviour, IAnyScoreListener
{
    public TextMeshProUGUI label;
    public Vector3 defaultScale;
    public Vector3 punchScale;

    void Start()
    {
        var listener = Contexts.sharedInstance.gameState.CreateEntity();
        listener.AddAnyScoreListener(this);
        defaultScale = transform.localScale;
    }

    public void OnAnyScore(GameStateEntity entity, int value)
    {
        label.text = value.ToString();
        transform.DOPunchScale(punchScale, 0.3f, 5, 0.3f);
    }
}
