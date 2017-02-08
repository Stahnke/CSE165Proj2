using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTeleporter : MonoBehaviour {


    public void Teleport(Vector3 position)
    {
        //maintain y position
        position.y = gameObject.transform.position.y;

        //update x and z position
        gameObject.transform.position = position;

        print("Teleport to" + position);
    }
}
