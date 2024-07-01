using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;


public class ClickJacking : MonoBehaviour
{
    private bool remoteObjectpresence = false;
    private bool scriptAdded = false;
    private GameObject firstObject;
    private GameObject secondObject;


    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
          if (remoteObjectpresence == false)
        {
            // check for number of remote objects created
            var remoteObjectList = GameObject.FindGameObjectsWithTag("ObjectFinder");
            Debug.Log("remoteObjectListLength" + remoteObjectList.Length);
            if (remoteObjectList.Length >= 5)
            {
                Debug.Log("Length Condition Met");
                CheckRemoteObjectPresence(remoteObjectList);
            }
        }
        else if(scriptAdded == false)
        {
         
            GameObject firstChild = firstObject.transform.GetChild(0).gameObject;
            GameObject secondChild = secondObject.transform.GetChild(0).gameObject;


            ProxyObjectManipulator proxyObjectManipulator = firstChild.AddComponent<ProxyObjectManipulator>();
            proxyObjectManipulator.targetObject = secondChild;

            ObjectManipulator objectManipulator = firstChild.GetComponentInChildren<ObjectManipulator>();
            if (objectManipulator != null)
            {
                objectManipulator.enabled = false;
                Debug.Log("ObjectManipulator has been disabled.");
            }
            else
            {
                Debug.Log("ObjectManipulator not found on the target object.");
            }

            Debug.Log("First Component Added");

            ProxyObjectManipulator proxyObjectManipulatorSecond = secondChild.AddComponent<ProxyObjectManipulator>();
            proxyObjectManipulatorSecond.targetObject = firstChild;
            
            Debug.Log("Second Component Added");

            ObjectManipulator objectManipulatorSecond = secondChild.GetComponentInChildren<ObjectManipulator>();
            if (objectManipulatorSecond != null)
            {
                objectManipulatorSecond.enabled = false;
                Debug.Log("ObjectManipulatorSecond has been disabled.");
            }
            else
            {
                Debug.Log("ObjectManipulatorSecond not found on the target object.");
            }
            scriptAdded = true;
        }
        
    }


     void CheckRemoteObjectPresence(GameObject[] listOfRemoteObjects)
    {
        
        // randomly pick an index for target game object
        Debug.Log("In CheckRemoteObjectPresence // listOfRemoteObjects.Length: " + listOfRemoteObjects.Length);
        int targetIndex = Random.Range(2, listOfRemoteObjects.Length);
        Debug.Log("In CheckRemoteObjectPresence // targetIndex: " + targetIndex);

        // GameObject localRemoteObject = GameObject.Find("RemoteObject(Clone)/BaseMesh");
        GameObject localFirstObject = listOfRemoteObjects[listOfRemoteObjects.Length - targetIndex];
        GameObject localSecondObject = listOfRemoteObjects[listOfRemoteObjects.Length - targetIndex - 1];

        if (localFirstObject && localSecondObject && remoteObjectpresence == false) 
        {
            remoteObjectpresence = true;
            firstObject = localFirstObject;
            secondObject = localSecondObject;
        }
    }
}
