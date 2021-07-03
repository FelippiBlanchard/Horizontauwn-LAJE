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

    [SerializeField] private Animator anim;



    public void Interagir()
    {
        StartCoroutine(IAnimacaoSumir());
    }
    IEnumerator IAnimacaoSumir()
    {
        GetComponent<AudioSource>().DOFade(0f, tempoFadeSomMedo).SetEase(Ease.InQuad);

        anim.SetBool("feliz", true);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f);

        caminho_Plataformas.AtivarTudo();
        
        GameManager.instance.EventoMedoLiberado(indiceMedo);

        fragmento.GetComponent<SpriteRenderer>().DOFade(1f, tempoSubidaFragmento);
        fragmento.transform.DOMoveY(transform.position.y + distanciaSubidaFragmento, tempoSubidaFragmento);

        yield return new WaitForSeconds(tempoSubidaFragmento);
        fragmento.GetComponent<Collider2D>().enabled = true;
        fragmento.transform.SetParent(transform.parent);
        yield return new WaitForSeconds(tempoParaSumir);
        
        Destroy(gameObject);
    }
}
