using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Interagir : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] public float tempoInteragindo;
    public Inventario inventario;
    public List<GameObject> fragmentos;
    [SerializeField] private float tempoPraDica;
    [SerializeField] private float tempoFadeDica;
    [SerializeField] private TextMeshProUGUI dicaK;
    [SerializeField] private TextMeshProUGUI dicaPlanar;
    [SerializeField] private Transform sprite;
    [SerializeField] private TextMeshProUGUI dicaI;

    private bool interagivel;
    private GameObject objetoInteragindo;
    private Coroutine dicaKmostrando;
    private float posicaoInicialSprite;


    private void Start()
    {
        dicaK.DOFade(0f, 0f);
        dicaPlanar.DOFade(0f, 0f);
        dicaI.DOFade(0f, 0f);
        posicaoInicialSprite = sprite.localPosition.y;
    }
    private void Update()
    {
        AtivarInteracao();
    }

    public void AtivarInteracao()
    {
        if (Input.GetKeyDown("f")) {
            StartCoroutine(AnimacaoInteracao(posicaoInicialSprite));
        }
        if (Input.GetKeyDown("f") && interagivel)
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

    IEnumerator AnimacaoInteracao(float posicaoInicial)
    {
        sprite.DOLocalMoveY(posicaoInicial - 0.6f, tempoInteragindo);
        yield return new WaitForSeconds(tempoInteragindo);
        sprite.DOLocalMoveY(posicaoInicial, tempoInteragindo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objetoInteragindo = collision.gameObject;
        if (objetoInteragindo.CompareTag("Interagivel"))
        {
            dicaKmostrando = StartCoroutine(MostrarDicaK());
        }
        interagivel = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interagivel = false;
        if (objetoInteragindo.CompareTag("Interagivel"))
        {
            StopCoroutine(dicaKmostrando);
            dicaK.DOFade(0f, tempoFadeDica);
        }
    }
    IEnumerator MostrarDicaK()
    {
        yield return new WaitForSeconds(tempoPraDica);
        dicaK.DOFade(1f, tempoFadeDica);
    }

    public void MostrarDicaI()
    {
        StartCoroutine(IMostrarDicaI());
    }
    IEnumerator IMostrarDicaI()
    {
        yield return new WaitForSeconds(2f);
        dicaI.DOFade(1f, tempoFadeDica);
        yield return new WaitForSeconds(2.5f);
        dicaI.DOFade(0f, tempoFadeDica);
    }


    public void MostrarDicaPlanar()
    {
        StartCoroutine(IMostrarDicaPlanar());
    }
    IEnumerator IMostrarDicaPlanar()
    {
        dicaPlanar.DOFade(1f, tempoFadeDica);
        yield return new WaitForSeconds(2.5f);
        dicaPlanar.DOFade(0f, tempoFadeDica);
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
                var fragmento = Instantiate(fragmentos[i], new Vector3(transform.position.x, transform.position.y+6), fragmentos[i].transform.rotation, transform);

                fragmento.transform.SetParent(porta.transform);
                fragmento.transform.DOMove(porta.posicaoFragmentos[i].position, porta.tempoAnimacao).SetUpdate(true);
                yield return new WaitForSecondsRealtime(porta.intervaloEntreAnimacao);
            }
        }
        porta.VerificarColecao();
    }
}
