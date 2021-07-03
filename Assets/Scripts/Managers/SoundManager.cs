using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private AudioSource backgroundMenu;
    [SerializeField] private AudioSource botaoEscolher;
    [SerializeField] private AudioSource botaoTrocar;

    [Header("Fase")]
    [SerializeField] private AudioSource backgroundFase;

    public static SoundManager instance;

    public AudioMixer mixer;

    private void Start()
    {
        instance = this;
    }
    public void SomBotaoEscolher()
    {
        botaoEscolher.Play();
    }

    public void AbaixarSonsMenu()
    {
        backgroundMenu.DOFade(0f, 1f).SetEase(Ease.InQuad);
        botaoEscolher.DOFade(0f, 0.5f).SetEase(Ease.InQuad);
        botaoTrocar.DOFade(0f, 0.5f).SetEase(Ease.InQuad);
    }
    public void StopbackgroundFase()
    {
        backgroundFase.Pause();
    }
    public void ResumebackgroundFase()
    {
        backgroundFase.Play();
    }

    public void SetLevel(float slidervalue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(slidervalue) * 20);
    }

}
