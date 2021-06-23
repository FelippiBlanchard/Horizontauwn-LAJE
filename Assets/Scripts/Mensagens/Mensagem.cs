using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class Mensagem : MonoBehaviour
{
    [SerializeField] private string texto;
    [SerializeField] private float fade;
    [SerializeField] private float duracao;
    [SerializeField] private float tempoMin;
    [SerializeField] TextMeshProUGUI caixaDeTexto;

    private float contador;
    private bool contando;
    private Coroutine coroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coroutine = StartCoroutine(IMostrarMensagem());
        }
    }
    private void Update()
    {
        if (contando)
        {
            contador += Time.deltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(coroutine);

            if (contador < tempoMin)
            {
                StartCoroutine(IHide());
            }
            else
            {
                Hide();
            }

            try
            {
                GetComponentInParent<Caminho>().ProximaMensagem();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
    public void Show()
    {
        caixaDeTexto.alpha = 0.01f;
        caixaDeTexto.text = texto;
        caixaDeTexto.CrossFadeAlpha(255f, fade, false);
        contando = true;
    }
    public void Hide()
    {
        caixaDeTexto.CrossFadeAlpha(0f, fade, false);
        contando = false;
    }
    IEnumerator IMostrarMensagem()
    {
        Show();
        yield return new WaitForSeconds(duracao);
        Hide();
    }
    IEnumerator IHide()
    {
        yield return new WaitForSeconds(tempoMin);
        Hide();
    }
}
