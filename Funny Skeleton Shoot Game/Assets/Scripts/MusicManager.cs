using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> currentChord;
    [SerializeField] List<AudioClip> CSm= new List<AudioClip>();
    [SerializeField] List<AudioClip> AMaj7= new List<AudioClip>();
    [SerializeField] List<AudioClip> GS7 = new List<AudioClip>();
    [SerializeField] List<AudioClip> FSm7 = new List<AudioClip>();
    [SerializeField] List<AudioClip> B7 = new List<AudioClip>();
    [SerializeField] List<AudioClip> E = new List<AudioClip>();
    [SerializeField] List<AudioClip> FS7 = new List<AudioClip>();
    [SerializeField] List<AudioClip> B = new List<AudioClip>();
    [SerializeField] List<AudioClip> CS = new List<AudioClip>();
    [SerializeField] List<AudioClip> CS7 = new List<AudioClip>();
    [SerializeField] List<AudioClip> FS = new List<AudioClip>();

    [Space]
    [SerializeField] bool allowedHardCode = true;
    [SerializeField] float bpm;
    [SerializeField] List<List<AudioClip>> progression = new List<List<AudioClip>>();

    AudioSource source;
    float timer;
    void Start()
    {
        timer = 0f;
        source = GetComponent<AudioSource>();
        if (allowedHardCode)
        {
            progression.Add(CSm);        progression.Add(AMaj7);         progression.Add(GS7);
            progression.Add(GS7);        progression.Add(CSm);           progression.Add(FSm7);
            progression.Add(B);          progression.Add(B7);            progression.Add(E);
            progression.Add(FS7);        progression.Add(B);             progression.Add(GS7);
            progression.Add(CS);         progression.Add(CS7);           progression.Add(FS);
            progression.Add(GS7);
        }
    }

    private void Update()
    {
        currentChord = GetCurrentChord();
    }

    public List<AudioClip> GetCurrentChord()
    {
        return progression[(int)(source.time / 2f) % progression.Count];
    }
}
