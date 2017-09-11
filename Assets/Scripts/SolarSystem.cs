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
    Scrollbar mDateSlider;
    // Use this for initialization
    void Start()
    {
        mGame = GameObject.Find("Game");
        mGlobe = GameObject.Find("Solar System");
        mTimeSlider = GameObject.Find("Time Slider").GetComponent<Slider>();
        mKmSlider = GameObject.Find("KM Slider").GetComponent<Slider>();
        mTimeInfoText = GameObject.Find("Time Multiplier Text").GetComponent<Text>();
        mKmInfoText = GameObject.Find("KM Scale Text").GetComponent<Text>();

        mDateSlider = GameObject.Find("Date Slider").GetComponent<Scrollbar>();

        mTimeSlider.value = timeMultiplier;
        mKmSlider.value = kmScale;

        ChangeTimeMultiplierInfoText(timeMultiplier.ToString());
        ChangeKmScaleInfoText(kmScale.ToString());
        GameObject.Find("Sun").transform.localScale = new Vector3(1392000 /2/ kmScale*planetScale, 1392000 /2/ kmScale * planetScale, 1392000 /2/ kmScale * planetScale);
    }

    #region UI Sliders functions (including km and time scale change)
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
    #endregion

    // Update is called once per frame
    void Update()
    {

    }

    float secondsInaDay = 60 * 60 * 24;
    public void ChangeSolarSystemDate()
    {
        float dateInc = mDateSlider.value;
        float midPoint = 0.5f;
        float diff = dateInc - midPoint;

        if(diff > 0){
            timeMultiplier = secondsInaDay * (diff * 32);
        }else if(diff < 0){
            timeMultiplier = secondsInaDay * (diff * 32);
        }else{
            timeMultiplier = 1;
        }

        ChangeTimeMultiplierInfoText(timeMultiplier.ToString());
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
}