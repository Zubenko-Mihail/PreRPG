using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject oporaY, cam;
    Vector3 prevRot, startPos;
    Quaternion startRot;
    Controls controls;
    RaycastHit hit;
    Ray ray;
    float rayRange;
    private void Awake()
    {
        oporaY = GameObject.Find("OporaY");
        cam = GameObject.Find("Main Camera");
        startRot = oporaY.transform.localRotation;
        controls = GetComponent<Controls>();
        startPos = cam.transform.localPosition;
    }
    private void Start()
    {
        rayRange = Vector3.Distance(cam.transform.position, oporaY.transform.position);
        ray = new Ray(oporaY.transform.position, cam.transform.position - oporaY.transform.position);
        if (Physics.Raycast(ray, out hit, rayRange))
        {
            cam.transform.position = hit.point - (cam.transform.position - oporaY.transform.position) * 0.2f;
        }
        else
        {
            cam.transform.localPosition = startPos;
        }
    }
    void FixedUpdate()
    {
        MoveCam();
    }
    private void Update()
    {
        UpdateOporaPos();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            controls.canInteract = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            oporaY.transform.localRotation = startRot;
        }
    }
    void MoveCam()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            controls.canInteract = false;
            prevRot = oporaY.transform.localEulerAngles;
            oporaY.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * 2, Space.World);
            oporaY.transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * 2, Space.Self);
            if (oporaY.transform.localEulerAngles.z > 100)
            {
                oporaY.transform.localRotation = Quaternion.Euler(prevRot);
            }
            ray = new Ray(oporaY.transform.position, cam.transform.position - oporaY.transform.position);
            if (Physics.Raycast(ray, out hit, rayRange))
            {
                cam.transform.position = hit.point - (hit.point - oporaY.transform.position) * 0.2f;
            }
            else
            {
                cam.transform.localPosition = startPos;
            }
        }
        print(controls.isRunning);
        if (controls.isRunning)
        {
            ray = new Ray(oporaY.transform.position, cam.transform.position - oporaY.transform.position);
            if (Physics.Raycast(ray, out hit, rayRange))
            {
                cam.transform.position = hit.point - (hit.point - oporaY.transform.position) * 0.2f;
            }
            else
            {
                cam.transform.localPosition = startPos;
            }
        }
        if (hit.point == Vector3.zero)
        {
            print(cam.transform.localPosition.magnitude);
            if (cam.transform.localPosition.magnitude > 3 && Input.mouseScrollDelta.y > 0)
                cam.transform.position += cam.transform.forward * Input.mouseScrollDelta.y;
            if (cam.transform.localPosition.magnitude < 8 && Input.mouseScrollDelta.y < 0)
                cam.transform.position += cam.transform.forward * Input.mouseScrollDelta.y;
            startPos = cam.transform.localPosition;
            rayRange = Vector3.Distance(cam.transform.position, oporaY.transform.position);
        }
    }
    void UpdateOporaPos()
    {
        oporaY.transform.position = transform.position + Vector3.up * 1.5f;
    }
}
