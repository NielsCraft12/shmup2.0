using System.Collections;
using UnityEngine;

public class ShootingEnemy : BasicEnemy
{
    [SerializeField]
    int state;

    [SerializeField]
    GameObject EnemyBullet;

    float randomY;
    float randomX;

    [SerializeField]
    int shootSpeed;

    Vector2 goToPos;
    bool isInPos;

    protected override void Start()
    {
        base.Start();
        state = Random.Range(1, 3);
        randomX = Random.Range(-11f, rightSideOfScreenInWorld);
        randomY = Random.Range(0f, 6f);
        goToPos = new Vector2(randomX, randomY);
    }

    private void FixedUpdate()
    {
        if (state == 2)
        {
        GameObject _player = GameObject.FindWithTag("Player");
        Vector3 _relativePos = _player.transform.position;
        Quaternion _lookAt = Quaternion.LookRotation(Vector3.forward, transform.position - _relativePos);
        transform.rotation = _lookAt;
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(randomX, randomY), .1f);

        if (transform.position.x == goToPos.x && transform.position.y == goToPos.y && !isInPos)
        {
            isInPos = true;
        StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {



         GameObject _bullet = Instantiate(EnemyBullet,transform.position,Quaternion.identity);
         EnemyBullet enemyBullet = _bullet.GetComponent<EnemyBullet>();
            enemyBullet.bulletState = state;
        yield return new WaitForSeconds(shootSpeed);
        }
    }
}
