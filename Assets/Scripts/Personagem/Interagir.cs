using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interagir : MonoBehaviour
{
    [Header("Configurações")]
    public Inventario inventario;
    public List<GameObject> fragmentos;

    private bool interagivel;
    private GameObject objetoInteragindo;



    private void Update()
    {
        AtivarInteracao();
    }

    public void AtivarInteracao()
    {
        if (Input.GetKeyDown("k") && interagivel)
        {
            if (objetoInteragindo.layer == 8) //medo
            {
                objetoInteragindo.GetComponent<Collider2D>().enabled = false;
                InteracaoMedo(objetoInteragindo);
            }
            else
            {
                if (objetoInteragindo.layer == 9)//porta
                {
                    StartCoroutine(InteracaoPorta(objetoInteragindo));
                }
                else
                {
                    if (objetoInteragindo.layer == 10)//flor
                    {
                        objetoInteragindo.GetComponent<Collider2D>().enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objetoInteragindo = collision.gameObject;
        interagivel = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interagivel = false;
    }

    public void InteracaoMedo(GameObject gameobjectMedo)
    {
        var medo = gameobjectMedo.GetComponent<Medo>();
        medo.Interagir();
    }
    IEnumerator InteracaoPorta(GameObject gameobjectPorta)
    {
        var porta = gameobjectPorta.GetComponent<Porta>();

        for (int i = 0; i < (inventario.inventario.Count - 1); i++)
        {
            if (inventario.inventario[i])
            {
                porta.fragmentos[i] = true;
                inventario.inventario[i] = false;
                var fragmento = Instantiate(fragmentos[i], porta.transform);
                fragmento.transform.DOMove(porta.posicaoFragmentos[i].position, porta.tempoAnimacao);
                yield return new WaitForSeconds(porta.intervaloEntreAnimacao);
            }
        }
        porta.VerificarColecao();
    }
}
