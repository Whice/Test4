using System.Collections.Generic;

namespace Model
{
    public class DecelerationBuff : IBuff
    {
        protected override int score => 17;
        public const int BUFF_ID = 1;
        public override void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            base.SetEffect(player, activeBuffs);
            UndoAllBuffWithID(activeBuffs, BUFF_ID);
            UndoAllBuffWithID(activeBuffs, AccelerationBuff.BUFF_ID);
            player.speedMultiplier = 0.6f;
        }
        public override void UndoEffect()
        {
            base.UndoEffect();
            player.ResetSpeedMultiplier();
        }

        public DecelerationBuff() : base(BUFF_ID) { }
    }
}