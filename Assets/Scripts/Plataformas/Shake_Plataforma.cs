using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake_Plataforma : MonoBehaviour
{
    [Header("Características")]
    [SerializeField] private float distancia;
    [SerializeField] private float tempoParaRealizar;
    private float initialPosition;
    private SpriteRenderer sr;
    private Collider2D coll;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<Collider2D>();
        initialPosition = transform.position.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled)
        {
            var col = collision.collider;
            if (col.gameObject.CompareTag("Player"))
            {
                col.transform.SetParent(transform);
                transform.DOMoveY(initialPosition - distancia, tempoParaRealizar);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var col = collision.collider;
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
            transform.DOMoveY(initialPosition, tempoParaRealizar);
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
