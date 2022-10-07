using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementAnimator : MonoBehaviour
    {
        private IPlayerInputs _playerInputs;
        private Animator _characterAnimator;
        private CharacterController _characterController;

        public void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _characterAnimator = GetComponentInChildren<Animator>();
        }

        public void Start()
        {
            _playerInputs = new PlayerInputs();
        }

        private void Update()
        {
            _characterAnimator?.SetBool("Grounded", _characterController.isGrounded);
            if (_characterController.isGrounded)
            {
                Vector2 newMove = new()
                {
                    x = _playerInputs.Vertical,
                    y = _playerInputs.Horizontal
                };
                _characterAnimator?.SetBool("Walk", newMove.magnitude > 0);
            }
            else
            {
                _characterAnimator?.SetBool("Walk", false);
            }
        }
    }
}