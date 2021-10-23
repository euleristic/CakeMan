using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleObject : MonoBehaviour
{
    public bool wiggling = true;
    private float startRot;
    [SerializeField] private float wiggleSpeed = 1,   wiggleAngle = 15f, input = 1, offset = 0f;
    [SerializeField] private bool  clampPositive, clampNegative;

    public float lastCosSign = 1f;

    [SerializeField] private AudioClip clip;

    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        startRot = transform.localRotation.eulerAngles.z;

    }
    public void SetInputFloat(float input_in)
    {
        input = input_in;
    }
    public void SetWiggleSpeedFloat(float wiggleSpeed_in)
    {
        wiggleSpeed = wiggleSpeed_in;
    }

    void Update()
    {
        if (!wiggling) return;

        float root;
        root = Mathf.Sin(offset + Time.time * wiggleSpeed);

        var c = Mathf.Sign(root);

        if(spr.enabled &&  clip != null  && c != lastCosSign)
        {
            SoundEffectPlayer.PlaySoundEffect(clip, Mathf.Min(input, 1f), 3f, 0.3f, Mathf.Max(input, 1f) / 10f);
        }
        
        
        if (clampNegative) root = Mathf.Max(root, 0f);
        if (clampPositive) root = Mathf.Min(root, 0f);

        root *= input;


        transform.localRotation = Quaternion.Euler(0f,0f, startRot + root * wiggleAngle);
        lastCosSign = c;

    }
}
