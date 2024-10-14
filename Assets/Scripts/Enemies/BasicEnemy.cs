using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    protected int lives = 3;

   protected float rightSideOfScreenInWorld;
   protected float leftSideOfScreenInWorld;
    protected virtual void Start()
    {

        rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
    }

    protected virtual void Update()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }


    protected void DropLoot()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            lives--;

        }
    }
}
