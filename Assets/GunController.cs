using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GunController : MonoBehaviour {
    public GameObject bullet;
    public float timeBetweenBullets = 1f;
    public float barrelLength = 1f;
    public AnimationClip fireAnimation;

    private float _timer;
    private GameController _gameController;
    private Animator _animator;
    private bool _isFiring;
    private bool _isStopped = true;

    private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");


    void Start() {
        _animator = GetComponent<Animator>();
        _gameController = FindObjectOfType<GameController>();
    }

    void Update() {
        if (_gameController.IsPaused()) {
            if (!_isStopped) {
                _isStopped = true;
                _animator.enabled = false;
            }

            return;
        }

        if (_isStopped) {
            _isStopped = false;
            _animator.enabled = true;
        }

        _timer += Time.deltaTime;

        if (!_isFiring && _timer >= timeBetweenBullets - fireAnimation.length) {
            _isFiring = true;
            _animator.SetBool(IsFiringHash, _isFiring);
        }

        if (_timer >= timeBetweenBullets) {
            _isFiring = false;
            _animator.SetBool(IsFiringHash, _isFiring);
            _timer = 0;
            Instantiate(bullet, transform.position + new Vector3(-1 * barrelLength, 0, 0), transform.rotation);
        }
    }
}