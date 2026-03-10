using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Attention
{
    public class AttentionHintPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hintText;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Coroutine _fadeRoutine;

        private const float Delay = 0.3f;
        private const float Duration = 0.65f;

        public void Show(string message)
        {
            gameObject.SetActive(true);

            _hintText.text = message;
            _canvasGroup.alpha = 1f;

            if (_fadeRoutine != null)
                StopCoroutine(_fadeRoutine);

            _fadeRoutine = StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(Delay);

            float time = 0f;

            while (time < Duration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / Duration);
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}