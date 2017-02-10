using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpawner : MonoBehaviour {

    public GameObject DataManager;

    public void Spawn(Vector3 position, Quaternion rotation, GameObject curPrefab)
    {
        GameObject curObj = Instantiate(curPrefab, new Vector3(position.x, curPrefab.transform.position.y, position.z), rotation) as GameObject;
        curObj.name = curObj.GetComponent<ObjectIndex>().GetObjectIndex() + "";
        DataManager.GetComponent<ManageData>().AddObject(curObj);
    }
}
