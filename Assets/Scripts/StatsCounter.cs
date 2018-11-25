using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCounter : MonoBehaviour {

    private float clearTime;
    private int shrinkHoleKill;
    private int freezeShockKill;
    private int electricOilKill;

	// Use this for initialization
	void Start () {
        clearTime = 0f;
        shrinkHoleKill = 0;
        freezeShockKill = 0;
        electricOilKill = 0;
    }
	
	// Update is called once per frame
	void Update () {
        clearTime += Time.deltaTime;
	}

    public void incShrinkHoleKill()
    {
        shrinkHoleKill += 1;
    }

    public void incFreezeShockKill()
    {
        freezeShockKill += 1;
    }

    public void incElectricOilKill()
    {
        electricOilKill += 1;
    }

    public string getClearTime() {
        float minutes = Mathf.Floor(clearTime / 60);
        float seconds = clearTime % 60;
        string text = minutes + ":" + Mathf.RoundToInt(seconds);

        return text; 
    }

    public string getShrinkHoleKill()
    {
        return shrinkHoleKill.ToString();
    }

    public string getFreezeShockKill()
    {
        return freezeShockKill.ToString();
    }

    public string getElectricOilKill()
    {
        return electricOilKill.ToString();
    }

    public string getTotalKill()
    {
        int total = shrinkHoleKill + freezeShockKill + electricOilKill;
        return total.ToString();
    }

    public void resetCounter()
    {
        clearTime = 0f;
        shrinkHoleKill = 0;
        freezeShockKill = 0;
        electricOilKill = 0;
    }
}
