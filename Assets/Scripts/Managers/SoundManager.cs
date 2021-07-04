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
    [SerializeField] private AudioSource pause;

    [Header("Fase")]
    [SerializeField] private AudioSource backgroundFase;
    [SerializeField] private AudioSource medoLiberado;

    [Header("Coletáveis")]
    [SerializeField] private AudioSource coletarPecinha;
    [SerializeField] private AudioSource coletarJoia;

    [SerializeField] private AudioSource creditos;

    public static SoundManager instance;

    public AudioMixer mixer;

    private void Start()
    {
        instance = this;
        backgroundFase.DOFade(0.5f, 6f).SetUpdate(true).SetEase(Ease.InQuad);

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

        backgroundFase.DOFade(0.01f, 0f).SetUpdate(true);
        backgroundFase.Pause();
    }
    public void ResumebackgroundFase()
    {
        backgroundFase.Play();
        backgroundFase.DOFade(0.5f, 2f).SetUpdate(true);
    }

    public void ColetarPecinha()
    {
        coletarPecinha.Play();
    }

    public void ColetarColecionavel()
    {
        coletarPecinha.DOFade(0.15f, 0f);
        coletarPecinha.Play();
        coletarPecinha.DOFade(0.25f, 0f);
    }

    public void ColetarJoia()
    {
        coletarJoia.Play();
    }
    public void SomPause()
    {
        pause.Play();
    }

    public void SetLevel(float slidervalue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(slidervalue) * 20);
    }

    public void Creditos()
    {
        backgroundFase.Stop();
        creditos.Play();
    }
    public void MedoLiberado()
    {
        medoLiberado.Play();
    }

}
