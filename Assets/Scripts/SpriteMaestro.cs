using System;
using System.Collections.Generic;
using Other;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpriteMaestro : MonoBehaviour
{
    [SerializeField] private InterfaceSorcerer interfaceSorcerer;

    private readonly HashSet<int> _specialIndices = new HashSet<int>();
    private ScoreSystem _scoreSystem;
    private int _activeLevel;

    private void Awake()
    {
        LevelCrafting();
        AssignButtonClickListeners();
        AssignSpecialSprites();
    }

    private void LevelCrafting()
    {
        _scoreSystem = FindObjectOfType<ScoreSystem>();
        _activeLevel = PlayerPrefs.GetInt("ActiveLevel", 0);

        UpdateUI(0);
    }

    private void UpdateUI(int newScore)
    {
        interfaceSorcerer.RefreshScoreUI(newScore, _activeLevel);

        if (_scoreSystem != null)
        {
            _scoreSystem.ScoreChanged += (score) => interfaceSorcerer.RefreshScoreUI(score, _activeLevel);
        }
    }

    private void AssignButtonClickListeners()
    {
        for (var indexCounter = 0; indexCounter < interfaceSorcerer.GameButtons.Length; indexCounter++)
        {
            var index = indexCounter;
            interfaceSorcerer.RegisterButtonClickListener(index, () => OnButtonClick(index));
        }
    }

    public void VictoryFanfare()
    {
        PlayerPrefs.SetInt("ActiveLevel", _activeLevel + 1);
        PlayerPrefs.Save();
        
        var levelManager = FindObjectOfType<StageNavigator>();
        levelManager.UnlockLevel(_activeLevel);
    }

    private void AddPointsToScore(int points)
    {
        _scoreSystem.ScoreAscend(points);
    }

    private void AssignSpecialSprites()
    {
        while (_specialIndices.Count < ConstVault.NUMBER_DISTINGUISHING_SPRITES)
        {
            var randomIndex = Random.Range(0, interfaceSorcerer.GameButtons.Length);
            if (_specialIndices.Add(randomIndex))
            {
                interfaceSorcerer.ChangeButtonSprite(randomIndex, interfaceSorcerer.GetSpecialSprite());
            }
        }
    }

    private void OnButtonClick(int index)
    {
        var clickedImage = interfaceSorcerer.GameButtons[index].GetComponent<Image>();

        if (clickedImage.sprite != interfaceSorcerer.GetSpecialSprite()) return;

        var normalIndex = FindNormalSpriteIndex();
        if (normalIndex == -1) return;

        AddPointsToScore(_activeLevel * 3);
        UpdateUI(_scoreSystem.GetScore());

        clickedImage.sprite = interfaceSorcerer.GetDefaultSprite();
        interfaceSorcerer.ChangeButtonSprite(normalIndex, interfaceSorcerer.GetSpecialSprite());
    }

    private int FindNormalSpriteIndex()
    {
        while (true)
        {
            var number = Random.Range(0, interfaceSorcerer.GameButtons.Length);
            if (interfaceSorcerer.GameButtons[number].GetComponent<Image>().sprite == interfaceSorcerer.GetDefaultSprite())
                return number;
        }
    }
}