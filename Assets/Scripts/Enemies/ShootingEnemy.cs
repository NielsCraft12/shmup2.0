using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BasicEnemy
{
    int state;

    [SerializeField]
    GameObject EnemyBullet;

    float randomY;
    float randomX;

    [SerializeField]
    int shootSpeed;

    protected override void Start()
    {
        base.Start();
        state = Random.Range(0, 2);
        randomX = Random.Range(-11f, rightSideOfScreenInWorld);
        randomY = Random.Range(0f, 6f);
        StartCoroutine(Shoot());

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(randomX, randomY), .1f);
    }

    IEnumerator Shoot()
    {
        while (true)
        {

            GameObject _player = GameObject.FindWithTag("Player");
            Vector3 _relativePos = _player.transform.position;
            Quaternion _lookAt = Quaternion.LookRotation(Vector3.forward, _relativePos - _relativePos);
            transform.rotation = _lookAt;



            Instantiate(EnemyBullet,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(shootSpeed);
        }
    }
}
