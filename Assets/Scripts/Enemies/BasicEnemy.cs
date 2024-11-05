using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    protected int lives = 3;

    SpriteRenderer sprite;
    float hurttimer;

   protected float rightSideOfScreenInWorld;
   protected float leftSideOfScreenInWorld;

    [SerializeField]
    List<GameObject> Pickups = new List<GameObject>();

    [SerializeField]
    GameObject explosion;
    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
    }

    protected virtual void Update()
    {
        if (lives <= 0)
        {
          GameObject _particle  = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(_particle, 0.3f);
            GameManager.Instance.score += 5;
            int randomNumber = Random.Range(0, 3);
            if (randomNumber == 2)
            {
                randomNumber = Random.Range(0, Pickups.Count);
                Instantiate(Pickups[randomNumber], transform.position,Quaternion.identity);
            }
            Destroy(gameObject);

        }
       

        if (hurttimer > 0)
        {
            hurttimer -= Time.deltaTime;

            if (hurttimer <= 0)
            {
                sprite.color = Color.white;
                hurttimer = 0;
            }
        }
    }


    protected void DropLoot()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            sprite.color = new Color(1, 0, 0);
            hurttimer = 0.1f;
            Destroy(collision.gameObject);
            lives--;

        }
    }
}
