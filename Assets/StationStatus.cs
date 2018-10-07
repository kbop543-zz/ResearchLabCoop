using UnityEngine;
using System.Collections;

public class StationStatus : MonoBehaviour
{

    public bool activated = false;
    public bool waiting = false;

    public float duraiton = 4f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!waiting && activated){
            StartCoroutine(waitForTermination());
            waiting = true;

            Debug.Log("Waiting on Termination!!!");
        }

    }

    IEnumerator waitForTermination()
    {
        yield return new WaitForSeconds(duraiton);
        activated = false;
        waiting = false;
    }
}
