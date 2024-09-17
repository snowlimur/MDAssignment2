using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreBoardController : MonoBehaviour {
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bonusesText;

    private GameController _gameController;

    void Start() {
        _gameController = FindObjectOfType<GameController>();
        Hide();
    }
    
    public void UpdateScore(float distance, int bonuses) {
        distanceText.text = distance.ToString("0") + "m";
        bonusesText.text = bonuses.ToString("0");
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OnExit() {
        _gameController.Exit();
    }

    public void OnRestart() {
        _gameController.Restart();
    }
}