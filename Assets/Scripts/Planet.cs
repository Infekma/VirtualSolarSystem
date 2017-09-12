using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour {

    [SerializeField] float dayLength, orbitLength,diameter, distanceFromSun;
    // tilt and direction of spin
    [SerializeField] GameObject planet;
    [SerializeField] GameObject sun;
    LineRenderer mRenderer;

    [SerializeField] bool eastSpinDirection, eastOrbitDirection;
    [SerializeField] float rotationAxis;
    [SerializeField] Vector3 rotation;
    [SerializeField] float orbitDaysLeft;
    [SerializeField] string planetName;
    GameObject camera;
    Text planetNameUI;
    // Use this for initialization
    void Start () {
        ForceUpdate();
        SolarSystem.mPlanets.Add(this.GetComponent<Planet>());

        planet.transform.Rotate(Quaternion.AngleAxis(rotationAxis, rotation).eulerAngles);
        planet.transform.position = (sun.transform.position + (new Vector3(0, 0, distanceFromSun)) / SolarSystem.kmScale);

        SetPlanetSize();
        SetStartingPosition();

        planetNameUI = GameObject.Find(planetName + " Names").GetComponent<Text>();
        camera = GameObject.Find("Main Camera");
    }

    void SetStartingPosition()
    {
        float rotation = ((360) / orbitLength) * orbitDaysLeft;
        planet.transform.RotateAround(sun.transform.position, Vector3.up, rotation);
    }

    public void SetupPlanet()
    {
        float scaleDif = SolarSystem.kmScale - initialisedScale;
        initialisedScale = SolarSystem.kmScale;

        planet.transform.position = (sun.transform.position + (new Vector3(0, 0, distanceFromSun)) / SolarSystem.kmScale);
        SetPlanetSize();
        SetStartingPosition();

        mRenderer = GetComponent<LineRenderer>();
        if (mRenderer != null)
        {
            CreateOrbitPath(mRenderer);
        }
    }

    public Vector3 GetCurrentPlanetSize()
    {
        return new Vector3(diameter / 2, diameter / 2, diameter / 2);
    }

    public Vector3 GetPlanetSizeRelativeToDistanceScale()
    {
        return new Vector3(diameter / 2 / SolarSystem.kmScale, diameter / 2 / SolarSystem.kmScale, diameter / 2 /SolarSystem.kmScale);
    }

    public void SetPlanetSize()
    {
        float planScale = GetPlanetSizeRelativeToDistanceScale().z * SolarSystem.planetScale;
        planet.transform.localScale = new Vector3(planScale, planScale, planScale);
    }

    [SerializeField] int orbitPathSmoothness = 10; // smoothness factor higher value equals smoother circle
    private void CreateOrbitPath(LineRenderer renderer)
    {
        if (renderer != null)
        {
            int totalSteps = (int)dayLength * orbitPathSmoothness; // total steps
            renderer.numPositions = totalSteps + 1; // total steps
            float lineScale = 3 + (GetPlanetSizeRelativeToDistanceScale().z / SolarSystem.planetScale);
            //float lineScale = 0.1f;
            renderer.startWidth = lineScale;
            renderer.endWidth =lineScale;

            float angle_step = 360 / totalSteps + 1; // angle per step
            float curAngle = 0; // current angle
            int count = 0; // current count
            Vector3 pointA = this.transform.position;
            while (count <= totalSteps)
            {
                // calculate position rotated around a point
                Vector3 curPos = sun.transform.position;
                Quaternion rotation = Quaternion.AngleAxis(curAngle, Vector3.up); // around which axis to rotate by how much angle
                Vector3 vector2 = curPos - this.transform.position;
                vector2 = rotation * vector2;
                vector2 = curPos + vector2;
                curPos = vector2;

                renderer.SetPosition(count, curPos); // set vertex position

                count += 1;
                curAngle += angle_step;
            }
        }
    }

    public Vector3 CameraDistanceFromSun()
    {
        return GameObject.Find("Main Camera").transform.position;
    }
    public Vector3 DistanceFromSunWithKmScale()
    {
        return new Vector3(0, 0, distanceFromSun / SolarSystem.kmScale);
    }

    float initialisedScale = 0;
	// Update is called once per frame
	void Update () {
        UpdateOrbit();
        UpdateOrientation();

        if(SolarSystem.kmScale != initialisedScale)
        {
            SetupPlanet();
        }

        planetNameUI.transform.position = this.transform.position + new Vector3(0, GetPlanetSizeRelativeToDistanceScale().y/2 * SolarSystem.planetScale, 0);
        planetNameUI.transform.LookAt(-camera.transform.position);
	}

    public void ForceUpdate()
    {
        CreateOrbitPath(mRenderer);
    }

    private void UpdateOrbit()
    {
        float rotation = ((360) / DaysToSeconds(orbitLength)) * SolarSystem.timeMultiplier * Time.deltaTime;
        if (eastOrbitDirection)
            rotation = -rotation;
        planet.transform.RotateAround(sun.transform.position, Vector3.up, rotation);
    }

    private void UpdateOrientation()
    {
        float yRotation = ((360) / DaysToSeconds(dayLength)) * SolarSystem.timeMultiplier * Time.deltaTime;
        if (eastSpinDirection)
            yRotation = -yRotation;
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        this.transform.Rotate(rotation.eulerAngles);
    }

    /// <summary>
    /// Returns how many seconds there are in how many given days
    /// </summary>
    /// <param name="days">amount of hours</param>
    /// <returns></returns>
    private float DaysToSeconds(float days)
    {
        return days * 24 * 60 * 60;
    }
}
/*
public struct Planet
{
    private string sName;
    private float sDistanceFromSun;
    private float sDiameter;
    private float sDaylength;
    private float sOrbitLenth;

    public Planet(string name, float sunDistance, float diameter, float dayLength, float orbitLength)
    {
        sName = name;
        sDistanceFromSun = sunDistance;
        sDiameter = diameter;
        sDaylength = dayLength;
        sOrbitLenth = orbitLength;
    }

    public string GetName()
    {
        return sName;
    }
    public float GetDistanceToSun()
    {
        return sDistanceFromSun;
    }
    public float GetDiameter()
    {
        return sDiameter;
    }
    public float GetDayLength()
    {
        return sDaylength;
    }
    public float GetOrbitLength()
    {
        return sOrbitLenth;
    }
}
*/