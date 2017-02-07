using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour {

    public GameObject eventText;

    public void Teleport(Vector3 position)
    {
        //maintain y position
        position.y = gameObject.transform.position.y;

        //update x and z position
        gameObject.transform.position = position;

        eventText.GetComponent<EventText>().UpdateText("Teleported");

        print("Teleport to" + position);
    }
}
