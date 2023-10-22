using Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameInterface : MonoBehaviour
    {
        [SerializeField] private Button restartButton = null;
        [SerializeField] private TextMeshProUGUI timeTMP = null;
        [SerializeField] private TextMeshProUGUI scoreTMP = null;
        [SerializeField] private TextMeshProUGUI endGameInfoTMP = null;

        private Player player;
        private float startTime;
        private void OnScoreChanged()
        {
            scoreTMP.text = $"Score: {player.score}";
        }
        private void Update()
        {
            timeTMP.text = $"Time left: {(int)(Time.time - startTime)}";
        }
        public event Action gameReseted;
        private void ResetGame()
        {
            gameReseted?.Invoke();
            endGameInfoTMP.gameObject.SetActive(false);
            startTime = Time.time;
            OnScoreChanged();
        }
        public void EndGameInfoShow(bool isWin)
        {
            endGameInfoTMP.gameObject.SetActive(true);
            if (isWin)
                endGameInfoTMP.text = $"You win!\nScore: {scoreTMP.text}\nTime: {timeTMP.text}";
            else
                endGameInfoTMP.text = "You loose!";
        }
        public void Initialize(Player player)
        {
            this.player = player;
            OnScoreChanged();
            player.scoreChanged += OnScoreChanged;
            restartButton.onClick.AddListener(ResetGame);
        }
    }

}