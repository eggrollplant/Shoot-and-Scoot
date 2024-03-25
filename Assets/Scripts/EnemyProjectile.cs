using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(21, 21);
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = transform.up * -speed;
        //transform.position += speed * tr;

        Object.Destroy(gameObject, 15.0f);
    }

    
}
