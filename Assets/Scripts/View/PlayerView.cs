using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float playerSpeed =5f;
        [SerializeField] private float jumpForce =400f;
        [SerializeField] private Rigidbody playerRigidbody = null;
        [SerializeField] private BodyCollisionEvents playerBodyCollisionEvents = null;
        private Camera mainCamera;

        private Vector3 position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
                mainCamera.transform.position = new Vector3
                    (
                     value.x,
                     mainCamera.transform.position.y,
                     mainCamera.transform.position.z
                    );
            }
        }

        private bool isOnFloor;
        private void Update()
        {
            float posx = position.x + playerSpeed * Time.deltaTime;
            position = new Vector3
                (
                posx,
                position.y,
                position.z
                );

            if (isOnFloor && Input.GetKey(KeyCode.UpArrow))
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isOnFloor = false;
            }
        }

        private void OnCollisionEntered(Collision collision)
        {
            if (collision.gameObject.name.ToLower().Contains("floor"))
            {
                isOnFloor = true;
            }
        }

        private void Awake()
        {
            isOnFloor = true;
            mainCamera = Camera.main;
            playerBodyCollisionEvents.collisionEntered += OnCollisionEntered;
        }
        private void OnDestroy()
        {
            playerBodyCollisionEvents.collisionEntered -= OnCollisionEntered;
        }
    }
}