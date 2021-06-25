using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Loop_Plataforma : MonoBehaviour
{
    [Header("Características")]
    [SerializeField] private float distanciaHorizontal;
    [SerializeField] private float distanciaVertical;
    [SerializeField] private float tempoPraPercorrer;
    [SerializeField] private float intervalo;
    [SerializeField] private float tempoParaComecar;
    private SpriteRenderer sr;
    private Collider2D coll;

    private Vector3 posicaoInicial;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<Collider2D>();
        posicaoInicial = transform.position;
        StartCoroutine(IMovimentar());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled)
        {
            var col = collision.collider;
            if (col.gameObject.CompareTag("Player"))
            {
                col.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var col = collision.collider;
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
        }
    }

    public void MovePlataform(Vector3 position, float distanciaH, float distanciaV)
    {

            transform.DOMoveX(position.x + distanciaH, tempoPraPercorrer);
            transform.DOMoveY(position.y + distanciaV, tempoPraPercorrer);

    }
    IEnumerator IMovimentar()
    {
        yield return new WaitForSeconds(tempoParaComecar);  
        while (true)
        {
            yield return new WaitForSeconds(intervalo);
            MovePlataform(posicaoInicial, distanciaHorizontal, distanciaVertical);
            yield return new WaitForSeconds(tempoPraPercorrer);
            yield return new WaitForSeconds(intervalo);
            MovePlataform(posicaoInicial, 0, 0);
            yield return new WaitForSeconds(tempoPraPercorrer);
        }
    }
    public void Desativar()
    {
        sr.DOFade(0f, 1);
        coll.enabled = false;
    }
    public void Ativar()
    {
        sr.enabled = true;
        sr.DOFade(0f, 0f);
        sr.DOFade(1f, 1);
        coll.enabled = true;
    }
}
