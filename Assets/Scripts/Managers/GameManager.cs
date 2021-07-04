using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<bool> medosLiberados;
    public List<GameObject> ventos;
    public List<GameObject> preVentos;
    public static GameManager instance;

    [SerializeField] private GameObject backgroundTransition;
    [SerializeField] private CanvasGroup canvasGroupTransition;
    [SerializeField] private float tempoStart;
    [SerializeField] private TextMeshProUGUI textoInicial;

    [Header("Créditos")]
    [SerializeField] private Image backgroundCreditos;
    [SerializeField] private Image backgroundCreditosPreto;
    [SerializeField] private Camera camerinha;
    [SerializeField] private GameObject creditos;
    [SerializeField] private float fadeBackground;
    [SerializeField] private float fadeBackgroundPreto;
    [SerializeField] private float tempoCreditos;
    [SerializeField] private Inventario inventario;
    [SerializeField] private CanvasGroup textoEspecialCredito;

    private void Start()
    {
        textoInicial.DOFade(0f, 0f);
        Time.timeScale = 1f;
        instance = this;
        DesativarVentos();
        AtivarPreVentos();
        StartCoroutine(ITransition());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.K))
        {
            Creditos();
        }
    }
    IEnumerator ITransition()
    {
        canvasGroupTransition.alpha = 1;
        yield return new WaitForSeconds(1f);
        textoInicial.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(2.5f);
        textoInicial.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.5f);
        //backgroundTransition.transform.DOScale(0f, tempoStart * 0.7f);
        backgroundTransition.GetComponent<Image>().DOFade(0f, tempoStart).SetEase(Ease.InQuad);
    }

    public void EventoJoia()
    {
        //Time.timeScale = 0f;
       // backgroundInicial.GetComponent<Image>().DOFade(0, 1.5f);
        //Time.timeScale = 1f;
        //Mudar background
    }

    public void EventoMedoLiberado(int indiceMedo)
    {
        medosLiberados[indiceMedo] = true;
        if (MedosForamLiberados())
        {
            AtivarVentos();
            DesativarPreVentos();
        }
    }

    public void AtivarVentos()
    {
        for (int i = 0; i < ventos.Count; i++)
        {
            var particleSystem = ventos[i].GetComponentInChildren<ParticleSystem>();
            var collider = ventos[i].GetComponent<Collider2D>();
            particleSystem.Play();
            collider.enabled = true;
        }
    }
    public void DesativarVentos()
    {
        for (int i = 0; i < ventos.Count; i++)
        {
            var particleSystem = ventos[i].GetComponentInChildren<ParticleSystem>();
            var collider = ventos[i].GetComponent<Collider2D>();
            particleSystem.Stop();
            collider.enabled = false;
        }
    }

    public void AtivarPreVentos()
    {
        for (int i = 0; i < preVentos.Count; i++)
        {
            var particleSystem = preVentos[i].GetComponentInChildren<ParticleSystem>();
            var collider = preVentos[i].GetComponent<Collider2D>();
            particleSystem.Play();
            collider.enabled = true;
        }
    }
    public void DesativarPreVentos()
    {
        for (int i = 0; i < preVentos.Count; i++)
        {
            var particleSystem = preVentos[i].GetComponentInChildren<ParticleSystem>();
            var collider = preVentos[i].GetComponent<Collider2D>();
            particleSystem.Stop();
            collider.enabled = false;
        }
    }


    public bool MedosForamLiberados()
    {
        var cont = 0;
        for (int i = 0; i < medosLiberados.Count; i++)
        {
            if (medosLiberados[i])
            {
                cont++;
            }
        }
        if (cont == medosLiberados.Count)
        {
            return true;
        }
        return false;
    }

    public void Creditos()
    {
        StartCoroutine(ICreditos());
    }
    IEnumerator ICreditos()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2.5f);

        camerinha.DOOrthoSize(10, 2f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2);

        SoundManager.instance.Creditos();

        backgroundCreditos.DOFade(1f, 3f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(fadeBackground);

        if (inventario.VerificarColecionaveis())
        {
            textoEspecialCredito.DOFade(1f, 2f).SetUpdate(true);
            yield return new WaitForSecondsRealtime(5f);
            textoEspecialCredito.DOFade(0f, 2f).SetUpdate(true);
        }

        backgroundCreditosPreto.DOFade(1f, 3f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(fadeBackgroundPreto);

        creditos.GetComponent<RectTransform>().DOMoveY(1900, tempoCreditos).SetUpdate(true).SetEase(Ease.InOutSine);
        yield return new WaitForSecondsRealtime(tempoCreditos);

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif

    }
}
