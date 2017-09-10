using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SolarSystem : MonoBehaviour {

    /// <summary>
    ///  vector2(1,1) == vector2(kmScale, kmScale);
    /// </summary>
    [SerializeField] public static float kmScale = 500000;
    [SerializeField] public static float timeMultiplier = 10000;
    [SerializeField] public static float planetScale = 2500;
    [SerializeField] GameObject mGlobe;
    [SerializeField] GameObject mGame;
    public static List<Planet> mPlanets = new List<Planet>();
    // Use this for initialization
    void Start()
    {
        mGame = GameObject.Find("Game");
        mGlobe = GameObject.Find("Solar System");
        mTimeSlider = GameObject.Find("Time Slider").GetComponent<Slider>();
        mKmSlider = GameObject.Find("KM Slider").GetComponent<Slider>();
        mTimeInfoText = GameObject.Find("Time Multiplier Text").GetComponent<Text>();
        mKmInfoText = GameObject.Find("KM Scale Text").GetComponent<Text>();

        mTimeSlider.value = timeMultiplier;
        mKmSlider.value = kmScale;

        ChangeTimeMultiplierInfoText(timeMultiplier.ToString());
        ChangeKmScaleInfoText(kmScale.ToString());
        GameObject.Find("Sun").transform.localScale = new Vector3(1392000 / kmScale, 1392000 / kmScale, 1392000 / kmScale);
    }

    Slider mTimeSlider, mKmSlider;
    Text mTimeInfoText, mKmInfoText;
    public void ChangeTimeMultiplierInfoText(string text)
    {
        mTimeInfoText.text = "1 Second = "+text+"s";
    }
    public void ChangeKmScaleInfoText(string text)
    {
        mKmInfoText.text = "1 Km = " + text + "km";

    }
    public void ChangeKmScale()
    {
        kmScale = mKmSlider.value;
        ChangeKmScaleInfoText(mKmSlider.value.ToString());
    }
    public void ChangeTimeMultiplier()
    {
        timeMultiplier = mTimeSlider.value;
        ChangeTimeMultiplierInfoText(mTimeSlider.value.ToString());
    }

    // returns whether the mouse is current above the UI
   public bool MouseAboveUI()
    {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = Input.mousePosition;
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, objectsHit);
        if(objectsHit.Count > 0) // has hit a ui element
        {
            return true;
        }
        return false; // has not hit a ui element
    }

    // Update is called once per frame
    void Update()
    {
        
       /* if (Input.GetMouseButton(0) && !MouseAboveUI() )
        {
            if (!mouseWasDownLastFrame)
            {
                mouseWasDownLastFrame = true;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if( MouseMoved())
            {
                Debug.Log("mouse down, updating");
                UpdateGlobe();
                UpdateAllPlanets();
            }
        }else{
            mouseWasDownLastFrame = false;
            Cursor.lockState = CursorLockMode.None;
        }*/
        
    }

    private void UpdateAllPlanets()
    {
        foreach(Planet p in mPlanets)
        {
            p.ForceUpdate();
        }
    }

    Vector3 mouseMovement = new Vector3();
    bool mouseWasDownLastFrame = false;
    private bool MouseMoved()
    {
        mouseMovement.x = Input.GetAxis("Mouse X");
        mouseMovement.y = Input.GetAxis("Mouse Y");
        mouseMovement.z = 0;

        return mouseMovement != Vector3.zero;
    }

    private const float rotateSpeed = 1;
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
    }


    private float rotationFrequency;
    private float rotationPerMinute;
    private void RotateGlobe()
    {
        
    }
}