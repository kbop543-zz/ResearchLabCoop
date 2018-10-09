using UnityEngine;

public class PickOrDrop : MonoBehaviour {

    public GameObject gun;
    public GameObject collector;
    public bool emptyHand = true;
    public float pickUpRadius = 13f;
    public bool hasGun = false;
    public bool hasCollector = false;
    //private string curItem = "";

    void FixedUpdate()
    {
        if (((Input.GetKeyUp("v") || Input.GetKeyUp(KeyCode.Joystick1Button1)) && ((gameObject.name == "P1") || gameObject.name == "P1(Clone)")) ||
            ((Input.GetKeyUp("n") || Input.GetKeyUp(KeyCode.Joystick2Button1)) && ((gameObject.name == "P2") || (gameObject.name == "P2(Clone)"))))
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
                }

                i++;
            }

        }

        //Else
        return;
    }

    private void Dropdown()
    {
        Vector3 pos = transform.position;
        //GameObject item = Resources.Load(curItem) as GameObject;

        if (hasGun)
        {
            pos.y = 0.5f;
            hasGun = false;
            emptyHand = true;

            var b = (GameObject)Instantiate(gun,
                                            pos,
                                            Quaternion.identity);
        }
        else if(hasCollector)
        {
            pos.y = 2.6f;
            hasCollector = false;
            emptyHand = true;

            var b = (GameObject)Instantiate(collector,
                                            pos,
                                            Quaternion.identity);
        }
    }
}
