namespace Model
{
    public class Level
    {
        public Player player { get; private set; }
        private BuffManager buffsManager;

        private void ResetLevel()
        {
            player = new Player();
            buffsManager?.Dispose();
            buffsManager = new BuffManager(player);
            player.bonusAdded += buffsManager.SetBuff;
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