using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public static void PlaySoundEffect(AudioClip clip, float volume = 1f, float pitch = 1f, float pitchVariance = 0f, float volumeVariance = 0f)
    { 
        var audio = Instantiate(new GameObject(clip.name, typeof(AudioSource))).GetComponent<AudioSource>();
        audio.clip = clip;
        audio.volume = volume + Random.Range(-volumeVariance, volumeVariance);
        audio.pitch = pitch + Random.Range(-pitchVariance, pitchVariance);
        audio.Play();
        Destroy(audio.gameObject, clip.length + 1f);
    }
}
