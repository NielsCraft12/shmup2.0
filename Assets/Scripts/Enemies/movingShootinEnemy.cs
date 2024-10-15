using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class movingShootinEnemy : BasicEnemy
{


    [SerializeField]
    float enemyHightStart;
    [SerializeField]
    float enemyHight;
    int target = 0;

    float randomY;
    float randomX;

    [SerializeField]
    int shootSpeed;

    Vector2 goToPos;
    bool isInPos;

    [SerializeField]
    GameObject EnemyBullet;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        randomX = Random.Range(-11f, rightSideOfScreenInWorld);
        randomY = Random.Range(0f, 6f);
        goToPos = new Vector2(randomX, randomY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        GameObject _player = GameObject.FindWithTag("Player");
        Vector3 _relativePos = _player.transform.position;
        Quaternion _lookAt = Quaternion.LookRotation(Vector3.forward, transform.position - _relativePos);
        transform.rotation = _lookAt;

        if (!isInPos)
        {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(randomX, randomY), .1f);
        }

        if (transform.position.x == goToPos.x && transform.position.y == goToPos.y && !isInPos)
        {
            isInPos = true;
            StartCoroutine(Shoot());
        }

        if (isInPos)
        {
        LeftRight();
        }
    }

    private void LeftRight()
    {
        Debug.Log(target);

        if (transform.position.x <= leftSideOfScreenInWorld + 3)
        {
            target = 1;
        }
        if (transform.position.x >= rightSideOfScreenInWorld - 3)
        {
            target = 0;
        }

        if (target == 1)
        {

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightSideOfScreenInWorld - 1, transform.position.y), .1f);

        }
        if (target == 0)
        {

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftSideOfScreenInWorld + 1, transform.position.y), .1f);
        }

        Debug.Log(rightSideOfScreenInWorld);
    }

    IEnumerator Shoot()
    {
        while (true)
        {



            GameObject _bullet = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = _bullet.GetComponent<EnemyBullet>();
            enemyBullet.bulletState = 2;
            yield return new WaitForSeconds(shootSpeed);
        }
    }
}
