﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// Бафф влияющий на игрока.
    /// </summary>
    public abstract class IBuff
    {
        public int id { get;protected set; }
        protected Player player;
        private float createTime;
        protected float lifeTime;
        /// <summary>
        /// Применить эффект к игроку.
        /// </summary>
        public virtual void SetEffect(Player player, List<IBuff> activeBuffs)
        {
            this.player = player;
            createTime = Time.time;
            lifeTime = 10f;
        }
        /// <summary>
        /// Отменить из списка все баффы с указаным id.
        /// </summary>
        /// <param name="buffs"></param>
        /// <param name="id"></param>
        protected void UndoAllBuffWithID(List<IBuff> buffs,int id)
        {
            List<IBuff> buffsForRemove = new List<IBuff>();
            foreach (IBuff buff in buffs)
            {
                if (buff.id == id)
                {
                    buffsForRemove.Add(buff);
                }
            }
            foreach (IBuff buff in buffsForRemove)
            {
                buff.UndoEffect();
            }
        }
        /// <summary>
        /// Бафф был отменен.
        /// </summary>
        public event Action<IBuff> buffRolledBack;
        /// <summary>
        /// Отменить эффект наложеный этим баффом.
        /// </summary>
        public virtual void UndoEffect()
        {
            buffRolledBack?.Invoke(this);
        }

        public void Tick(float nowTime)
        {
            if (nowTime - createTime > lifeTime)
            {
                UndoEffect();
            }
        }

        public IBuff(int id)
        {
            this.id = id;
        }
        public void Dispose()
        {
            UndoEffect();
            player = null;
            buffRolledBack = null;
        }
    }
}