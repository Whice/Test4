using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace UI
{
    public class GameInterface : MonoBehaviour
    {
        [SerializeField] private LevelView levelView = null;

        [SerializeField] private Button restartButton = null;
        [SerializeField] private TextMeshProUGUI timeTMP = null;
        [SerializeField] private TextMeshProUGUI scoreTMP = null;


        private float startTime;
        private void OnScoreChanged()
        {
            scoreTMP.text = $"Score: {levelView.level.player.score}";
        }
        private void Update()
        {
            timeTMP.text = $"Time left: {(int)(Time.time - startTime)}";
        }
        private void ResetGame()
        {
            startTime = Time.time;
            levelView.ResetView();
            OnScoreChanged();
        }
        private void Awake()
        {
            levelView.Initialize();
            OnScoreChanged();
            levelView.level.player.scoreChanged += OnScoreChanged;
            restartButton.onClick.AddListener(ResetGame);
        }
    }

}