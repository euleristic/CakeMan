using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleObject : MonoBehaviour
{
    public bool wiggling = true;
    private float startRot;
    [SerializeField] private float wiggleSpeed = 1,   wiggleAngle = 15f, input = 1;
    [SerializeField] private bool  clampPositive, clampNegative;

    private void Start()
    {
        startRot = transform.localRotation.eulerAngles.z;
    }
    public void SetInputFloat(float input_in)
    {
        input = input_in;
    }
    void Update()
    {
        if (!wiggling) return;

        float root;
        root = Mathf.Sin(Time.time * wiggleSpeed);
        if (clampNegative) root = Mathf.Max(root, 0f);
        if (clampPositive) root = Mathf.Min(root, 0f);

        root *= input;


        transform.localRotation = Quaternion.Euler(0f,0f, startRot + root * wiggleAngle);
    }
}
