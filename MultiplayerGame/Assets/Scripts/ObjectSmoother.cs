using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


//A script to help with object interpolation on the network
namespace Com.MyCompany.MyGame
{
  public class ObjectSmoother : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField]
        private Rigidbody rigidbody;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
          

        }

    

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(rigidbody.position);
                stream.SendNext(rigidbody.rotation);
                stream.SendNext(rigidbody.linearVelocity);
            }
            else
            {
                rigidbody.position = (Vector3)stream.ReceiveNext();
                rigidbody.rotation = (Quaternion)stream.ReceiveNext();
                rigidbody.linearVelocity = (Vector3)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
                rigidbody.position += rigidbody.linearVelocity * lag;
            }
        }
    }
}
