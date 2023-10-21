using Model;
using Unity.VisualScripting;
using UnityEngine;

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

        private Player player;
        public void Initialize(Player player)
        {
            this.player=player;
        }

        private bool isOnFloor;
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

                if (isOnFloor && Input.GetKey(KeyCode.UpArrow))
                {
                    playerRigidbody.AddForce(Vector3.up * jumpForce);
                    isOnFloor = false;
                }
            }
        }

        private void OnCollisionEntered(Collision collision)
        {
            //Простой способ проверить, что игрок на полу, без проброса кучи ссылок.
            if (collision.gameObject.GetComponent<FloorIndicator>() != null)
            {
                isOnFloor = true;
            }
        }
        private void OnTriggerEntered(Collider other)
        {
            BonusViewReference bonusViewReference = other.GetComponent<BonusViewReference>();
            if(bonusViewReference != null )
            {
                player.AddBonus(bonusViewReference.bonusView.id);
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