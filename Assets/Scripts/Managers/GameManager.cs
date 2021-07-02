using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public List<bool> medosLiberados;
    public List<GameObject> ventos;
    public List<GameObject> preVentos;
    public static GameManager instance;

    [SerializeField] private GameObject backgroundTransition;
    [SerializeField] private CanvasGroup canvasGroupTransition;
    [SerializeField] private float tempoStart;

    private void Start()
    {
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
        backgroundTransition.transform.DOScale(0f, tempoStart * 0.5f);
    }

    public void EventoJoia()
    {
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
