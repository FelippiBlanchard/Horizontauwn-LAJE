using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventario : MonoBehaviour
{
    public List<bool> inventario;
    public Personagem personagem;


    [SerializeField] private float tempoFade;
    [SerializeField] private float tempoFadeFragmento;
    [SerializeField] private float tempoExibindo;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private List<Image> fragmentos;

    private bool exibindo;

    private void Start()
    {
        DesativarImages();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !exibindo)
        {
            StartCoroutine(IExibir());
        }
    }
    public void DesativarImages()
    {
        for (int i = 0; i < fragmentos.Count; i++)
        {
            fragmentos[i].DOFade(0f, 0f);
        }
    }
    public void Coletar(int indice)
    {
        StartCoroutine(IExibirColetando(indice));
        ContarPecinha(indice);
    }
    IEnumerator IExibir()
    {
        exibindo = true;
        canvasGroup.DOFade(1f, tempoFade);
        yield return new WaitForSeconds(tempoFade);
        yield return new WaitForSeconds(tempoExibindo);
        canvasGroup.DOFade(0f, tempoFade);
        yield return new WaitForSeconds(tempoFade);
        exibindo = false;
    }
    IEnumerator IExibirColetando(int indice)
    {
        exibindo = true;
        canvasGroup.DOFade(1f, tempoFade);
        yield return new WaitForSeconds(tempoFade);
        fragmentos[indice].DOFade(1f, tempoFadeFragmento);
        yield return new WaitForSeconds(tempoFadeFragmento);
        yield return new WaitForSeconds(tempoExibindo);
        canvasGroup.DOFade(0f, tempoFade);
        yield return new WaitForSeconds(tempoFade);
        exibindo = false;
    }

    public void ContarPecinha(int indice)
    {
        inventario[indice] = true;

        if(indice == 8)
        {
            //GameManager.EventoJoia()
            personagem.temJoia1 = true;
            StartCoroutine(MostrarDicaPlanarAposInventario());
        }
    }
    IEnumerator MostrarDicaPlanarAposInventario()
    {
        yield return new WaitForSeconds(tempoFade+ tempoFadeFragmento+ tempoExibindo+ tempoFade);
        GetComponentInChildren<Interagir>().MostrarDicaPlanar();
    }

    //Atribui ao inventario as imagens de fragmentos
}
