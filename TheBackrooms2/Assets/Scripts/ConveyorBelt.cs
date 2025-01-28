using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBelt : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public List<GameObject> onBelt;
    void Start()
    {

    }

    private void Update()
    {
       for (int i = 0; i <= onBelt.Count -1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().linearVelocity = speed * direction;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
