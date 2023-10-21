using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Player
    {
        /// <summary>
        /// Множитель скорости игрока.
        /// </summary>
        public float speedMultiplier;
        public void ResetSpeedMultiplier()
        {
            speedMultiplier = 1f;
        }

        /// <summary>
        /// Изменилось условие должен ли игрок летать.
        /// </summary>
        public event Action<bool> isPlayerMustFlyChanged;
        /// <summary>
        /// Игрок должен летать.
        /// </summary>
        private bool _isPlayerMustFly;
        /// <summary>
        /// Игрок должен летать.
        /// </summary>
        public bool isPlayerMustFly
        {
            get
            {
                return _isPlayerMustFly;
            }
            set
            {
                if(_isPlayerMustFly != value)
                {
                    _isPlayerMustFly = value;
                    isPlayerMustFlyChanged?.Invoke(value);
                }
            }
        }

        public void ResetPlayer()
        {
            ResetSpeedMultiplier();
            _isPlayerMustFly = false;
        }
        public Player()
        {
            ResetPlayer();
        }
    }
}