using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript_Spatial : MonoBehaviour
{
    [SerializeField]
    private GameObject OcclusionCube;
    private bool remoteObjectpresence = false;
    private GameObject RemoteObject;
    private Vector3 remoteObjectPosition;
    private Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In Start");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("In Update");

        if (remoteObjectpresence == false)
        {
            // check for number of remote objects created
            var remoteObjectList = GameObject.FindGameObjectsWithTag("ObjectFinder");

            if (remoteObjectList.Length >= 4)
            {
                CheckRemoteObjectPresence(remoteObjectList);
            }
        }

    }

    Bounds GetBoundsWithPadding(GameObject obj, float padding)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(); // Get all renderers of the object
        Bounds bounds = renderers[0].bounds; // Initialize bounds with the first renderer bounds

        // Iterate through all renderers to encapsulate their bounds
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        bounds.Expand(padding);

        return bounds;
    }

    void CheckRemoteObjectPresence(GameObject[] listOfRemoteObjects)
    {
        Debug.Log("In CheckRemoteObjectPresence");

        // randomly pick an index for target game object
        Debug.Log("In CheckRemoteObjectPresence // listOfRemoteObjects.Length: " + listOfRemoteObjects.Length);
        int targetIndex = Random.Range(0, listOfRemoteObjects.Length-1);
        Debug.Log("In CheckRemoteObjectPresence // targetIndex: " + targetIndex);

        // GameObject localRemoteObject = GameObject.Find("RemoteObject(Clone)/BaseMesh");
        GameObject localRemoteObject = listOfRemoteObjects[targetIndex];

        if (localRemoteObject && remoteObjectpresence == false) 
        {
            remoteObjectpresence = true;
            RemoteObject = localRemoteObject;
             Debug.Log("In Update // about to update OcclusionCube position");

            // GameObject parentOfRemoteObject = RemoteObject.transform.parent.gameObject;
            bounds = GetBoundsWithPadding(RemoteObject, 1.0f); 
            // Set cube position to center of bounds
            OcclusionCube.transform.position = bounds.center;      
            OcclusionCube.transform.localScale = bounds.size; // / scaleFactor;
        }
    }
}

