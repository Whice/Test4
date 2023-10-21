namespace Model
{
    public class Level
    {
        public Player player { get; private set; }
        private BufffManager buffsManager;

        private void ResetLevel()
        {
            player = new Player();
            buffsManager?.Dispose();
            buffsManager = new BufffManager(player);
        }
        public void AddBuffToPlayer(int id)
        {
            buffsManager.SetBuff(id);
        }
        public Level()
        {
            ResetLevel();
        }
    }
}