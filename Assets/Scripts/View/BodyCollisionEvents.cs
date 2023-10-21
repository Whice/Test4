using System;
using UnityEngine;

namespace View
{
    public class BodyCollisionEvents : MonoBehaviour
    {
        public event Action<Collision> collisionEntered;
        public event Action<Collision> collisionExited;
        public event Action<Collider> triggerEntered;
        public event Action<Collider> triggerExited;

        private void OnCollisionEnter(Collision collision)
        {
            collisionEntered?.Invoke(collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            collisionExited?.Invoke(collision);
        }
        private void OnTriggerEnter(Collider other)
        {
            triggerEntered?.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            triggerExited?.Invoke(other);
        }
    }
}