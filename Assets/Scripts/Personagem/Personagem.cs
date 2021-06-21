using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Personagem : MonoBehaviour
{
    [Header("Características")]
    public float velocidade;
    public float forcaPulo;
    public float tempoPulo;
    [Range(0f, 1f)]
    [SerializeField] private float anguloRotacao;
    [Range(0f, 0.5f)]
    [SerializeField] private float tempoRotacao;
    //[SerializeField] private float gravidade;
    

    [Header("Configuracoes")]
    public Transform posicaoPe;
    public float checarRaio;
    public LayerMask maskChao;
    [SerializeField] private Transform raycast;
    [SerializeField] private float sizeRaycast;

    [Space(10)]
    [SerializeField] private bool podePular;
    [SerializeField] private bool taNoChao;
    [SerializeField] private float contadorTempoPulo;

    private Rigidbody2D rb;
    private float gravidadeInicial;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        gravidadeInicial = rb.gravityScale;
    }
    void FixedUpdate()
    {
        Andar();
      
    }
    private void Update()
    {
        Pular();
        Rotacionar();
    }
    void Andar()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xMov * velocidade, rb.velocity.y);

        //testar depois de implementar gravidade, por enquanto, faz o personagem voar em quinas
        //rb.velocity = transform.TransformDirection(new Vector3(xMov * velocidade, 0, 0)) + new Vector3 (0,rb.velocity.y,0);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(posicaoPe.position, checarRaio);
    }
    void Pular()
    {
        taNoChao = Physics2D.OverlapCircle(posicaoPe.position, checarRaio, maskChao);
        /*
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
        }
        */

        if (taNoChao)
        {
            podePular = true;
            contadorTempoPulo = tempoPulo;
            rb.gravityScale = 0f;
        }
        else
        {
            contadorTempoPulo -= Time.deltaTime;
            rb.gravityScale = gravidadeInicial;
        }
        if (Input.GetKeyDown(KeyCode.Space) && podePular)
        {
            rb.velocity = Vector2.up * forcaPulo;
            
        }

        if (Input.GetKey(KeyCode.Space) && podePular == true)
        {
            if (contadorTempoPulo > 0)
            {
                rb.velocity = Vector2.up * forcaPulo;
                //ContadorTempoPulo -= Time.deltaTime;
            }
            else
            {
                podePular = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            podePular = false;
        }
    }

    void Rotacionar()
    {
        //raycast
        RaycastHit2D raycastHit = Physics2D.Raycast(raycast.position, raycast.TransformDirection(Vector2.down), sizeRaycast);
        Debug.DrawRay(raycast.position, raycast.TransformDirection(Vector2.down)* sizeRaycast, Color.red);

        Vector3 movementDirection = Vector3.Cross(raycastHit.normal, new Vector3(0, 0, 1));
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.AngleAxis(angle * anguloRotacao, Vector3.forward);
        Quaternion rotation = Quaternion.AngleAxis(angle * anguloRotacao, Vector3.forward);
        transform.DORotate(rotation.eulerAngles, tempoRotacao);
        
    }

    /*
    void Gravidade(float angle)
    {
        float gravity = rb.velocity.y - gravidade * (Mathf.Exp(Time.deltaTime)) / 2;
        rb.velocity = new Vector2(rb.velocity.x, gravity);
    }
    */
}
