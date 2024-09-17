using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour {
    [FormerlySerializedAs("Sections")] public MapSectionController[] sections;

    [FormerlySerializedAs("Start Section")]
    public MapSectionController startSection;

    [FormerlySerializedAs("Speed")] public float speed = 2;

    [FormerlySerializedAs("Left Edge")] public float leftEdge = -17.3f;

    [FormerlySerializedAs("Start Position")]
    public float startPosition = 11f;

    private MapSectionController _leftSection;
    private MapSectionController _rightSection;
    private GameController _gameController;
    private int _cursor = -1;
    private bool _isStopped;

    void Start() {
        _gameController = FindObjectOfType<GameController>();
        Run();
    }

    void Update() {
        if (_gameController.IsPaused()) {
            if (!_isStopped) {
                Stop();
            }

            return;
        }

        if (_isStopped) {
            Continue();
        }

        _gameController.IncDistance(Time.deltaTime * speed);

        if (_leftSection && _leftSection.X() < leftEdge) {
            _leftSection.Reset();
            _leftSection = _rightSection;

            _rightSection = NextSection();
            var x = _leftSection.X();
            _rightSection.Run(transform.position + new Vector3(x + _leftSection.Length(), 0, 0), speed);
        }
    }

    public void Run() {
        Reset();
        for (var t = 0; t < sections.Length; t++) {
            var tmp = sections[t];
            var r = Random.Range(t, sections.Length);
            sections[t] = sections[r];
            sections[r] = tmp;
        }

        _leftSection = startSection;
        _leftSection.Run(transform.position + new Vector3(startPosition, 0, 0), speed);

        _rightSection = NextSection();
        _rightSection.Run(transform.position + new Vector3(startPosition + _leftSection.Length(), 0, 0), speed);
    }
    
    public void Reset() {
        _isStopped = false;
        if (_leftSection) {
            _leftSection.Reset();
        }

        if (_rightSection) {
            _rightSection.Reset();
        }
    }

    public void Continue() {
        _isStopped = false;
        if (_leftSection) {
            _leftSection.Continue(speed);
        }

        if (_rightSection) {
            _rightSection.Continue(speed);
        }
    }

    public void Stop() {
        _isStopped = true;
        if (_leftSection) {
            _leftSection.Stop();
        }

        if (_rightSection) {
            _rightSection.Stop();
        }
    }

    private MapSectionController NextSection() {
        _cursor = (_cursor + 1) % sections.Length;
        return sections[_cursor];
    }
}