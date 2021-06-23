using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminho : MonoBehaviour
{
    [SerializeField] private List<GameObject> mensagens;
    public int index;

    private void Start()
    {
        mensagens[index].GetComponent<Collider2D>().enabled = true;
    }
    public void ProximaMensagem()
    {

        mensagens[index].GetComponent<Collider2D>().enabled = false;
        index++;
        if (index < mensagens.Count)
        {
            mensagens[index].GetComponent<Collider2D>().enabled = true;
        }
    }
}
