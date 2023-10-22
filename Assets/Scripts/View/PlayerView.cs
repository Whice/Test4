using Model;
using Unity.VisualScripting;
using UnityEngine;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 5f;
        [SerializeField] private float jumpForce = 400f;
        [SerializeField] private float maxFlyHeight = 6f;
        [SerializeField] private float liftingTime = 1f;
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
            if (player != null)
            {
                player.isPlayerMustFlyChanged -= OnIsPlayerMustFlyChanged;
            }
            this.player = player;
            player.isPlayerMustFlyChanged += OnIsPlayerMustFlyChanged;
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
        private void Update()
        {
            if (player != null)
            {
                float deltaTime = Time.deltaTime;
                float posx = position.x + playerSpeed * player.speedMultiplier * deltaTime;
                position = new Vector3
                    (
                        posx,
                        position.y,
                        position.z
                    );

                //Игрок может либо летать, либо прыгать.
                if(player.isPlayerMustFly)
                {
                    liftingTimeLeft+= deltaTime;
                    Transform rbTransform = playerRigidbody.transform;
                    Vector3 pos = rbTransform.localPosition;
                    if (rbTransform.localPosition.y < maxFlyHeight)
                    {
                        rbTransform.localPosition = new Vector3
                            (
                                pos.x,
                                Mathf.Lerp(startYPosition, maxFlyHeight, liftingTimeLeft / liftingTime),
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