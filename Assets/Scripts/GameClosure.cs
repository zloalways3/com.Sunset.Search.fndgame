using UnityEngine;

public class GameClosure : MonoBehaviour
{ 
    public void ExitJourney()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}