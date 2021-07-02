using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeDaScene;
    [SerializeField] private float tempoStart;
    [SerializeField] private GameObject backgroundTransition;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private SoundManager soundmanager;

    public void Start()
    {
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        StartCoroutine(IStart());
    }
    IEnumerator IStart()
    {
        soundmanager.AbaixarSonsMenu();
        canvasGroup.alpha = 1;
        backgroundTransition.transform.DOScale(2, tempoStart*0.5f);
        yield return new WaitForSeconds(tempoStart);
        SceneManager.LoadScene(nomeDaScene);
    }
    public void ExitGame()
    {
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
