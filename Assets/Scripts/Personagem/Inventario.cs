using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public List<bool> inventario;
    public List<Image> fragmentosCanvas; 
    public Personagem personagem;

    
    public void ContarPecinha(int indice)
    {
        inventario[indice] = true;

        if(indice == 8)
        {
            //GameManager.MudançadeBackground()
            personagem.temJoia1 = true;
        }
        else
        {
            if((inventario[6]) && (inventario[7]))
            {
                //GameManager.AtivarVentos()
            }
        }
    }

    //Atribui ao inventario as imagens de fragmentos
}
