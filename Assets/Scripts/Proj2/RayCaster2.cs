using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster2 : MonoBehaviour {

    RaycastHit hit;
    GameObject otherObject;

    private int mode;
    public GameObject SpawnButton;
    public GameObject PrefabButtons;
    private GameObject curPrefab;
    public GameObject[] prefabs;
    public GameObject DataManager;
    public GameObject emptyLocation;

    private const int TELEPORT_MODE = 0;
    private const int SELECT_MODE = 1;
    private const int SPAWN_MODE = 2;

    private const int SAVE_TYPE = 0;
    private const int LOAD_TYPE = 1;

    private void Start()
    {
        PrefabButtons.SetActive(false);
    }

    public bool Launch() {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 100))
        {
            otherObject = hit.collider.gameObject;
            if (otherObject != null)
            {
                print("Raycast detect object: " + otherObject.name);

                if(otherObject.CompareTag("handButton"))
                {
                    int buttonIndex = otherObject.gameObject.GetComponent<HandButton>().GetButtonIndex();
                    otherObject.GetComponent<Animator>().SetTrigger("Select");
                    ChangeMode(buttonIndex);
                }

                if (otherObject.CompareTag("prefabButton"))
                {
                    int buttonIndex = otherObject.gameObject.GetComponent<HandButton>().GetButtonIndex();
                    otherObject.GetComponent<Animator>().SetTrigger("Select");
                    ChangePrefab(buttonIndex);
                }

                if (otherObject.CompareTag("saveloadButton"))
                {
                    int buttonIndex = otherObject.gameObject.GetComponent<HandButton>().GetButtonIndex();
                    otherObject.GetComponent<Animator>().SetTrigger("Select");
                    SaveLoad(buttonIndex);
                }

                if (otherObject.CompareTag("teleportPlane") && mode == TELEPORT_MODE)
                {
                    GetComponentInParent<HandTeleporter>().Teleport(hit.point);
                    return true;
                }

                else if (otherObject.CompareTag("teleportPlane") && mode == SPAWN_MODE)
                {
                    GetComponentInParent<HandSpawner>().Spawn(hit.point, curPrefab);
                    return true;
                }

                else if (otherObject.CompareTag("labObject") && mode == SELECT_MODE)
                {
                    print("Selected: " + otherObject.name);
                    GameObject emptyObject = Instantiate(emptyLocation, hit.point, this.transform.parent.rotation) as GameObject;
                    emptyObject.transform.SetParent(this.transform.parent);
                    otherObject.transform.SetParent(emptyObject.transform);
                    otherObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                    return true;
                }
            }
        }

        return false;
    }

    void ChangeMode(int mode)
    {
        this.mode = mode;
        print("mode = " + mode);

        if (mode == SPAWN_MODE)
        {
            SpawnButton.SetActive(false);
            PrefabButtons.SetActive(true);
        }

        else
        {
            SpawnButton.SetActive(true);
            PrefabButtons.SetActive(false);
        }
    }

    void ChangePrefab(int index)
    {
        curPrefab = prefabs[index];
        print("curPrefab = " + curPrefab.name);
    }

    void SaveLoad(int index)
    {
        if (index == SAVE_TYPE)
        {
            DataManager.GetComponent<ManageData>().SaveData("myTest.txt");
        }
        else if (index == LOAD_TYPE)
        {
            DataManager.GetComponent<ManageData>().LoadData("myTest.txt");
        }
    }
}
