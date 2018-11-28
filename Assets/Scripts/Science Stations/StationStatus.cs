using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StationStatus : MonoBehaviour
{

    public bool activated = false;
    public bool waiting = false;
    public bool prepared = true;
    public float flashDuration = 0.075f;
    public float maxIntensity = 5.0f;
    Light myLight;
    Coroutine flashLight;
    GameObject gm;
    public ParticleSystem ParticleEffect;
    public ParticleSystem StationEffect;

    public float duraiton = 4f;
    public float coolDown = 3f;
    private bool used;
    private float curCoolDown;

    public bool recharging = false;
    public GameObject battery;
    public Image batteryFill;
    public float coolDownStartTime;
    public float coolDownTimePassed; // timePassed = Time.time - startTime

    public GameObject arrow;
    public GameObject outOfOrderSign;
    public GameObject model;
    private Material originalMaterial;
    private bool colorChanged;
    public Material disabledMaterial;

    private void Start()
    {
        myLight = GetComponentInChildren<Light>();
        ParticleEffect = GetComponentInChildren<ParticleSystem>();

        //Get originalMaterialColor
        colorChanged = true;
        GetMaterialColor();

        gm = GameObject.FindGameObjectWithTag("GameManager");
        if (gm.GetComponent<GameConstants>().completeLvl1 == false &&
            (gameObject.name.Contains("ElectricityStation") ||
             gameObject.name.Contains("FreezeStation"))) {

            prepared = false;
            colorChanged = false;

            //Darken the station
            Darken(0.9f);
        }

        // Set Arrow angle
        used = false;
        Vector3 pos = new Vector3(0f, 300f, -350f);
        arrow.transform.GetChild(0).LookAt(pos);

        // Set curCoolDown;
        curCoolDown = coolDown;

        // Set fill amount of battery fill to 1
        batteryFill.fillAmount = 1.0f;
        // Set both batteryIcon and batteryFill to inactive at start of game
        //battery.SetActive(false);
    }

    private void Update()
    {
        if (recharging)
        {
            coolDownTimePassed = Time.time - coolDownStartTime;
            batteryFill.fillAmount = coolDownTimePassed / coolDown;

            if (batteryFill.fillAmount >= 1.0f)
            {
                //battery.SetActive(false);
                recharging = false;
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Restore color
        if (prepared && !colorChanged)
        {
            RestoreColor();
            colorChanged = true;
        }

        if (!activated && prepared)
        {
            if (curCoolDown < coolDown) {
                curCoolDown += Time.deltaTime;
            }
        }

        if (!waiting && activated)
        {
            StartCoroutine(waitForTermination());
            waiting = true;

            flashLight = StartCoroutine(flashNow());
            //Debug.Log("Waiting on Termination!!!");

            used = true;
        }

        if (!prepared || used)
        {
            disableArrow();

        }
        else {
            enableArrow();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!prepared)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            // Enable player control UI
            GameObject player = other.transform.root.gameObject;

            GameObject ui = player.transform.Find("ControlUI").gameObject;

            ui.GetComponent<Canvas>().enabled = true;

            // Enable station UI
            GameObject stationUI = gameObject.transform.Find("StationCanvas").gameObject;

            stationUI.GetComponent<Canvas>().enabled = true;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (!prepared) {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            // Disable player control UI
            GameObject player = other.transform.root.gameObject;

            GameObject ui = player.transform.Find("ControlUI").gameObject;

            ui.GetComponent<Canvas>().enabled = false;

            // Disable station UI
            GameObject stationUI = gameObject.transform.Find("StationCanvas").gameObject;

            stationUI.GetComponent<Canvas>().enabled = false;
        }
    }

    private IEnumerator waitForTermination()
    {
        yield return new WaitForSeconds(duraiton);
        activated = false;
        waiting = false;
        ParticleEffect.Stop();

        // Display and start cooldown UI of station
        //battery.SetActive(true);
        coolDownStartTime = Time.time;
        coolDownTimePassed = Time.time - coolDownStartTime;
        batteryFill.fillAmount = coolDownTimePassed / coolDown;
        recharging = true;

        StopCoroutine(flashLight);
        myLight.intensity = maxIntensity;
    }

    private IEnumerator flashNow()
    {
        float waitTime = flashDuration  / (2 * maxIntensity);

        while(true){
            while (myLight.intensity < maxIntensity)
            {
                myLight.intensity += Time.deltaTime / waitTime;        // Increase intensity
                yield return null;
            }
            while (myLight.intensity > 0)
            {
                myLight.intensity -= Time.deltaTime / waitTime;        //Decrease intensity
                yield return null;
            }
        }
    }

    public void disableArrow()
    {
        var parts = arrow.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer part in parts)
        {
            part.enabled = false;
        }
    }

    public void enableArrow()
    {
        var parts = arrow.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer part in parts)
        {
            part.enabled = true;
        }
    }

    public void disableSign()
    {
        var parts = outOfOrderSign.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer part in parts)
        {
            part.enabled = false;
        }
    }

    public void enableSign()
    {
        print("I am enabling");
        var parts = outOfOrderSign.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer part in parts)
        {
            part.enabled = true;
        }
        print("Done enabling");

    }

    public bool isStationOnCD()
    {
        if (curCoolDown >= coolDown) {
            curCoolDown = 0f;
            return false;
        }
        else {
            return true;
        }
    }

    private void GetMaterialColor()
    {
        MeshRenderer[] allMesh = model.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in allMesh)
        {
            originalMaterial = mesh.material;
        }
    }

    public void Darken(float percent)
    {
        percent = Mathf.Clamp01(percent);
        MeshRenderer[] allMesh = model.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in allMesh)
        {
            //mesh.material.color = new Color(originalColor.r * (1 - percent), originalColor.g * (1 - percent), originalColor.b * (1 - percent), originalColor.a);
            mesh.material = disabledMaterial;
        }

        enableSign();

        if (StationEffect != null) {
            StationEffect.Stop();
        }
    }

    public void RestoreColor()
    {
        MeshRenderer[] allMesh = model.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in allMesh)
        {
            mesh.material = originalMaterial;
        }

        disableSign();

        if (StationEffect != null) {
            StationEffect.Play();
        }
    }
}
