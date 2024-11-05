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


        if (state == 1)
        {
            enemyHight = -1;
        }
        LeftRight();

        if (transform.position.y <= -7)
        {
            transform.position = new Vector3(transform.position.x, 10.32f, 0);
            enemyHight = 0;
        }

    }

    private void LeftRight()
    {

        if (transform.position.x <= leftSideOfScreenInWorld + 3)
        {
            target = 1;
            if (state == 2)
            {
                enemyHight += 0.5f;
            }
        }
        if (transform.position.x >= rightSideOfScreenInWorld - 3)
        {
            target = 0;
            if ( state == 2)
            {
                enemyHight += 0.5f;
            }
        }

        if (target == 1)
        {


          transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightSideOfScreenInWorld - 1, enemyHightStart - enemyHight), .1f);

        }
        if (target == 0)
        {

         transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftSideOfScreenInWorld + 1, enemyHightStart - enemyHight), .1f);
        }

    }




}