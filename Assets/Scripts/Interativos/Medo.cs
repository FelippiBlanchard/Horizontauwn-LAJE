using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Medo : MonoBehaviour
{

    [Header("Configurações")]
    [SerializeField] private float tempoParaSumir;
    [SerializeField] private float tempoSubidaFragmento;
    [SerializeField] private float distanciaSubidaFragmento;
    [SerializeField] private GameObject fragmento;
    [SerializeField] public int indice;


    public void Interagir()
    {
        StartCoroutine(IAnimacaoSumir());
    }
    IEnumerator IAnimacaoSumir()
    {
        //chama a animação
        var frag = Instantiate(fragmento,transform.parent);
        frag.transform.DOMoveY(distanciaSubidaFragmento, tempoSubidaFragmento);
        yield return new WaitForSeconds(tempoParaSumir);
        Destroy(gameObject);
    }
}
