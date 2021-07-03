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

    private void Start()
    {
        textoInicial.DOFade(0f, 0f);
        Time.timeScale = 1f;
        instance = this;
        DesativarVentos();
        AtivarPreVentos();
        StartCoroutine(ITransition());
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
}
