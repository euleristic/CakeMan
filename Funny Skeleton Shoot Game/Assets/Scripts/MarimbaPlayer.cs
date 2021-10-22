using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MarimbaPlayer : MonoBehaviour
{
    [SerializeField] float notesPerSecond;
    [SerializeField] List<AudioClip> noteClips = new List<AudioClip>();
    public bool playing;
    int lastPlayed;
    float timer = 0f;
    float SpN;
    AudioSource source;
    void Start()
    {
        SpN = 1f / notesPerSecond;
        source = GetComponent<AudioSource>();
        lastPlayed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > SpN)
        {
            timer -= SpN;
            source.PlayOneShot(noteClips[ChooseRandom()]);
        }
    }

    int ChooseRandom()
    {
        int random = (Random.Range(0, noteClips.Count - 1));
        if (random >= lastPlayed) random++;

        return random;
    }
}
