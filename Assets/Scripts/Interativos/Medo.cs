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
    [SerializeField] private float tempoFadeSomMedo;
    [SerializeField] private GameObject fragmento;
    [SerializeField] public int indiceMedo;

    [Header("Caminho pra Volta")]
    [SerializeField] private Caminho_Plataformas caminho_Plataformas;


    public void Interagir()
    {
        StartCoroutine(IAnimacaoSumir());
    }
    IEnumerator IAnimacaoSumir()
    {
        GetComponent<AudioSource>().DOFade(0f, tempoFadeSomMedo).SetEase(Ease.InQuad);

        //chama a animação

        caminho_Plataformas.AtivarTudo();
        
        GameManager.instance.EventoMedoLiberado(indiceMedo);

        var frag = Instantiate(fragmento,transform.parent);
        frag.transform.DOMoveY(transform.position.y + distanciaSubidaFragmento, tempoSubidaFragmento);
        
        yield return new WaitForSeconds(tempoParaSumir);
        
        Destroy(gameObject);
    }
}
