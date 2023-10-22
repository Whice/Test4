using Providers;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Часть уровня, из таких будет собираться уровень.
    /// </summary>
    public class LevelPart : MonoBehaviour
    {
        [SerializeField] private BonusViewProvider bonusViewProvider = null;
        [SerializeField] private int bonusesCount = 3;
        [SerializeField] private int spaceBetweenBonuses = 3;
        [SerializeField] private Vector2 minMaxBonusHeight = new Vector2(1, 5);
        [SerializeField] private Transform _playerStartPoint = null;
        [SerializeField] private Transform _playerFinisPoint = null;
        [SerializeField] private Transform floor = null;
        public Transform playerStartPoint
        {
            get => _playerStartPoint;
        }
        public Transform playerFinisPoint
        {
            get => _playerFinisPoint;
        }
        public bool isPlayerStart
        {
            get => playerStartPoint != null;
        }
        public bool isPlayerFinish
        {
            get => playerFinisPoint != null;
        }
        public float size
        {
            get => floor.transform.localScale.x;
        }
        private void CreateRandomBonuses()
        {
            for (int i = -bonusesCount; i < bonusesCount; i++)
            {
                int index = Random.Range(0, bonusViewProvider.ids.Length);
                BonusView bonusView = bonusViewProvider.GetPrefabClone(bonusViewProvider.ids[index]);
                bonusView.transform.SetParent(transform);
                float height = Random.Range(minMaxBonusHeight.x, minMaxBonusHeight.y);
                bonusView.transform.localPosition = new Vector3(spaceBetweenBonuses * i, height, 0);
            }
        }
        private void Awake()
        {
            if (!isPlayerStart && !isPlayerFinish)
            {
                CreateRandomBonuses();
            }
        }
    }
}