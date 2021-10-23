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
        Quaternion inital_rotation = transform.rotation;

        transform.right = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        Quaternion to_rotation = transform.rotation;

        transform.rotation = Quaternion.Slerp(inital_rotation, to_rotation, 6f * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z);
    }
}
