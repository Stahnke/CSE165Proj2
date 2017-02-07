using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    RaycastHit hit;
    GameObject otherObject;
    public GameObject eventText;

    public void Launch()
    {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 100))
        {
            otherObject = hit.collider.gameObject;
            if (otherObject != null)
            {
                
                print("Laser detect object: " + otherObject.name);
                if (otherObject.CompareTag("brick"))
                {
                    Destroy(otherObject);
                    otherObject = null;

                    eventText.GetComponent<EventText>().UpdateText("Destroyed Brick!");
                }
            }
        }
    }
}
