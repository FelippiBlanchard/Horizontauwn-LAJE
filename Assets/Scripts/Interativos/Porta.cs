using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    [Header("Tempo")]
    [SerializeField] public float tempoAnimacao;
    [SerializeField] public float intervaloEntreAnimacao;

    public List<bool> fragmentos;
    public List<Transform> posicaoFragmentos;

    [SerializeField] public Transform personagem;
    [SerializeField] private GameManager gm;
    

    public void VerificarColecao()
    {
        var cont = 0;
        for (int i = 0; i<fragmentos.Count; i++)
        {
            if (fragmentos[i])
            {
                cont++;
            }
        }
        if (cont == fragmentos.Count)
        {
            gm.Creditos();
            Debug.Log("zerou");
        }
    }
}
