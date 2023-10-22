using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Бафф ускорения отменяет бафф замедления 
    /// и обновляет время для предыдущего баффа ускорения.
    /// </summary>
    public class AccelerationBuff : SpeedBuff
    {
        protected override int score => 3;
        public const int BUFF_ID = 2;
        public override void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            base.SetEffect(player, activeBuffs);
            UndoAllBuffWithID(activeBuffs, DecelerationBuff.BUFF_ID);
            player.speedMultiplier = 1.7f;
        }
        public AccelerationBuff() : base(BUFF_ID) { }
    }
}