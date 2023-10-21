using System;

namespace Model
{
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
        /// Бонус был добавлен.
        /// В аргументе id.
        /// </summary>
        public event Action<int> bonusAdded;
        /// <summary>
        /// Добавить бонус игроку.
        /// </summary>
        /// <param name="id"></param>
        public void AddBonus(int id)
        {
            bonusAdded?.Invoke(id);
        }

        public event Action finished;
        public void OnPlayerFinished()
        {
            finished?.Invoke();
        }

        public void ResetPlayer()
        {
            ResetSpeedMultiplier();
            _isPlayerMustFly = false;
            score = 0;
        }
        public Player()
        {
            ResetPlayer();
        }
    }
}