using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Andar();
        float yMov = Input.GetAxisRaw("Vertical") * speed;
        rb.velocity = new Vector2(rb.velocity.x, yMov);
    }
    void Andar()
    {
        float xMov = Input.GetAxisRaw("Horizontal") * speed;
        rb.velocity = new Vector2(xMov, rb.velocity.y);
    }
}
