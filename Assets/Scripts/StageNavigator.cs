using Other;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageNavigator : MonoBehaviour
{
    public void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("ProgressTier" + levelIndex, 1);
        PlayerPrefs.Save();
    }

    public void StageElevator(int levelIndex)
    {
        PlayerPrefs.SetInt("ActiveLevel", levelIndex + 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(ConstVault.PIXEL_QUEST);
    }

    public void InitializeLevels()
    {
        for (var i = 0; i < ConstVault.TOTAL_LEVELS; i++)
        {
            if (!PlayerPrefs.HasKey("ProgressTier" + i))
                PlayerPrefs.SetInt("ProgressTier" + i, i == 0 ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt("ProgressTier" + levelIndex, 0) == 1;
    }
}