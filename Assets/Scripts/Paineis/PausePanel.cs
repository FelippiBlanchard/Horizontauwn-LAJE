using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private const float animationDefaultLength = 0.5f;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private string menu;
    public bool isPaused;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressPause();
        }
    }
    public void PressPause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
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

    }
    public void Resume()
    {
        float duration = animationDefaultLength;
        canvasGroup.DOFade(0, duration).Restart();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
