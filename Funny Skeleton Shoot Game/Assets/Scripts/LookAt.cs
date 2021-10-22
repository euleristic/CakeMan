using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        transform.right = Vector3.Lerp(transform.right, cam.ScreenToWorldPoint((Input.mousePosition-transform.position)), 0.6f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z);
    }
}
