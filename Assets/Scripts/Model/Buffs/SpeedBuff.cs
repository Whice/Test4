using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Класс с общими действиями для баффов, изменяющих скорость передвижения.
    /// </summary>
    public abstract class SpeedBuff : IBuff
    {
        public override void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            base.SetEffect(player, activeBuffs);
            //Отменяются все баффы того же типа. Бафф переналожиться и действие будет продлено.
            UndoAllBuffWithID(activeBuffs, id);
        }
        public override void UndoEffect()
        {
            base.UndoEffect();
            player?.ResetSpeedMultiplier();
        }

        public SpeedBuff(int id) : base(id) { }
    }
}