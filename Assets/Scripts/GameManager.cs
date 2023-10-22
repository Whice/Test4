using UI;
using UnityEngine;
using View;

/* Использована система типа MV*. Не в чистом виде, в чистом виде они почти не встречаются на практике,
 * а если встречаются, то работют не эффективно.
 * Модельные данные хранят и меняют состояние внутри самой модели.
 * Классы представления передают информацию о событиях в модельную часть, чтобы она решила,
 * как изменить своё состояние, а потом подстраиваются под ее новое состояние.
 * 
 * Для системы баффов был реализован пулл объектов. Сейчас толку от него мало, т.к. баффа не складываются,
 * но если такие появятся, то от неё толк будет.
 * 
 * С точки зения производительности в коде все плохо, т.к. это не было целью при написании.
 * Производительности для задачи, которая была выполнена, более чем хватает.
 * 
 * Условие о нередактировании класса игрока при добавлении нового баффа не может быть всегда выполнимо,
 * т.к. если бафф должен менять что-то, чего у игрока еще нет, то в любом случае это придется добавить.
 * Например, если надо добавить бафф, который увеличивает ХП игрока в 2 раза, то он будет бесполезен, если у игрока нет ХП.
 * 
 * ECS, которые можно было бы применить тут, не был использован, 
 * т.к. его основная цель была бы увеличить производительность, что не требовалось в описании ТЗ.
 */

/// <summary>
/// Класс объединяет системы скриптов 3D и интерфейса.
/// </summary>
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
