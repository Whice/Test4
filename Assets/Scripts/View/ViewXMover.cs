using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    /// <summary>
    /// Скрипт для передвижения объекта.
    /// </summary>
    public class ViewXMover : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        public void UpdatePosition(float speedMultiplier)
        {
            Vector3 position = transform.position;
            float deltaTime = Time.deltaTime;
            float posx = position.x + speed * speedMultiplier * deltaTime;
            transform.position = new Vector3
                (
                    posx,
                    position.y,
                    position.z
                );
        }
    }
}