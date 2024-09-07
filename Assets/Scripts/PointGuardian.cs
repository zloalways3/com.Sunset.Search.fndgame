using System;
using UnityEngine;

public class PointGuardian : MonoBehaviour
{
    private ScoreSystem _scoreSystem;

    private void Start()
    {
        _scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    private int GetCurrentScore()
    {
        return _scoreSystem.GetScore();
    }

    public bool HasSufficientPoints(int levelMultiplier)
    {
        return levelMultiplier <= GetCurrentScore();
    }
}