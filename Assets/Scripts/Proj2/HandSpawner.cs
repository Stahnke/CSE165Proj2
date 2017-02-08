using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpawner : MonoBehaviour {

    public GameObject DataManager;

    public void Spawn(Vector3 position, GameObject curPrefab)
    {
        GameObject curObj = Instantiate(curPrefab, new Vector3(position.x, curPrefab.transform.position.y - 0.5f, position.z), curPrefab.transform.rotation) as GameObject;
        curObj.name = curObj.GetComponent<ObjectIndex>().GetObjectIndex() + "";
        DataManager.GetComponent<ManageData>().AddObject(curObj);
    }
}
