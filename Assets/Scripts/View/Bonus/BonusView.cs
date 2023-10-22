using UnityEngine;

namespace View
{
    /// <summary>
    /// Бонус, который будет персонаж подбирать.
    /// </summary>
    public class BonusView : MonoBehaviour
    {
        /// <summary>
        /// Идентификатор, который должен совпадать с id баффа.
        /// </summary>
        [SerializeField] private int _id = 0;
        [SerializeField] private BonusViewReference bonusViewReference = null;

        /// <summary>
        /// Идентификатор, который должен совпадать с id баффа.
        /// </summary>
        public int id
        {
            get => _id;
        }
        private void Awake()
        {
            if (id == 0)
                Debug.LogError("ID must be more 0!");
            bonusViewReference.Initialize(this);
        }
    }
}