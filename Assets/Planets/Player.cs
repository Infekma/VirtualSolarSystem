using UnityEngine;
using System.Collections;


/* Class Author: Alex DS  */
class Player : MonoBehaviour
{

    Vector3 mPosition;
    float startingDistance = 1;
    GameObject mEarth;
    Planet mEarthScript;
    void Start()
    {
        mEarth = GameObject.Find("Earth");
        mEarthScript = mEarth.GetComponent<Planet>();
        this.transform.position = mEarthScript.DistanceFromSunWithKmScale() + (-this.transform.forward * startingDistance);
        this.transform.LookAt(mEarth.transform);
    }

    public bool autoPilot = false;
    void Update()
    {
        CheckKeyboard();    
    }

    /* Method Author: Alex DS  */
    // method which checks for keyboard input, is only called if debuglook camera state is active
    public void CheckKeyboard()
    {
        if (Input.GetKey("a") || Input.GetKeyDown("a")) // sideways movement
            RotateLeft();
        if (Input.GetKey("d") || Input.GetKeyDown("d"))
            RotateRight();

        if (Input.GetKey("w") || Input.GetKeyDown("w") || autoPilot) // up/downwards movement
            MoveForwards();
        if (Input.GetKey("s") || Input.GetKeyDown("s"))
            MoveBackwards();

        if (Input.GetKey("q") || Input.GetKeyDown("q")) // up/downwards movement
            RotateUp();
        if (Input.GetKey("e") || Input.GetKeyDown("e"))
            RotateDown();

        if (Input.GetKeyDown(KeyCode.Numlock))
            autoPilot = !autoPilot;
    }

    // movement variables
    private const float MOVEMENT_INCREMENT = 0.5f;
    private float mMoveSpeed = 1000f;
    private float mRotateSpeed = 10f;

    private void RotateLeft()
    {
        Quaternion rotation = Quaternion.Euler(0, -mRotateSpeed, 0);
        this.transform.Rotate(rotation.eulerAngles);
    }

    private void RotateRight()
    {
        Quaternion rotation = Quaternion.Euler(0, mRotateSpeed, 0);
        this.transform.Rotate(rotation.eulerAngles);
    }

    private void RotateUp()
    {
        Quaternion rotation = Quaternion.Euler(-mRotateSpeed, 0, 0);
        this.transform.Rotate(rotation.eulerAngles);
    }

    private void RotateDown()
    {
        Quaternion rotation = Quaternion.Euler(mRotateSpeed, 0, 0);
        this.transform.Rotate(rotation.eulerAngles);
    }

    private void MoveForwards()
    {
        this.transform.position += (this.transform.forward * mMoveSpeed) * Time.deltaTime;
    }
    private void MoveBackwards()
    {
        this.transform.position -= (this.transform.forward * mMoveSpeed) * Time.deltaTime;
    }

}
