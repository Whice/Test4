using UnityEngine;

namespace View
{
    /// <summary>
    /// Скрипт нужен для передачи ссылки на представление бонуса.
    /// </summary>
    public class BonusViewReference : MonoBehaviour
    {
        public BonusView bonusView { get; private set; }
        public void Initialize(BonusView view)
        {
            bonusView = view;   
        }
    }
}