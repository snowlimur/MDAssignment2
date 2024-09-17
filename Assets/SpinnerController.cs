using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinnerController : MonoBehaviour
{
    [FormerlySerializedAs("Rigidbody 2D")]
    public Rigidbody2D rb;
    
    public float rotationSpeed = 1f;
    
    void Update()
    {
        rb.rotation += rotationSpeed;
    }
}
