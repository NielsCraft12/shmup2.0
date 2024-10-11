using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : BasicEnemy
{
    [SerializeField]
    private int state;

    int target = 0;
    [SerializeField]
    Transform player;

    [SerializeField]
    float enemyHightStart;
    [SerializeField]
    float enemyHight;



    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        state = Random.Range(1, 3);
        enemyHightStart = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if(state == 1)
        {
            enemyHight = -1;
        }
        LeftRight();

    }

    private void LeftRight()
    {   
        Debug.Log(target);

        if(transform.position.x <= leftSideOfScreenInWorld + 3)
        {
            target = 1;
            if(enemyHight < 5 && state == 2)
            {
                enemyHight++;
            }
        }
        if (transform.position.x >= rightSideOfScreenInWorld - 3)
        {
            target = 0;
            if (enemyHight < 5 && state == 2)
            {
                enemyHight++;
            }
        }

        if (target == 1)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightSideOfScreenInWorld - 1, enemyHightStart - enemyHight), .1f);

        }  if (target == 0)
        {
            //transform.rotation = Quaternion.Euler(0, 0, -44);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftSideOfScreenInWorld + 1, enemyHightStart - enemyHight), .1f);
        }   

        Debug.Log(rightSideOfScreenInWorld);
    }


    

}
