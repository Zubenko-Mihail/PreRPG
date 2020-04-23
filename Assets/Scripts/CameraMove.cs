using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        UsefulThings.inputManager.Gameplay.SetDefaultCamPosition.performed += _ => SetDefaultCamPosition();
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
        if (UsefulThings.mouse.rightButton.wasReleasedThisFrame)
        {
            controls.canInteract = true;
        }
    }
    void MoveCam()
    {
        if (UsefulThings.mouse.rightButton.isPressed)
        {
            controls.canInteract = false;
            prevRot = oporaY.transform.localEulerAngles;
            oporaY.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * 2, Space.World); ;
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
            //print(cam.transform.localPosition.magnitude);
            if (cam.transform.localPosition.magnitude > 3 &&  UsefulThings.mouse.scroll.ReadValue().y > 0)
                cam.transform.position += cam.transform.forward * UsefulThings.mouse.scroll.ReadValue().normalized.y;
            if (cam.transform.localPosition.magnitude < 8 && UsefulThings.mouse.scroll.ReadValue().y < 0)
                cam.transform.position += cam.transform.forward * UsefulThings.mouse.scroll.ReadValue().normalized.y;
            startPos = cam.transform.localPosition;
            rayRange = Vector3.Distance(cam.transform.position, oporaY.transform.position);
        }
    }
    void UpdateOporaPos()
    {
        oporaY.transform.position = transform.position + Vector3.up * 1.5f;
    }
    void SetDefaultCamPosition()
    {
        oporaY.transform.localRotation = startRot;
    }
}
