using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource background;
    [SerializeField] private AudioSource botaoEscolher;
    [SerializeField] private AudioSource botaoTrocar;

    public void SomBotaoEscolher()
    {
        botaoEscolher.Play();
    }

    public void AbaixarSonsMenu()
    {
        background.DOFade(0f, 1f).SetEase(Ease.InQuad);
        botaoEscolher.DOFade(0f, 0.5f).SetEase(Ease.InQuad);
        botaoTrocar.DOFade(0f, 0.5f).SetEase(Ease.InQuad);
    }
}
