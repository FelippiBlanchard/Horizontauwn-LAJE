using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<bool> inventario;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ContarPecinha(int indice)
    {
        inventario[indice] = true;
        Debug.Log(inventario);
        //if indice = 8 chama a função de background
    }
}
