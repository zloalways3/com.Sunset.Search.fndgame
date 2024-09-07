using UnityEngine;

namespace System
{
    public class ScoreSystem : MonoBehaviour
    {
        public delegate void OnScoreChanged(int newScore);
        public event OnScoreChanged ScoreChanged;
        
        private int _score;

        public void ScoreAscend(int points)
        {
            _score += points;
            ScoreChanged?.Invoke(_score);
        }

        public int GetScore()
        {
            return _score;
        }
    }
}
