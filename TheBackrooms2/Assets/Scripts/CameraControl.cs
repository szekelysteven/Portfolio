using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public Transform camera;
    float xMouse = 0;
    float yMouse = 0;
    float sensitivity = 2;

    //11/14/2021
    //Added seperate camera transform to resolve issue with camera controller incorrectly rotating player model.
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        yMouse = rot.y;
        xMouse = rot.x;
        //Removes cursor from game
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yMouse += Input.GetAxis("Mouse X") * sensitivity;
        xMouse -= Input.GetAxis("Mouse Y") * sensitivity;

        xMouse = Mathf.Clamp(xMouse, -90f, 90f);

        Quaternion playerRotation = Quaternion.Euler(0, yMouse, 0.0f);
        player.transform.rotation = playerRotation;

        Quaternion cameraRotation = Quaternion.Euler(xMouse, yMouse, 0.0f);
        camera.transform.rotation = cameraRotation;

    }
}
