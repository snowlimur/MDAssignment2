using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int KnockedOut = Animator.StringToHash("KnockedOut");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int ResetTrigger = Animator.StringToHash("Reset");

    public float jumpVelocity = 15f;
    public float jumpTime = 0.5f;
    public float fallGravityScale = 4;

    private Animator _animator;
    private Rigidbody2D _rb;
    private GameController _gameController;

    private bool _isJumping;
    private bool _isGrounded;
    private bool _isStopped;
    private bool _isKnockedOut;
    private float _jumpTimer;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _gameController = FindObjectOfType<GameController>();
    }

    void Update() {
        if (_isKnockedOut) {
            return;
        }

        if (_gameController.IsPaused()) {
            if (!_isStopped) {
                _isStopped = true;
                _animator.enabled = false;
                _rb.simulated = false;
            }

            return;
        }

        if (_isStopped) {
            _isStopped = false;
            _animator.enabled = true;
            _rb.simulated = true;
        }

        DoJump();
    }

    public void Reset() {
        _isKnockedOut = false;
        _animator.SetBool(KnockedOut, false);

        _isGrounded = true;
        _isJumping = false;
        _isStopped = false;
        _jumpTimer = 0;

        _animator.enabled = true;
        _rb.simulated = true;
        _animator.SetTrigger(ResetTrigger);
    }

    void DoJump() {
        if (_isGrounded && Input.GetButtonDown("Jump")) {
            _rb.velocity = Vector2.up * jumpVelocity;
            _isJumping = true;
            _isGrounded = false;
            _animator.SetTrigger(Jump);
            _animator.SetBool(IsJumping, true);

            return;
        }

        if (_isJumping && Input.GetButton("Jump")) {
            if (_jumpTimer < jumpTime) {
                _rb.velocity = Vector2.up * jumpVelocity;
                _jumpTimer += Time.deltaTime;
            }
            else {
                _isJumping = false;
            }

            return;
        }

        if (Input.GetButtonUp("Jump")) {
            if (!_isGrounded) {
                _rb.gravityScale = fallGravityScale;
                _isJumping = false;
                _jumpTimer = 0f;
            }
        }
    }

    void DoGround() {
        _isGrounded = true;
        _isJumping = false;
        _jumpTimer = 0f;
        _rb.gravityScale = 1;
        _rb.velocity = Vector2.zero;
        _animator.SetFloat(YVelocity, 0);
        _animator.SetTrigger(Grounded);
        _animator.SetBool(IsJumping, false);
    }

    private void FixedUpdate() {
        _animator.SetFloat(YVelocity, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!_isGrounded && collision.gameObject.CompareTag("Floor")) {
            DoGround();
        }

        if (collision.gameObject.CompareTag("Obstacle")) {
            var health = _gameController.DecHealth(25f);
            if (health <= 0) {
                _isKnockedOut = true;
                _animator.SetBool(KnockedOut, true);
            }
        }

        if (collision.gameObject.CompareTag("Bonus")) {
            _gameController.IncBonus();
        }
    }
}