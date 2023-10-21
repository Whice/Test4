using System.Collections.Generic;

namespace Model
{
    public class FlightBuff : IBuff
    {
        protected override int score => 11;
        public const int BUFF_ID = 3;
        public override void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            base.SetEffect(player, activeBuffs);
            UndoAllBuffWithID(activeBuffs, BUFF_ID);
            player.isPlayerMustFly = true;
        }
        public override void UndoEffect()
        {
            base.UndoEffect();
            player.isPlayerMustFly = false;
        }
        public FlightBuff() : base(BUFF_ID) { }
    }
}