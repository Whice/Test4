using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Бафф замедления отменяет бафф ускорения 
    /// и обновляет время для предыдущего баффа замедления.
    /// </summary>
    public class DecelerationBuff : SpeedBuff
    {
        protected override int score => 17;
        public const int BUFF_ID = 1;
        public override void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            base.SetEffect(player, activeBuffs);
            UndoAllBuffWithID(activeBuffs, AccelerationBuff.BUFF_ID);
            player.speedMultiplier = 0.6f;
        }

        public DecelerationBuff() : base(BUFF_ID) { }
    }
}