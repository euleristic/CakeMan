using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    private void Update()
    {
        if (GetComponent<AudioSource>()?.clip == null) Destroy(gameObject);
    }
}
