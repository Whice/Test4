using Model;
using System;
using UnityEngine;

namespace View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private PlayerView playerViewTemplate = null;
        [SerializeField] private LevelPart levelPartTemplate = null;
        [SerializeField] private LevelPart levelPartStart = null;
        [SerializeField] private LevelPart levelPartFinish = null;

        [SerializeField] private int levelPartCount = 10;
        [SerializeField] private Transform levelPartsParent = null;

        private PlayerView playerView;

        public Level level { get; private set; }

        private LevelPart[] levelParts;
        private void CreateLevelParts()
        {
            levelParts[0] = Instantiate(levelPartStart, levelPartsParent);
            playerView.transform.SetParent(levelParts[0].transform, true);
            playerView.transform.localPosition = levelParts[0].playerStartPoint.localPosition;
            playerView.transform.SetParent(transform, true);
            levelParts[levelParts.Length - 1] = Instantiate(levelPartFinish, levelPartsParent);

            for (int i = 0; i < levelPartCount; i++)
            {
                levelParts[i + 1] = Instantiate(levelPartTemplate, levelPartsParent);
            }

            for (int i = 0; i < levelParts.Length; i++)
            {
                levelParts[i].transform.localPosition = new Vector3
                    (
                    (levelParts[i].size + 2.5f) * i,
                    0,
                    0
                    );
            }
        }
        public void ResetView()
        {
            level.ResetLevel();
            playerView.ResetPlayer();
            foreach (LevelPart part in levelParts)
            {
                Destroy(part.gameObject);
            }
            CreateLevelParts();
        }
        public event Action<bool> gameFinished;
        private void OnGameFinished(bool isWin)
        {
            gameFinished?.Invoke(isWin);
        }
        public void Initialize()
        {
            level = new Level();

            playerView = Instantiate(playerViewTemplate, transform);
            playerView.Initialize(level.player);
            playerView.playerLoosed += () => { OnGameFinished(false); };

            levelParts = new LevelPart[levelPartCount + 2];
            CreateLevelParts();
        }
        private void Update()
        {
            level.Tick(Time.time);

            if (playerView.position.x > levelParts[levelParts.Length - 1].playerFinisPoint.transform.position.x)
            {
                OnGameFinished(true);
            }
        }
    }
}