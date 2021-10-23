using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneToPick : MonoBehaviour, IPlayerCollide
{
    public void OnCollideWithPlayer(PlayerController player)
    {
        if(player.GetBone())
        {
            //play sound

            Destroy(gameObject);
        }
    }
}
