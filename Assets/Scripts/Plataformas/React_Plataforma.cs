using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class React_Plataforma : MonoBehaviour
{
    [Header("Características")]
    [SerializeField] private float distanciaHorizontal;
    [SerializeField] private float distanciaVertical;
    [SerializeField] private float tempoPraPercorrer;
    [SerializeField] private bool volta;
    private Vector3 posicaoInicial;
    private SpriteRenderer sr;
    private Collider2D coll;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<Collider2D>();
        posicaoInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled)
        {
            var col = collision.collider;
            if (col.gameObject.CompareTag("Player"))
            {
                MovePlataform(posicaoInicial, distanciaHorizontal, distanciaVertical);
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
            if (volta)
                 {
                     MovePlataform(posicaoInicial, 0, 0);
                 }
        }
    }

    public void MovePlataform(Vector3 position, float distanciaH, float distanciaV)
    {

        transform.DOMoveX(position.x + distanciaH, tempoPraPercorrer);
        transform.DOMoveY(position.y + distanciaV, tempoPraPercorrer);

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
