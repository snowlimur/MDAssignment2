using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
    public Collider2D collider;
    public SpriteRenderer spriteRenderer;
    public float disableTime;

    private float _timer;
    private bool _isEnabled;

    void Start() {
        _isEnabled = true;
    }

    void Update() {
        if (!_isEnabled) {
            _timer += Time.deltaTime;

            if (_timer >= disableTime) {
                _timer = 0;
                _isEnabled = true;
                collider.enabled = _isEnabled;
                spriteRenderer.color = new Color(255, 255, 255, 255);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_isEnabled && collision.gameObject.CompareTag("Player")) {
            _isEnabled = false;
            collider.enabled = _isEnabled;
            spriteRenderer.color = new Color(255, 255, 255, 0.3f);
        }
    }
}