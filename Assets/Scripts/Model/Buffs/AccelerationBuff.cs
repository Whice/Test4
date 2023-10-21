using System.Collections.Generic;

namespace Model
{
    public class AccelerationBuff : IBuff
    {
        public const int BUFF_ID = 2;
        public override void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            base.SetEffect(player, activeBuffs);
            UndoAllBuffWithID(activeBuffs, BUFF_ID);
            UndoAllBuffWithID(activeBuffs, DecelerationBuff.BUFF_ID);
            player.speedMultiplier = 1.7f;
        }
        public override void UndoEffect()
        {
            base.UndoEffect();
            player.ResetSpeedMultiplier();
        }
        public AccelerationBuff() : base(BUFF_ID) { }
    }
}