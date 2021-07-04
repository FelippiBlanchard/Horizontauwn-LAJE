using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    private const float animationDefaultLength = 0.5f;

    [SerializeField] private float tempoFadeEntreBackground;
    [SerializeField] private float tempoIntervaloEntreBackgrounds;
    [SerializeField] private float tempoFadeAdiantado;

    [Range(0f, 1f)]
    [SerializeField] private float alpha;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private string menu;
    [SerializeField] private Image backgroundAzul;
    [SerializeField] private Image backgroundAmarelo;
    public bool isPaused;

    private Coroutine corrotina;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.SomPause();
            PressPause();
        }
    }
    public void PressPause()
    {
        if (isPaused)
        {
            Resume();
            StopCoroutine(corrotina);
        }
        else
        {
            Pause();
            corrotina = StartCoroutine(IChangeBackground());
        }
    }
    public void Pause()
    {
        float duration = animationDefaultLength;
        canvasGroup.DOFade(1, duration).SetUpdate(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        Time.timeScale = 0f;
        isPaused = true;
        SoundManager.instance.StopbackgroundFase();
        

    }
    public void Resume()
    {
        float duration = animationDefaultLength;
        canvasGroup.DOFade(0, duration).Restart();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        Time.timeScale = 1f;
        isPaused = false;
        SoundManager.instance.ResumebackgroundFase();
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(menu);
    }

    IEnumerator IChangeBackground()
    {
        backgroundAzul.DOFade(alpha, 0.1f).SetUpdate(true); ;
        backgroundAmarelo.DOFade(0f, 0.1f).SetUpdate(true); ;


        while (isPaused)
        {
            yield return new WaitForSecondsRealtime(tempoIntervaloEntreBackgrounds);

            backgroundAzul.DOFade(0.1f, tempoFadeEntreBackground).SetUpdate(true); ;
            backgroundAmarelo.DOFade(alpha, tempoFadeEntreBackground - tempoFadeAdiantado).SetUpdate(true); ;

            yield return new WaitForSecondsRealtime(tempoFadeEntreBackground);

            yield return new WaitForSecondsRealtime(tempoIntervaloEntreBackgrounds);

            backgroundAzul.DOFade(alpha, tempoFadeEntreBackground - tempoFadeAdiantado).SetUpdate(true); ;
            backgroundAmarelo.DOFade(0.1f, tempoFadeEntreBackground).SetUpdate(true); ;
            yield return new WaitForSecondsRealtime(tempoFadeEntreBackground);

        }
    }
}
