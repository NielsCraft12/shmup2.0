using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBullet : MonoBehaviour
{
    public int bulletState = 2;

    Transform player; 
    [SerializeField]
    Vector2 playerpos;

    [SerializeField]
    int bulletSpeed;

    Rigidbody2D rb;



    Vector2 moveDirection;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerpos = new Vector2(player.position.x, player.position.y);
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3); 

        // Calculate the initial direction towards the player
        moveDirection = (playerpos - (Vector2)transform.position).normalized;

        if (bulletState == 2)
        {
        // Face the player when the bullet is instantiated
        Vector3 _relativePos = player.position;
        Quaternion _lookAt = Quaternion.LookRotation(Vector3.forward, transform.position - _relativePos);
        transform.rotation = _lookAt;
        }
    }

    private void FixedUpdate()
    {
        if (bulletState == 1)
        {
        rb.velocity = Vector2.down * bulletSpeed;

        }
        if (bulletState == 2)
        {
           rb.velocity = moveDirection * bulletSpeed;

        }
    }
}

