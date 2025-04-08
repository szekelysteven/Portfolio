using UnityEngine;


namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("The distance in the local x-z plane to the target")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("The height we want the camera to be above the target")]
        [SerializeField]
        private float height = 3.0f;

        [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;

        [Tooltip("The Smoothing for the camera to follow the target")]
        [SerializeField]
        private float smoothSpeed = 0.125f;

        [SerializeField]
        private float mouseSensitivity = 250;

        [SerializeField]
        public Transform virtualCameraTransform;

        [SerializeField]
        public Transform center;


        // cached transform of the target
        Transform cameraTransform;

        //create a vector variable to help change camera rotation and ignore y from camera follow
        Vector3 cameraTransformNew = Vector3.zero;
        
        
        // maintain a flag internally to reconnect if target is lost or camera is switched
        bool isFollowing;

        // Cache for camera offset
        Vector3 cameraOffset = Vector3.zero;

        //get reference for play model
        [SerializeField]
        public Transform playerModel;
        //define animator reference to help turn player model with camera       
        Animator animator;

        //radius for camera orbit
        public float radius = 5f;
        #endregion

        #region MonoBehaviour Callbacks


        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            animator = GetComponent<Animator>();
            cameraTransform = Camera.main.transform;

            //defines the player model as a point to rotate around with camera
            transform.position = (transform.position - playerModel.position).normalized * radius + playerModel.position;

        }


        void LateUpdate()
        {
            cameraTransform.LookAt(center);
            //virtualCameraTransform.LookAt(playerModel);
            PlayerRotateCamera();
            AttachToVirtualCamera();
        }

        #endregion

        #region Public Methods

    
        public void OnStartFollowing()
        {


        }

        #endregion

        #region Private Methods

        
        void AttachToVirtualCamera()
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, virtualCameraTransform.position, smoothSpeed * Time.deltaTime);
        }


        void PlayerRotateCamera()
        {
            //Allow player to control y-axis and x-axis of camera seperate from player script
            //Set restrictions for camera y movement
            
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");
            Debug.Log("" + rotateVertical);
            Debug.Log("" + rotateHorizontal);

            //rotates camera around x-axis using mouse input, sensitivity, and player position
            //virtualCameraTransform.RotateAround(playerModel.position, Vector3.up, rotateHorizontal * 250 * Time.deltaTime);

            //rotates camera around y-axis using mouse input, sensitivity, and player position
            if (Input.GetKey(KeyCode.LeftControl))
            {
                virtualCameraTransform.RotateAround(playerModel.position, Vector3.right, -rotateVertical * 250 * Time.deltaTime);
            }
            animator.transform.Rotate(Vector3.up * rotateHorizontal * (250f * Time.deltaTime));
            //virtualCameraTransform.Rotate(Vector3.up * rotateHorizontal * (250f * Time.deltaTime));
            //cameraTransform.Rotate(Vector3.up * rotateHorizontal * (250f * Time.deltaTime));





            //animator.transform.Rotate(Vector3.right * rotateVertical * (250f * Time.deltaTime));
            //virtualCameraTransform.Rotate(Vector3.right * rotateVertical * (250f * Time.deltaTime));
            //cameraTransform.Rotate(Vector3.right * rotateVertical * (250f * Time.deltaTime));


            //need to ignore y axis rotation to allow player control



            //it just works!



        }




       
        #endregion
    }
}




/*
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform center;
    public float rotationSpeed = 10f;
    public float radius = 5f;

    void Start()
    {
        //Optional: Set initial position on a circle
        transform.position = (transform.position - playerModel.position).normalized * radius + center.position;
    }

    void Update()
    {
        transform.RotateAround(playerModel.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

   //// if (virtualCameraTransform.localRotation.x >= -0.385 && virtualCameraTransform.localRotation.x <= 0.385)
           // {
                //virtualCamera.Rotate(Vector3.right * rotateVertical * (mouseSensitivity * Time.deltaTime));
            //}

            //if (virtualCamera.localRotation.x <= -0.385)
           // {
              //  virtualCamera.Rotate(Vector3.right * -rotateVertical * (mouseSensitivity * Time.deltaTime));

           // }
           // if (virtualCamera.localRotation.x >= 0.385)
           // {
               // virtualCamera.Rotate(Vector3.right * -rotateVertical * (mouseSensitivity * Time.deltaTime));

          //  }

*/