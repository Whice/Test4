using System;

namespace Model
{
    /// <summary>
    /// Класс игрока.
    /// Содрежит информацию о состоянии игрока.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Множитель скорости игрока.
        /// </summary>
        public float speedMultiplier;
        public void ResetSpeedMultiplier()
        {
            speedMultiplier = 1f;
        }

        /// <summary>
        /// Изменилось условие должен ли игрок летать.
        /// </summary>
        public event Action<bool> isPlayerMustFlyChanged;
        /// <summary>
        /// Игрок должен летать.
        /// </summary>
        private bool _isPlayerMustFly;
        /// <summary>
        /// Игрок должен летать.
        /// </summary>
        public bool isPlayerMustFly
        {
            get
            {
                return _isPlayerMustFly;
            }
            set
            {
                if (_isPlayerMustFly != value)
                {
                    _isPlayerMustFly = value;
                    isPlayerMustFlyChanged?.Invoke(value);
                }
            }
        }

        public int score { get; private set; }
        public event Action scoreChanged;
        public void AddScore(int value)
        {
            score += value;
            scoreChanged?.Invoke();
        }

        /// <summary>
        /// Добавить бонус игроку.
        /// </summary>
        /// <param name="id"></param>
        public void AddBonus(int id)
        {
            buffsManager.SetBuff(id);
        }

        public event Action finished;
        public void OnPlayerFinished()
        {
            finished?.Invoke();
        }

        private BuffManager buffsManager;

        /// <summary>
        /// Обновить внутренние данные.
        /// </summary>
        /// <param name="nowTime"></param>
        public void Tick(float nowTime)
        {
            buffsManager.Tick(nowTime);
        }
        public void ResetPlayer()
        {
            ResetSpeedMultiplier();
            _isPlayerMustFly = false;
            score = 0;

            buffsManager?.Dispose();
        }
        public Player()
        {
            buffsManager = new BuffManager(this);
            ResetPlayer();
        }
    }
}