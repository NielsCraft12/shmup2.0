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


    protected void DropLoot()
    {

    }
}
