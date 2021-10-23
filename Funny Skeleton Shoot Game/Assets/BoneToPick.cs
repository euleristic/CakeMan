using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneToPick : MonoBehaviour, IPlayerCollide
{
    [SerializeField] private AudioClip plop;
    private void Start()
    {
        if (plop == null) return;
        SoundEffectPlayer.PlaySoundEffect(plop, 1f, 1f, 0.15f);
    }
    public void OnCollideWithPlayer(PlayerController player)
    {
        if(player.GetBone())
        {
            //play sound

            Destroy(gameObject);
        }
    }
}
