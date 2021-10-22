using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    List<Collider2D> colliderList = new List<Collider2D>();

    public bool Triggered()
    {
        return colliderList.Count > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderList.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (colliderList.Contains(other)) colliderList.Remove(other);
    }
}
