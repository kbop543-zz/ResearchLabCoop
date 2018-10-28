using UnityEngine;
using System.Collections;

public class StationStatus : MonoBehaviour
{

    public bool activated = false;
    public bool waiting = false;
    public float flashDuration = 0.075f;
    public float maxIntensity = 5.0f;
    Light myLight;
    Coroutine flashLight;

    public float duraiton = 4f;

    private void Start()
    {
        myLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!waiting && activated){
            StartCoroutine(waitForTermination());
            waiting = true;

            flashLight = StartCoroutine(flashNow());
            //Debug.Log("Waiting on Termination!!!");
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.transform.root.gameObject;

            GameObject ui = player.transform.Find("ControlUI").gameObject;

            ui.GetComponent<Canvas>().enabled = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.transform.root.gameObject;

            GameObject ui = player.transform.Find("ControlUI").gameObject;

            ui.GetComponent<Canvas>().enabled = false;
        }
    }

    private IEnumerator waitForTermination()
    {
        yield return new WaitForSeconds(duraiton);
        activated = false;
        waiting = false;

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
}
