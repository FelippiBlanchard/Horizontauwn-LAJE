using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fade_Plataforma : MonoBehaviour
{
    [Header("Características")]
    [SerializeField] private float tempoDeFade;
    [SerializeField] private float intervaloOpaco;
    [SerializeField] private float intervaloTransparente;
    [SerializeField] private float tempoParaComecar;
    private SpriteRenderer sr;
    private Collider2D coll;
    private bool desactivated;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<Collider2D>();
        StartCoroutine(IFadeLoop());
    }

    IEnumerator FadeTransparente()
    {
        sr.DOFade(0f, tempoDeFade);
        yield return new WaitForSeconds(tempoDeFade*0.8f);
        coll.enabled = false;
        yield return new WaitForSeconds(tempoDeFade * 0.2f);
    }
    IEnumerator FadeOpaco()
    {
        sr.DOFade(1f, tempoDeFade);
        coll.enabled = true;
        yield return new WaitForSeconds(tempoDeFade);
    }

    IEnumerator IFadeLoop()
    {
        yield return new WaitForSeconds(tempoParaComecar);
        while (!desactivated)
        {
                yield return FadeOpaco();
                yield return new WaitForSeconds(intervaloOpaco);

                yield return FadeTransparente();
                yield return new WaitForSeconds(intervaloTransparente);

        }
    }

    public void Desativar()
    {
        desactivated = true;
    }
    public void Ativar()
    {
        desactivated = false;
        StartCoroutine(IFadeLoop());
    }
}
