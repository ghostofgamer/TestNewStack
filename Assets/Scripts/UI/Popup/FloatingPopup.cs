using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingPopup : BasePopup<FloatingPopupData>
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private float moveUpDistance = 50f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fadeDuration = 0.5f;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    protected override void ShowTyped(FloatingPopupData data, System.Action<IPopup> onComplete)
    {
        valueText.text = (data.Value > 0 ? "+" : "") + data.Value;
        valueText.color = data.TextColor;
        icon.sprite = data.Icon;

        gameObject.SetActive(true);
        _canvasGroup.alpha = 1f;
        _rectTransform.localPosition = Vector3.zero;

        Vector3 endPos = _rectTransform.localPosition + Vector3.up * moveUpDistance;

        _rectTransform.DOLocalMove(endPos, duration).SetEase(Ease.OutCubic);
        _canvasGroup.DOFade(0f, fadeDuration).SetDelay(duration - fadeDuration)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke(this);
            });
    }
}