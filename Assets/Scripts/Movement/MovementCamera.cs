using System.Collections;
using UnityEngine;

namespace Movement
{
    /**
     * This should be attached to the Character
     * The hycheracy should look like this, the CameraParent is the point of interest
     * | GameObjectCharacter
     *   + CharacterModel
     *   + GameObjectCameraParent
     *     - Camera  
     **/
    public class MovementCamera : MonoBehaviour
    {

        public float LookSpeed = 2f;
        public float LookXLimit = 60f;

        public Transform PlayerCameraParent;
        public IPlayerInputs PlayerInputs { set; get; }

        private Vector2 _cameraRotation = Vector2.zero;

        void Start()
        {
            _cameraRotation.y = transform.eulerAngles.y;
            _cameraRotation.x = PlayerCameraParent.localRotation.x;
            if (PlayerInputs == null)
                PlayerInputs = new PlayerInputs();
        }

        // Update is called once per frame
        void Update()
        {
            _cameraRotation.y += PlayerInputs.LookHorizontal * LookSpeed * Time.deltaTime;
            _cameraRotation.x += -PlayerInputs.LookVertical * LookSpeed * Time.deltaTime;
            _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, -LookXLimit, LookXLimit);
            PlayerCameraParent.localRotation = Quaternion.Euler(_cameraRotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, _cameraRotation.y);
        }
    }
}