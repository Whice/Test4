using UI;
using UnityEngine;
using View;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameInterface gameInterface = null;
    [SerializeField] private LevelView levelView = null;

    private bool isGameStart;
    private void OnGameReseted()
    {
        levelView.ResetView();
        isGameStart = true;
    }
    private void OnGameFinished(bool isWin)
    {
        if (isGameStart)
        {
            isGameStart = false;
            gameInterface.EndGameInfoShow(isWin);
        }
    }
    private void Awake()
    {
        levelView.Initialize();
        gameInterface.Initialize(levelView.level.player);
        gameInterface.gameReseted += OnGameReseted;
        levelView.gameFinished += OnGameFinished;
        isGameStart = true;
    }
}
