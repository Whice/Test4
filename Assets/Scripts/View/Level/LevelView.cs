using Model;
using System.Collections;
using System.Collections.Generic;
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

        private Level level;

        private LevelPart[] levelParts;
        private void Awake()
        {
            level = new Level();

            playerView = Instantiate(playerViewTemplate, transform);
            playerView.Initialize(level.player);

            levelParts = new LevelPart[levelPartCount + 2];
            levelParts[0] = Instantiate(levelPartStart, levelPartsParent);
            playerView.transform.SetParent(levelParts[0].transform, false);
            playerView.transform.localPosition = levelParts[0].playerStartPoint.localPosition;
            playerView.transform.SetParent(transform, false);
            levelParts[levelParts.Length - 1] = Instantiate(levelPartFinish, levelPartsParent);

            for (int i = 0; i < levelPartCount; i++)
            {
                levelParts[i + 1] = Instantiate(levelPartTemplate, levelPartsParent);
            }

            for (int i = 0; i < levelParts.Length; i++)
            {
                levelParts[i].transform.localPosition = new Vector3
                    (
                    (levelParts[i].size + 0.5f) * i,
                    0,
                    0
                    );
            }
        }
    }
}