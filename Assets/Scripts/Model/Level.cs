namespace Model
{
    /// <summary>
    /// Уровень.
    /// Пока что тут можно управлять только игроком.
    /// </summary>
    public class Level
    {
        public Player player { get; private set; }

        /// <summary>
        /// Обновить внутренние данные.
        /// </summary>
        /// <param name="nowTime"></param>
        public void Tick(float nowTime)
        {
            player.Tick(nowTime);
        }
        public void ResetLevel()
        {
            player.ResetPlayer();
        }
        public Level()
        {
            player = new Player();
            ResetLevel();
        }
    }
}