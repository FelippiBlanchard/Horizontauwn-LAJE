using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coletavel : MonoBehaviour
{
    public int indice;
    public Inventario inventario;
       void Awake()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventario.Coletar(indice);
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if(indice == 8)
        {
            GetComponent<AudioSource>().DOFade(0f, 1f).SetEase(Ease.InQuad);
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
