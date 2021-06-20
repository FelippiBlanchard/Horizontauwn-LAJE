using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float speed;
    public float ForcaPulo;
    Rigidbody2D rb;

    private bool TaNoChao;
    public Transform PosPe;
    public float ChecarRaio;
    public LayerMask QueEChao;

    private float ContadorTempoPulo;
    public float TempoPulo;
    private bool TaPulando;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Andar();
      
    }
    private void Update()
    {
        Pular();
    }
    void Andar()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xMov * speed, rb.velocity.y);
    }
    void Pular()
    {
        TaNoChao = Physics2D.OverlapCircle(PosPe.position, ChecarRaio, QueEChao);
        if (TaNoChao == true && Input.GetKeyDown(KeyCode.Space))
        {
            TaPulando = true;
            ContadorTempoPulo = TempoPulo;
            rb.velocity = Vector2.up * ForcaPulo;
        }
        if (Input.GetKey(KeyCode.Space) && TaPulando == true)
        {
            if (ContadorTempoPulo > 0)
            {
                rb.velocity = Vector2.up * ForcaPulo;
                ContadorTempoPulo -= Time.deltaTime;
            }
            else
            {
                TaPulando = false;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                TaPulando = false;
            }
        }
    }
}
