using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceSorcerer : MonoBehaviour
{
    [SerializeField] private Button[] _gameButtons;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _specialSprite;
    
    [SerializeField] private TextMeshProUGUI[] _additionalScoreDisplays;

    public Button[] GameButtons => _gameButtons;

    public void RefreshScoreUI(int score, int level)
    {
        var scoreText = $"Points:\n{score}/{level * ConstVault.POINT_MULTIPLIER}";
        ScoreReveal(scoreText);
    }

    public void ChangeButtonSprite(int buttonIndex, Sprite sprite)
    {
        if (IsIndexValid(buttonIndex))
        {
            _gameButtons[buttonIndex].GetComponent<Image>().sprite = sprite;
        }
    }

    public void RegisterButtonClickListener(int buttonIndex, UnityEngine.Events.UnityAction callback)
    {
        if (IsIndexValid(buttonIndex))
        {
            _gameButtons[buttonIndex].onClick.AddListener(callback);
        }
    }

    public Sprite GetDefaultSprite() => _defaultSprite;
    public Sprite GetSpecialSprite() => _specialSprite;

    private void ScoreReveal(string text)
    {
        foreach (var display in _additionalScoreDisplays)
        {
            display.text = text;
        }
    }

    private bool IsIndexValid(int index) => index >= 0 && index < _gameButtons.Length;
}