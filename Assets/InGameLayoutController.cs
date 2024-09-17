using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class InGameLayoutController : MonoBehaviour {
    
    public GameObject pauseBackground;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bonusesText;
    public Sprite pauseIcon;
    public Sprite playIcon;

    public void Start() {
        Continue();
    }

    public void UpdateScore(float distance, int bonuses) {
        distanceText.text = distance.ToString("0");
        bonusesText.text = bonuses.ToString("0");
    }

    public void Pause() {
        pauseBackground.SetActive(true);
        // pauseButtonIcon.sprite = playIcon;
    }

    public void Continue() {
        gameObject.SetActive(true);
        pauseBackground.SetActive(false);
        // pauseButtonIcon.sprite = pauseIcon;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}