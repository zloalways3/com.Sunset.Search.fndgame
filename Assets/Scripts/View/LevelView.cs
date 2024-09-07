using Other;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Button[] _levelOptions;

    private StageNavigator _stageNavigator;

    private void Start()
    {
        _stageNavigator = FindObjectOfType<StageNavigator>();
        _stageNavigator.InitializeLevels();
        LevelPalette();
    }

    private void LevelPalette()
    {
        for (var i = 0; i < ConstVault.TOTAL_LEVELS; i++)
        {
            if (i == 0 || _stageNavigator.IsLevelUnlocked(i))
            {
                var levelIndex = i;
                _levelOptions[i].onClick.AddListener(() => _stageNavigator.StageElevator(levelIndex));
            }
            else
            {
                _levelOptions[i].interactable = false;
            }
        }
    }
}