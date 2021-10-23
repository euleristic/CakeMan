using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] private float length, speed;
    private Vector3 root;

    [SerializeField] private bool x, y;
    private void Start()
    {
        root = transform.localPosition;
    }
    void Update()
    {
        if (x)
            transform.localPosition = root.With(x:root.x + Mathf.Sin(Time.time * speed) * length);
        if (y)
            transform.localPosition = root.With(y: root.y + Mathf.Cos(Time.time * speed) * length);

    }
}
