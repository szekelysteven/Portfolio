using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        
        #region Private Fields

        [SerializeField]
        private float directionDampTime = 0.25f;
        Animator animator;
        #endregion


        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }
        #region MonoBehaviour Callbacks

        // Update is called once per frame
        void Update()
        {

            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }

            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // only allow jumping if we are running.
            if (stateInfo.IsName("Base Layer.Run"))
            {
                // When using trigger parameter
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("Jump");
                }
            }

            //keyboard controls
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            //mouse controls
            
      
            //sets animator variable speed to vertical input
            animator.SetFloat("Speed", v);
            animator.SetFloat("Strafe", h);

            

           
            
                
            

        }

        #endregion
    }
}
