using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Events")]
    [SerializeField] private FadeEventChannelSO fadeOut;
    [SerializeField] private FadeEventChannelSO fadeIn;

    private Coroutine _routine;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        if (fadeOut) fadeOut.OnRaised += HandleFadeOut;
        if (fadeIn) fadeIn.OnRaised += HandleFadeIn;
    }

    private void OnDisable()
    {
        if (fadeOut) fadeOut.OnRaised -= HandleFadeOut;
        if (fadeIn) fadeIn.OnRaised -= HandleFadeIn;
    }

    private void HandleFadeOut(float duration)
    {
        StartFade(1f, duration); // alpha -> 1 (preto)
    }

    private void HandleFadeIn(float duration)
    {
        StartFade(0f, duration); // alpha -> 0 (transparente)
    }

    private void StartFade(float targetAlpha, float duration)
    {
        if (_routine != null) StopCoroutine(_routine);
        _routine = StartCoroutine(FadeRoutine(targetAlpha, duration));
    }

    private IEnumerator FadeRoutine(float target, float duration)
    {
        float start = canvasGroup.alpha;
        float t = 0f;

        // Evita divisão por zero
        duration = Mathf.Max(0.01f, duration);

        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // unscaled pra não afetar se você pausar o jogo
            canvasGroup.alpha = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }

        canvasGroup.alpha = target;
    }
}
