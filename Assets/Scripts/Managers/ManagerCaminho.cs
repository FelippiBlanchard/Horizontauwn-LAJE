using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerCaminho : MonoBehaviour
{
    [SerializeField] private Camera camerinha;
    [SerializeField] private Caminho_Plataformas caminho_Plataformas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caminho_Plataformas.DesativarTudo();
            camerinha.DOOrthoSize(30, 1.5f);
        }
    }
}
