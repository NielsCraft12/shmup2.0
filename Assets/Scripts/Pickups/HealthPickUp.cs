using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : BasePickup
{
    protected override void PickUpBoost()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerControler playerControler = Player.GetComponent<PlayerControler>();
        if(playerControler.health < 3)
        {
        playerControler.health += 1;

        }
    }
}
