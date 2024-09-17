using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapSectionController : MonoBehaviour {
    [FormerlySerializedAs("Length")] public float length = 30.9f;

    [FormerlySerializedAs("Rigidbody 2D")] public Rigidbody2D rb;
    
    private Vector3 _startPosition;

    void Start() {
        _startPosition = transform.position;
    }

    void Update() { }

    public float X() {
        return transform.position.x;
    }

    public float Length() {
        return length;
    }

    public void Run(Vector3 position, float speed) {
        transform.position = position;
        rb.velocity = Vector2.left * speed;
    }

    public void Continue(float speed) {
        rb.velocity = Vector2.left * speed;
    }

    public void Stop() {
        rb.velocity = Vector2.zero;
    }

    public void Reset() {
        rb.velocity = Vector2.zero;
        transform.position = _startPosition;
    }
}