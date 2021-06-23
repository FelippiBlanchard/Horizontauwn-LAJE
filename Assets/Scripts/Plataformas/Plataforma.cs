using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private float distanciaHorizontal;
    [SerializeField] private float distanciaVertical;
    [SerializeField] private float tempoPraPercorrer;
    [SerializeField] private float intervalo;


    private Vector3 posicaoInicial;

    private void Start()
    {
        posicaoInicial = transform.position;
        StartCoroutine(IMovimentar());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.collider;
        if(col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
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
}
