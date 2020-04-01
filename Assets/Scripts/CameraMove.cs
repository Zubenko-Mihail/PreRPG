using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject oporaY, cam;
    Vector3 prevRot;
    Quaternion startRot;
    Controls controls;
    private void Awake()
    {
        oporaY = GameObject.Find("OporaY");
        cam = GameObject.Find("Main Camera");
        startRot = cam.transform.localRotation;
        controls = GetComponent<Controls>();
    }

    void FixedUpdate()
    {
        MoveCam();
        RotateCam();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            cam.transform.localRotation = startRot;
            controls.canInteract = true;
        }

    }
    void MoveCam()
    {
        if (Input.GetKey(KeyCode.RightBracket))
        {
            oporaY.transform.Rotate(Vector3.down, 1);
        }

        if (Input.GetKey(KeyCode.LeftBracket))
        {
            oporaY.transform.Rotate(Vector3.up, 1);
        }

        if (cam.transform.localPosition.magnitude > 5 && Input.mouseScrollDelta.y > 0)
            cam.transform.position += cam.transform.forward * Input.mouseScrollDelta.y;

        if (cam.transform.localPosition.magnitude < 8 && Input.mouseScrollDelta.y < 0)
            cam.transform.position += cam.transform.forward * Input.mouseScrollDelta.y;

    }
    void RotateCam()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            controls.canInteract = false;
            cam.transform.Rotate(Vector3.up , Input.GetAxis("Mouse X") *  2, Space.World);
            cam.transform.Rotate(Vector3.left , Input.GetAxis("Mouse Y") *  2, Space.Self);
            if (cam.transform.localEulerAngles.z > 100)
            {
                cam.transform.localRotation = Quaternion.Euler(prevRot);
            }
            prevRot = cam.transform.localEulerAngles;
        }
    }
}
