using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    int bulletSpeed;

    Rigidbody2D rb;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * bulletSpeed;
    }
}
