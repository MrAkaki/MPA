using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementBehaviour : MonoBehaviour
    {
        public IPlayerInputs PlayerInputs { set; get; }
        [SerializeField]
        public float Speed { set; get; }
        [SerializeField]
        public float Gravity { set; get; }
        [SerializeField]
        public float JumpForce { set; get; }

        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;

        private void Awake()
        {

        }

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            if (PlayerInputs == null)
            {
                PlayerInputs = new PlayerInputs();
                Speed = 10f;
                Gravity = 9.81f;
                JumpForce = 4;
            }
        }

        void FixedUpdate()
        {
            if (_characterController.isGrounded)
            {
                _moveDirection.x = PlayerInputs.Horizontal * Speed;
                _moveDirection.z = PlayerInputs.Vertical * Speed;
                if (PlayerInputs.Jump) _moveDirection.y += JumpForce;
            }
            _moveDirection.y -= Gravity * Time.fixedDeltaTime;
            _characterController.Move(_moveDirection * Time.fixedDeltaTime);
        }
    }
}