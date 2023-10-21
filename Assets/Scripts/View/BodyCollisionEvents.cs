using System;
using UnityEngine;

namespace View
{
    public class BodyCollisionEvents : MonoBehaviour
    {
        public event Action<Collision> collisionEntered;
        public event Action<Collision> collisionExited;

        private void OnCollisionEnter(Collision collision)
        {
            collisionEntered?.Invoke(collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            collisionExited?.Invoke(collision);
        }
    }
}