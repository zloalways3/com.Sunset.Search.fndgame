using Other;
using TMPro;
using UnityEngine;

namespace System
{
    public class GameLevelCoordinator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _timerTextArray;
        
        private TimerHandler _timerHandler;
        private GameEndProgressVisualizer _gameEndProgressVisualizer;
        private PointGuardian _pointGuardian;
        private int _levelIndex;
        private bool _isNotPause;

        private void Start()
        {
            _isNotPause = true;
            SetupComponents();
            ConfigureTimer();
            Time.timeScale = 1f;
        }

        private void SetupComponents()
        {
            _levelIndex = PlayerPrefs.GetInt("ActiveLevel", 0);
            _timerHandler = gameObject.AddComponent<TimerHandler>();
            _gameEndProgressVisualizer = FindObjectOfType<GameEndProgressVisualizer>();
            _pointGuardian = gameObject.AddComponent<PointGuardian>();
        }

        private void ConfigureTimer()
        {
            _timerHandler.Setup(60f);
        }

        private void Update()
        {
            if (!_timerHandler.IsActive) return;
            if (_isNotPause)
            {
                RefreshTimer();
            }

            if (_pointGuardian.HasSufficientPoints(_levelIndex * ConstVault.POINT_MULTIPLIER))
            {
                ProcessTimeOut();
            }
        }

        private void RefreshTimer()
        {
            _timerHandler.Tick();
            foreach (var arrayNumber in _timerTextArray)
            {
                arrayNumber.text = "Time:\n" + Mathf.Ceil(_timerHandler.RemainingTime).ToString("00") +"s";
            }
            if (_timerHandler.RemainingTime <= 0)
            {
                ProcessTimeOut();
            }
        }

        private void ProcessTimeOut()
        {
            if (_pointGuardian.HasSufficientPoints(_levelIndex * ConstVault.POINT_MULTIPLIER))
            {
                _gameEndProgressVisualizer.DisplaySuccessMessage();
            }
            else
            {
                _gameEndProgressVisualizer.DisplayFailureMessage();
            }

            GameEndProgressVisualizer.GameExitPortal();
        }
        
        public void OnTimeScale()
        {
            _isNotPause = true;
        }
    
        public void OffTimeScale()
        {
            _isNotPause = false;
        }
        
    }
}