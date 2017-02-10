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

    private int prefabIndex = 0;

    private bool selected = false;
    private GameObject selectedObject;

    private bool groupingMode = false;

    private GameObject groupObject;

    private bool switchedOn = false;
    private bool switchedOff = true;

    ArrayList otherObjects = new ArrayList();

    private void Start()
    {
        PrefabButtons.SetActive(false);
    }

    public bool Launch() {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (selected == true)
        {
            otherObjects.Clear();
            selected = false;
            Rigidbody[] rigidbodies = selectedObject.GetComponentsInChildren<Rigidbody>();
            Collider[] colliders = selectedObject.GetComponentsInChildren<Collider>();
            Light[] lights = selectedObject.GetComponentsInChildren<Light>();
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = false;
                //rigidbodies[i].GetComponent<MaintainRotation>().SendSelected(false);
            }

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = true;
            }

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = false;
            }

            selectedObject.transform.DetachChildren();
            Destroy(selectedObject);
            selectedObject = null;
        }

        else if (Physics.Raycast(transform.position, fwd, out hit, 100))
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

                else if (otherObject.CompareTag("prefabButton"))
                {
                    int buttonIndex = otherObject.gameObject.GetComponent<HandButton>().GetButtonIndex();
                    otherObject.GetComponent<Animator>().SetTrigger("Select");
                    ChangePrefab(buttonIndex);
                }

                else if (otherObject.CompareTag("saveloadButton"))
                {
                    int buttonIndex = otherObject.gameObject.GetComponent<HandButton>().GetButtonIndex();
                    otherObject.GetComponent<Animator>().SetTrigger("Select");
                    SaveLoad(buttonIndex);
                }

                else if (otherObject.CompareTag("teleportPlane") && mode == TELEPORT_MODE)
                {
                    GetComponentInParent<HandTeleporter>().Teleport(hit.point);
                    return true;
                }

                else if (otherObject.CompareTag("teleportPlane") && mode == SPAWN_MODE && prefabIndex != 5)
                {
                    GetComponentInParent<HandSpawner>().Spawn(hit.point, curPrefab.transform.rotation, curPrefab);
                    return true;
                }

                else if (otherObject.CompareTag("wallPlane") && mode == SPAWN_MODE && prefabIndex == 5)
                {
                    GetComponentInParent<HandSpawner>().Spawn(hit.point, otherObject.transform.rotation, curPrefab);
                    return true;
                }

                else if (otherObject.CompareTag("labObject") && mode == SELECT_MODE && groupingMode == false)
                {
                    selected = true;
                    print("Selected: " + otherObject.name);
                    GameObject emptyObject = Instantiate(emptyLocation, hit.point, this.transform.parent.rotation) as GameObject;
                    emptyObject.transform.SetParent(this.transform.parent);
                    otherObject.transform.SetParent(emptyObject.transform);
                    otherObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                    otherObject.transform.GetComponent<Collider>().enabled = false;
                    if (otherObject.GetComponent<ObjectIndex>().GetObjectIndex() != 5)
                        emptyObject.GetComponent<MaintainWorldRotation>().SendSelected(true);
                    else emptyObject.GetComponent<MaintainWorldRotation>().yLock = true;
                    selectedObject = emptyObject;
                    return true;
                }

                else if (otherObject.CompareTag("labObject") && mode == SELECT_MODE && groupingMode == true)
                {
                    print("Grouped: " + otherObject.name);

                    if (otherObject.GetComponent<ObjectIndex>().GetObjectIndex() != 5)
                    {
                        otherObject.GetComponent<Light>().enabled = true;
                        otherObjects.Add(otherObject);
                        return true;
                    }
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
        prefabIndex = index;
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

    public void GroupingOnOff(bool groupingMode)
    {
        this.groupingMode = groupingMode;
        if(groupingMode == true && switchedOff == true)
        {
            switchedOn = true;
            switchedOff = false;
            groupObject = Instantiate(emptyLocation, hit.point, this.transform.parent.rotation) as GameObject;
            groupObject.transform.SetParent(this.transform.parent);
            selectedObject = groupObject;
        }
        else if(groupingMode == false && switchedOn == true)
        {
            selected = true;
            switchedOn = false;
            switchedOff = true;

            groupObject.GetComponent<MaintainWorldRotation>().SendSelected(true);

            for (int i = 0; i < otherObjects.Count; i++)
            {
                ((GameObject)otherObjects[i]).transform.SetParent(groupObject.transform);
            }

            Rigidbody[] rigidbodies = selectedObject.GetComponentsInChildren<Rigidbody>();
            Collider[] colliders = selectedObject.GetComponentsInChildren<Collider>();
            
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = true;
                //rigidbodies[i].GetComponent<MaintainRotation>().SendSelected(true);
            }

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }
    }
}
