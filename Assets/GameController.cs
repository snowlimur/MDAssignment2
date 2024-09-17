using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public float health;
    public float distance;
    public int bonuses;

    public MapController mapController;
    public PlayerController playerController;
    public InGameLayoutController inGameLayoutController;
    public ScoreBoardController scoreBoard;

    private bool _isPaused;
    private bool _isGameOver;
    private bool _isStopped;

    void Start() {
    }

    void Update() {
        if (_isGameOver && !_isStopped) {
            _isStopped = true;
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            HandlePause();
        }

        if (_isPaused) {
            return;
        }

        inGameLayoutController.UpdateScore(distance, bonuses);
    }

    public void OnPauseButtonClick() {
        HandlePause();
    }

    private void HandlePause() {
        _isPaused = !_isPaused;
        if (_isPaused) {
            inGameLayoutController.Pause();
        }
        else {
            inGameLayoutController.Continue();
        }
    }

    public void GameOver() {
        _isGameOver = true;
        inGameLayoutController.Hide();
        mapController.Reset();

        scoreBoard.UpdateScore(distance, bonuses);
        scoreBoard.Show();
    }

    public void Exit() {
        Application.Quit();
    }

    public void Restart() {
        _isPaused = false;
        _isGameOver = false;
        _isStopped = false;

        playerController.Reset();
        
        scoreBoard.Hide();
        inGameLayoutController.UpdateScore(0, 0);
        inGameLayoutController.Continue();
        
        distance = 0f;
        health = 100f;
        bonuses = 0;
        mapController.Run();
    }

    public float DecHealth(float wound) {
        health -= wound;
        if (health <= 0) {
            health = 0;
            _isGameOver = true;
        }

        return health;
    }

    public void IncDistance(float value) {
        distance += value;
    }

    public int IncBonus() {
        bonuses++;
        return bonuses;
    }

    public bool IsPaused() {
        return _isPaused;
    }
}