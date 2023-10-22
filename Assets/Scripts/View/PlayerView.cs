﻿using Model;
using UnityEngine;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 5f;
        [SerializeField] private float jumpForce = 400f;
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

        private Player player;
        public void Initialize(Player player)
        {
            this.player = player;
        }

        private bool isOnFloor;
        private void Jump()
        {
            if (isOnFloor)
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isOnFloor = false;
            }
        }
        private void Update()
        {
            if (player != null)
            {
                float posx = position.x + playerSpeed * player.speedMultiplier * Time.deltaTime;
                position = new Vector3
                    (
                    posx,
                    position.y,
                    position.z
                    );

                if (Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
            }
        }
        public void ResetPlayer()
        {
            playerRigidbody.transform.rotation = Quaternion.identity;
            playerRigidbody.transform.localPosition = Vector3.up;
        }
        private void OnCollisionEntered(Collision collision)
        {
            if (collision.gameObject.GetComponent<FloorIndicator>() != null)
            {
                isOnFloor = true;
            }
        }
        private void OnTriggerEntered(Collider other)
        {
            BonusViewReference bonusViewReference = other.GetComponent<BonusViewReference>();
            if (bonusViewReference != null)
            {
                player.AddBonus(bonusViewReference.bonusView.id);
                bonusViewReference.bonusView.gameObject.SetActive(false);
            }
            if (other.GetComponent<FinishIndicator>() != null)
            {
                player.OnPlayerFinished();
            }
        }
        private void Awake()
        {
            isOnFloor = true;
            mainCamera = Camera.main;
            playerBodyCollisionEvents.collisionEntered += OnCollisionEntered;
            playerBodyCollisionEvents.triggerEntered += OnTriggerEntered;
        }
        private void OnDestroy()
        {
            playerBodyCollisionEvents.collisionEntered -= OnCollisionEntered;
            playerBodyCollisionEvents.triggerEntered -= OnTriggerEntered;
        }
    }
}