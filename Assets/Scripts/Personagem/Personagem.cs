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
    [SerializeField] private float velocidadeMaximaDeQueda;
    [Range(0f, 1f)]
    [SerializeField] private float anguloRotacao;
    [Range(0f, 0.5f)]
    [SerializeField] private float tempoRotacao;
    //[SerializeField] private float gravidade;


    [Header("Configuracoes")]
    [SerializeField] private Transform raycast;
    [SerializeField] private float sizeRaycast;

    private Animator anim;
    private SpriteRenderer sprite;

    [Space(10)]
    [SerializeField] private bool podePular;
    [SerializeField] private bool taNoChao;
    [SerializeField] private float contadorTempoPulo;

    [Header("Jóia 1")]
    public bool temJoia1;
    public float velocidadeYPlanar;
    [SerializeField] private float velocidadeXPlanar;

    private Rigidbody2D rb;
    private float gravidadeInicial;
    private bool planando;
    private RaycastHit2D raycastHit;

    private bool atravessandoPlataforma;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
        PoderJoia1();
        AjusteGravidade();
        int masklayer = LayerMask.GetMask("Chao");
        raycastHit = Physics2D.Raycast(raycast.position, raycast.TransformDirection(Vector2.down), sizeRaycast, masklayer);
    }
    void Andar()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xMov * velocidade * (planando ? velocidadeXPlanar : 1), rb.velocity.y);

        if(xMov > 0)
        {
            sprite.flipX = true;
        }
        if(xMov < 0)
        {
            sprite.flipX = false;
        }

    }

    void Pular()
    {
        //taNoChao = Physics2D.OverlapCircle(posicaoPe.position, checarRaio, maskChao);
        taNoChao = raycastHit.collider != null;

        if (taNoChao && !atravessandoPlataforma)
        {
            contadorTempoPulo = tempoPulo;
            rb.gravityScale = 0f;
            anim.SetBool("jump", false);
            podePular = true;

        }
        else
        {
            contadorTempoPulo -= Time.deltaTime;
            rb.gravityScale = gravidadeInicial;
        }
        if (Input.GetKeyDown(KeyCode.Space) && podePular)
        {
            anim.SetBool("jump", true);
            rb.velocity = Vector2.up * forcaPulo;

        }

        if (Input.GetKey(KeyCode.Space) && podePular == true)
        {
            if (contadorTempoPulo > 0)
            {
                rb.velocity = Vector2.up * forcaPulo;
                //ContadorTempoPulo -= Time.deltaTime;
                anim.SetBool("jump", true);
            }
            else
            {
                podePular = false;
            }
        }
        if (taNoChao && !atravessandoPlataforma) //por algum motivo conserta bug de pular continuamente sem animação
        {
            anim.SetBool("jump", false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            podePular = false;
        }
    }

    void Rotacionar()
    {
        //raycast

        Debug.DrawRay(raycast.position, raycast.TransformDirection(Vector2.down) * sizeRaycast, Color.red);

        Vector3 movementDirection = Vector3.Cross(raycastHit.normal, new Vector3(0, 0, 1));
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.AngleAxis(angle * anguloRotacao, Vector3.forward);
        Quaternion rotation = Quaternion.AngleAxis(angle * anguloRotacao, Vector3.forward);
        transform.DORotate(rotation.eulerAngles, tempoRotacao);

    }

    void PoderJoia1()
    {
        if (temJoia1)
        {
            if (!taNoChao)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (rb.velocity.y <= velocidadeYPlanar)
                    {
                        anim.SetBool("planar", true);
                        planando = true;
                        rb.velocity = new Vector2(rb.velocity.x, velocidadeYPlanar);
                    }
                }
                if (Input.GetKeyUp(KeyCode.Z))
                {
                    planando = false;
                    anim.SetBool("planar", false);
                }
            }
            else
            {
                planando = false;
                anim.SetBool("planar", false);
            }
        }
    }

    void AjusteGravidade()
    {
        if (!taNoChao)
        {
            if (rb.velocity.y <= velocidadeMaximaDeQueda)
            {
                rb.velocity = new Vector2(rb.velocity.x, velocidadeMaximaDeQueda);
            }
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.enabled)
        {
            encostandoAlgo = true;
        }
        else
        {
            encostandoAlgo = false;
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.enabled)
        {
            atravessandoPlataforma = true;
        }
        else
        {
            atravessandoPlataforma = false;
        }
    }
}
