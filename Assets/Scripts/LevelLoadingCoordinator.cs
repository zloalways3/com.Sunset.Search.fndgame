using Other;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingCoordinator : MonoBehaviour
{
    public void LoadingChronicles()
    {
        SceneManager.LoadScene(ConstVault.TRANSITION_SCENE);
    }
    public void LoadingGameScene()
    {
        SceneManager.LoadScene(ConstVault.PIXEL_QUEST);
    }
        
}