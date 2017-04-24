using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotaator : MonoBehaviour {

    public float tumble;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
