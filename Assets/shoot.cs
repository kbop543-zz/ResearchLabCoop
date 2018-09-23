using UnityEngine;

public class shoot : MonoBehaviour {     public GameObject bullet;     public float range = 100f;     public float startDistance = 15f;     public float duration = 1.5f;     public float shootRate = 5f;
    public float curCooldown = 0f;      void FixedUpdate()     {         if (Input.GetKey("b"))         {             Shoot();         }     }      public void Shoot() {
        Vector3 pos = transform.position;
        var b = (GameObject)Instantiate(bullet,
                                        pos + transform.forward * startDistance,
                                        Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = transform.forward * range;
         Destroy(b, duration);          return;     }  }