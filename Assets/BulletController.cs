using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletController : MonoBehaviour {
    public float speed;
    public float leftEdge = -17.3f;

    private Rigidbody2D _rb;
    private GameController _gameController;
    private bool _isStopped = true;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _gameController = FindObjectOfType<GameController>();

        Run();
    }

    void Update() {
        if (_gameController.IsPaused()) {
            Pause();
        }
        else {
            Run();
        }

        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }

    public void Run() {
        if (_isStopped) {
            _isStopped = false;
            _rb.velocity = Vector2.left * speed;
        }
    }

    public void Pause() {
        if (!_isStopped) {
            _isStopped = true;
            _rb.velocity = Vector2.zero;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}