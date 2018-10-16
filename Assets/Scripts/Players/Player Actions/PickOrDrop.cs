using UnityEngine;

public class PickOrDrop : MonoBehaviour {

    public GameObject gun;
    public GameObject shockWave;
    public GameObject collector;
    public bool emptyHand = true;
    public float pickUpRadius = 20f;
    public bool hasGun = false;
    public bool hasCollector = false;
    private string curItem;

    void FixedUpdate()
    {
        if (((Input.GetKeyUp("v") || Input.GetKeyUp(KeyCode.Joystick1Button0)) && ((gameObject.name == "P1") || gameObject.name == "P1(Clone)")) ||
            ((Input.GetKeyUp("n") || Input.GetKeyUp(KeyCode.Joystick2Button0)) && ((gameObject.name == "P2") || (gameObject.name == "P2(Clone)"))))
        {
            if (emptyHand)
            {
                Pickup();
            }
            else
            {
                Dropdown();
            }
        }

        if (!emptyHand)
        {
            //Relocate item location around character
        }

    }

    private void Pickup()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, pickUpRadius);
        bool found = false;
        int size = items.Length;

        if (size != 0) {
            int i = 0;

            //Debug code
            //if (GameObject.Find("ShockwaveGun(Clone)")) {
            //    float shockDist = Vector3.Distance(GameObject.Find("ShockwaveGun(Clone)").transform.position,
            //                                       transform.position);
            //    Debug.Log("Distance with ShockwaveGun(Clone): " + shockDist);
            //} else {
            //    Debug.Log("ShockwaveGun(Clone) not present: ERROR");
            //}

            while ((i < size) && !found)
            {
                //If we have weapon
                if (items[i].gameObject.tag == "Weapon")
                {
                    //Set parameters
                    hasGun = true;
                    //item = items[i].gameObject;
                    //curItem = items[i].gameObject.name;
                    Destroy(items[i].gameObject);
                    found = true;
                    emptyHand = false;
                    curItem = items[i].gameObject.name;
                    //Debug.Log(curItem);
                    GetComponent<shoot>().GunStatsUpdate(curItem);
                }
                //Elsif we have tool
                else if (items[i].gameObject.tag == "Tool")
                {
                    //Set parameters
                    hasCollector = true;
                    //item = items[i].gameObject;
                    //curItem = items[i].gameObject.name;
                    Destroy(items[i].gameObject);
                    found = true;
                    emptyHand = false;
                    curItem = items[i].gameObject.name;
                }

                i++;
            }

        }

        //Else
        return;
    }

    public void Dropdown()
    {
        Vector3 pos = transform.position;
        GameObject item;
        //GameObject item = Resources.Load(curItem) as GameObject;

        if (hasGun)
        {
            pos.y = 0.5f;
            hasGun = false;
            emptyHand = true;

            switch (curItem)
            {
                case "CrossBow(Clone)":
                    item = gun;
                    break;
                case "ShockwaveGun(Clone)":
                    item = shockWave;
                    break;
                default:
                    Debug.Log("Unregistered weapon");
                    item = shockWave;
                    break;
            }

            GameObject b = Instantiate(item);
            b.transform.position = pos;
            Debug.Log("Dropped " + b.name + " at: " + b.transform.position);
        }
        else if(hasCollector)
        {
            pos.y = 2.6f;
            hasCollector = false;
            emptyHand = true;

            switch (curItem)
            {
                case "Collector(Clone)":
                    item = collector;
                    break;
                default:
                    Debug.Log("Unregistered weapon");
                    item = shockWave;
                    break;
            }

            var b = (GameObject)Instantiate(item,
                                            pos,
                                            Quaternion.identity);
        }
    }
}
