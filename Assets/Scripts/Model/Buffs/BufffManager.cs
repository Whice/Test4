﻿using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Управляет баффами для установленного игрока.
    /// </summary>
    public class BufffManager
    {
        private Player player;

        /// <summary>
        /// Неактивные баффы.
        /// </summary>
        private Dictionary<int, Stack<IBuff>> buffsByID = new Dictionary<int, Stack<IBuff>>();
        /// <summary>
        /// Активные баффы, висят на игроке.
        /// </summary>
        private List<IBuff> activeBuffs = new List<IBuff>();
        private void RemoveFromActive(IBuff buff)
        {
            activeBuffs.Remove(buff);
            buffsByID[buff.id].Push(buff);
        }
        private IBuff GetNewBuff(int id)
        {
            if (!buffsByID.ContainsKey(id))
            {
                buffsByID.Add(id, new Stack<IBuff>());
            }
            if (buffsByID[id].Count == 0)
            {
                IBuff newBuff = null;

                switch (id)
                {
                    case AccelerationBuff.BUFF_ID:
                        {
                            newBuff = new AccelerationBuff();
                            break;
                        }
                    case DecelerationBuff.BUFF_ID:
                        {
                            newBuff = new DecelerationBuff();
                            break;
                        }
                    case FlightBuff.BUFF_ID:
                        {
                            newBuff = new FlightBuff();
                            break;
                        }
                    default:
                        {
                            //Todo напечатать ошибку.
                            break;
                        }
                }

                if (newBuff != null)
                {
                    buffsByID[id].Push(newBuff);
                    newBuff.buffRolledBack += RemoveFromActive;
                }
            }

            return buffsByID[id].Pop();
        }
        /// <summary>
        /// Добавить игроку бафф.
        /// </summary>
        /// <param name="id"></param>
        public void SetBuff(int id)
        {
            IBuff newBuff = GetNewBuff(id);
            newBuff.SetEffect(player, activeBuffs);
        }

        /// <summary>
        /// Обновить время жизни для баффов.
        /// </summary>
        /// <param name="nowTime"></param>
        public void Tick(float nowTime)
        {
            for (int i = 0; i < activeBuffs.Count; i++)
            {
                activeBuffs[i].Tick(nowTime);
            }
        }

        public BufffManager(Player player)
        {
            this.player = player;
        }

        public void Dispose()
        {
            foreach (Stack<IBuff> stack in buffsByID.Values)
            {
                while (stack.Count != 0)
                {
                    activeBuffs.Add(stack.Pop());
                }
            }
            foreach (IBuff buff in activeBuffs)
            {
                buff.Dispose();
            }
            activeBuffs.Clear();
        }
    }
}