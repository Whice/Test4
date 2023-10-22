using Model;
using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Предсталение игрока в пространстве.
    /// </summary>
    [RequireComponent(typeof(ViewXMover))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 400f;
        [SerializeField] private float maxFlyHeight = 6f;
        /// <summary>
        /// 
        /// Максильмальное время подуъема игрока, когда он летит.
        /// </summary>
        [SerializeField] private float maxLiftingTime = 1f;
        [SerializeField] private Rigidbody playerRigidbody = null;
        [SerializeField] private BodyCollisionEvents playerBodyCollisionEvents = null;
        private Camera mainCamera;


        public Vector3 position
        {
            get
            {
                return transform.position;
            }
        }

        private Player player;

        private bool isOnFloor;
        private void Jump()
        {
            if (isOnFloor)
            {
                playerRigidbody.AddForce(Vector3.up * jumpForce);
                isOnFloor = false;
            }
        }

        private float liftingTimeLeft;
        private float startYPosition;
        private void OnIsPlayerMustFlyChanged(bool isFly)
        {
            if (playerRigidbody.isKinematic != isFly)
            {
                playerRigidbody.isKinematic = isFly;
                if (isFly)
                {
                    liftingTimeLeft = 0;
                    startYPosition = playerRigidbody.transform.position.y;
                    isOnFloor = false;
                }
            }
        }

        public event Action playerLoosed;
        private ViewXMover viewXMover;
        private void Update()
        {
            if (player != null)
            {
                Transform rbTransform = playerRigidbody.transform;
                float deltaTime = Time.deltaTime;
                viewXMover.UpdatePosition(player.speedMultiplier); 
                mainCamera.transform.position = new Vector3
                    (
                     transform.position.x,
                     mainCamera.transform.position.y,
                     mainCamera.transform.position.z
                    );

                //Игрок может либо летать, либо прыгать.
                if (player.isPlayerMustFly)
                {
                    liftingTimeLeft += deltaTime;
                    Vector3 pos = rbTransform.localPosition;
                    if (rbTransform.localPosition.y < maxFlyHeight)
                    {
                        rbTransform.localPosition = new Vector3
                            (
                                pos.x,
                                Mathf.Lerp(startYPosition, maxFlyHeight, liftingTimeLeft / maxLiftingTime),
                                pos.z
                            );
                    }
                    else
                    {
                        rbTransform.localPosition = new Vector3
                            (
                                pos.x,
                                maxFlyHeight,
                                pos.z
                            );
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    Jump();
                }

                if (rbTransform.localPosition.y < 0)
                {
                    playerLoosed?.Invoke();
                }
            }
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
        public void ResetPlayer()
        {
            playerRigidbody.transform.rotation = Quaternion.identity;
            playerRigidbody.transform.localPosition = Vector3.up;
        }
        public void Initialize(Player player)
        {
            if (player != null)
            {
                player.isPlayerMustFlyChanged -= OnIsPlayerMustFlyChanged;
            }
            this.player = player;
            player.isPlayerMustFlyChanged += OnIsPlayerMustFlyChanged;
        }
        private void Awake()
        {
            viewXMover = GetComponent<ViewXMover>();    
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