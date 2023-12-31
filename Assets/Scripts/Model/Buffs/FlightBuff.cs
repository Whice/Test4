﻿using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Бафф полета. Сообщает о том, что игрок должен лететь.
    /// </summary>
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
            if (player != null)
                player.isPlayerMustFly = false;
        }
        public FlightBuff() : base(BUFF_ID) { }
    }
}