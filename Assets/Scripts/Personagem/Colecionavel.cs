using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colecionavel : MonoBehaviour
{
    public int indice;
    public Inventario inventario;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventario.ColetarColecionavel(indice);
            SoundManager.instance.ColetarColecionavel();
            Destroy(this.gameObject);
        }
    }
}
