using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour, IPlayerCollide
{    
    public void OnCollideWithPlayer(PlayerController player)
    {
        player.DrinkMilk();
        Destroy(gameObject);
        
    }
}
