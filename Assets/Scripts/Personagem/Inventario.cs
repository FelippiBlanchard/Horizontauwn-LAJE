using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventario : MonoBehaviour
{
    public List<bool> inventario;
    public Personagem personagem;

    [SerializeField] private float tempoFadeInventario;
    [SerializeField] private float tempoFadeFragmento;
    [SerializeField] private float tempoExibindoInventario;
    [SerializeField] private float tempoFadeBackground;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private List<Image> fragmentos;
    [SerializeField] private GameObject backgroundInicial;
    private int contadorInventario;

    [Header("Colecionaveis")]
    [SerializeField] private List<Image> colecionaveis;
    [SerializeField] private int colecionaveisContador;


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
        if(indice == 8)
        {
            SoundManager.instance.ColetarJoia();
            StartCoroutine(MudarBackground(indice));
        }
        else
        {
            SoundManager.instance.ColetarPecinha();
            StartCoroutine(IExibirColetando(indice));
            ContarPecinha(indice);
        }
    }
    public void ColetarColecionavel(int indice)
    {
        //colecionaveisBool[indice] = true;
        colecionaveisContador++;
        StartCoroutine(IColecionavel(indice));
    }
    public bool VerificarColecionaveis()
    {
        if(colecionaveisContador == colecionaveis.Count)
        {
            return true;
        }
        return false;
    }
    IEnumerator IColecionavel(int indice)
    {
        exibindo = true;
        canvasGroup.DOFade(1f, tempoFadeInventario);
        yield return new WaitForSeconds(tempoFadeInventario);

        colecionaveis[indice].DOFade(1f, tempoFadeFragmento);

        yield return new WaitForSeconds(tempoExibindoInventario);
        canvasGroup.DOFade(0f, tempoFadeInventario);
        yield return new WaitForSeconds(tempoFadeInventario);
        exibindo = false;

    }
    IEnumerator IExibir()
    {
        exibindo = true;
        canvasGroup.DOFade(1f, tempoFadeInventario);
        yield return new WaitForSeconds(tempoFadeInventario);
        yield return new WaitForSeconds(tempoExibindoInventario);
        canvasGroup.DOFade(0f, tempoFadeInventario);
        yield return new WaitForSeconds(tempoFadeInventario);
        exibindo = false;
    }
    IEnumerator IExibirColetando(int indice)
    {
        exibindo = true;
        canvasGroup.DOFade(1f, tempoFadeInventario);
        yield return new WaitForSeconds(tempoFadeInventario);

        fragmentos[indice].DOFade(1f, tempoFadeFragmento);
        yield return new WaitForSeconds(tempoFadeFragmento);
        
        yield return new WaitForSeconds(tempoExibindoInventario);
        canvasGroup.DOFade(0f, tempoFadeInventario);
        yield return new WaitForSeconds(tempoFadeInventario);
        exibindo = false;
    }

    public void ContarPecinha(int indice)
    {
        inventario[indice] = true;

        if(indice == 8)
        {
            //parar no tempo não funciona(?)
            personagem.temJoia1 = true;
            StartCoroutine(MostrarDicaPlanarAposInventario());
        }
        contadorInventario++;
        if (contadorInventario == 1)
        {
            GetComponentInChildren<Interagir>().MostrarDicaI();
        }
        if(contadorInventario == inventario.Count)
        {
            transform.DOScale(1.5f, 1.5f);
        }
        
    }
    IEnumerator MostrarDicaPlanarAposInventario()
    {
        yield return new WaitForSeconds(tempoFadeInventario+ tempoFadeFragmento+ tempoExibindoInventario+ tempoFadeInventario);
        GetComponentInChildren<Interagir>().MostrarDicaPlanar();
    }

    IEnumerator MudarBackground(int indice)
    {
        Time.timeScale = 0f;
        backgroundInicial.GetComponent<SpriteRenderer>().DOFade(0, tempoFadeBackground).SetUpdate(true);
        yield return new WaitForSecondsRealtime(tempoFadeBackground);
        Time.timeScale = 1f;
        StartCoroutine(IExibirColetando(indice));
        ContarPecinha(indice);
    }
    //Atribui ao inventario as imagens de fragmentos
}
