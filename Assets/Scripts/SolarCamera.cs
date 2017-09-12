using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SolarCamera : MonoBehaviour {

    enum CameraState
    {
        Star,
        SolarSystem,
        FreeLook
    }
    private CameraState mCameraState = CameraState.SolarSystem;

    Vector3 mPosition;
    float startingDistance = 1;
    GameObject mEarth, mCamera, mSolarSystem;
    Planet mEarthScript;
    void Start()
    {
        mSolarSystem = GameObject.Find("Solar System");
        mCamera = this.gameObject;
        mEarth = GameObject.Find("Earth");
        mEarthScript = mEarth.GetComponent<Planet>();

        InitialiseCamera();
        mCamera.transform.RotateAround(mSolarSystem.transform.position, mCamera.transform.right, startingRotation);
    }

    public bool autoPilot = false;
    void Update()
    {
        CheckMouse();
        CheckKeyboard();
    }
    private float startingRotation = 25;
    private float cameraDistance = 1000f;
    private float cameraZoomSpeed = 100f;
    private float cameraRotateSpeed = 1f;
    private void CheckMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        switch (mCameraState)
        {
            case CameraState.SolarSystem:
                /*if (Input.GetMouseButton(0) && !MouseAboveUI())
                {
                    if (!mouseWasDownLastFrame)
                    {
                        mouseWasDownLastFrame = true;
                        Cursor.lockState = CursorLockMode.Locked;
                    }

                    if (MouseMoved())
                    {
                    }
                }
                else
                {
                    mouseWasDownLastFrame = false;
                    Cursor.lockState = CursorLockMode.None;
                }*/

                float d = Input.GetAxis("Mouse ScrollWheel");
                if (d > 0f)
                {
                    UpdateCameraDistance(-cameraZoomSpeed);
                }
                else if (d < 0f)
                {
                    UpdateCameraDistance(cameraZoomSpeed);
                }
                break;
            case CameraState.Star:

                break;
            case CameraState.FreeLook:

                break;
        }
    }

    public void UpdateCameraDistance(float zoom)
    {
        cameraDistance += zoom;
        mCamera.transform.position += (-mCamera.transform.forward * zoom);
    }
    /* Method Author: Alex DS  */
    // method which checks for keyboard input, is only called if debuglook camera state is active
    public void CheckKeyboard()
    {
        // reset
        if (Input.GetKey("r"))
        {
            InitialiseCamera();
        }

        switch (mCameraState)
        {
            case CameraState.SolarSystem:
                if (Input.GetKey("w")) // rotate up
                {
                   RotateCameraAxis(new Vector3(1, 0, 0));
                }
                else if (Input.GetKey("s")) // rotate down
                {
                   RotateCameraAxis(new Vector3(-1, 0, 0));
                }

                if (Input.GetKey("a")) // rotate left
                {
                   RotateCameraAxis(new Vector3(0, 1, 0));
                }
                else if (Input.GetKey("d")) // rotate right
                {
                   RotateCameraAxis(new Vector3(0, -1, 0));
                }
                break;
                case CameraState.Star:

                break;
            case CameraState.FreeLook:

                break;
        }
    }

    private void InitialiseCamera()
    {
        // starting values
        mCamera.transform.LookAt(mSolarSystem.transform);
        mCamera.transform.position = (-mCamera.transform.forward * cameraDistance);
    }

    private void RotateCameraAxis(Vector3 axis)
    {
        if (axis.x > 0 || axis.x < 0)
        {
            mCamera.transform.RotateAround(mSolarSystem.transform.position, mCamera.transform.right * axis.x, cameraRotateSpeed);
        }
        else if(axis.y > 0 || axis.y < 0)
        {
            mCamera.transform.RotateAround(mSolarSystem.transform.position, mSolarSystem.transform.up * axis.y, cameraRotateSpeed);
        }
    }

    bool mouseWasDownLastFrame = false;
    Vector3 mouseMovement = new Vector3();
    private bool MouseMoved()
    {
        mouseMovement.x = Input.GetAxis("Mouse X");
        mouseMovement.y = Input.GetAxis("Mouse Y");
        mouseMovement.z = 0;

        return mouseMovement != Vector3.zero;
    }

    // returns whether the mouse is current above the UI
    public bool MouseAboveUI()
    {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = Input.mousePosition;
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, objectsHit);
        if (objectsHit.Count > 0) // has hit a ui element
        {
            return true;
        }
        return false; // has not hit a ui element
    }

    // deprecated mouse code
    /*
    private void UpdateGlobe()
    {
        // difference defined by mouse axis movement
        Vector3 difPos = mouseMovement;
        difPos = new Vector3(difPos.y, -difPos.x, difPos.z);

        // add current and new rotation together
        Quaternion curRotation = mGlobe.transform.rotation;
        mGlobe.transform.rotation = Quaternion.Euler(Vector3.zero);
        mGlobe.transform.rotation *= Quaternion.Euler(difPos);
        mGlobe.transform.rotation *= curRotation;
    }*/
}
