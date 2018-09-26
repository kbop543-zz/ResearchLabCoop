using UnityEngine;

public class PickOrDrop : MonoBehaviour {

    public GameObject cam;
    public GameObject item;
    public bool emptyHand = true;
    public float pickUpRadius = 13f;
    public bool hasGun = false;
    public bool hasCollector = false;

    void FixedUpdate()
    {
        if ((((Input.GetKey("v") || Input.GetKey("17")) ||Input.GetKey("joystick button 17")) && ((gameObject.name == "P1") || gameObject.name == "P1(Clone)")) ||
            (Input.GetKey("n") && ((gameObject.name == "P2") || (gameObject.name == "P2(Clone)")))) 
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

    public void Pickup()
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
                    Destroy(items[i].gameObject);
                    found = true;
                    emptyHand = false;
                }
                //Elsif we have tool
                else if (items[i].gameObject.tag == "Tool")
                {
                    //Set parameters
                    hasCollector = true;
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

    public void Dropdown()
    {
        return;
    }
}
