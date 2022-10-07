using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementBehaviour : MonoBehaviour
    {
        public float Speed { set; get; }
        public float Gravity { set; get; }
        public float JumpForce { set; get; }

        public IPlayerInputs PlayerInputs { set; get; }

        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Start()
        {

            if (PlayerInputs == null)
            {
                PlayerInputs = new PlayerInputs();
                Speed = 2f;
                Gravity = 9.81f;
                JumpForce = 6;
            }
        }

        void FixedUpdate()
        {
            if (_characterController.isGrounded)
            {
                _moveDirection.y = 0;
                Vector3 forward = transform.TransformDirection(Vector3.forward) * PlayerInputs.Vertical;
                Vector3 right = transform.TransformDirection(Vector3.right) * PlayerInputs.Horizontal;
                Vector3 newMove = forward + right;
                newMove *= Speed;

                _moveDirection = newMove;

                if (PlayerInputs.Jump)
                {
                    _moveDirection.y = JumpForce;
                }
            }

            _moveDirection.y -= Gravity * Time.fixedDeltaTime;
            _characterController.Move(_moveDirection * Time.fixedDeltaTime);
        }
    }
}