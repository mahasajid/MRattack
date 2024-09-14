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
        if (remoteObjectpresence == true && (firstObject == null || secondObject == null))
        {
            
              if (firstObject != null)
            {
                GameObject firstChild = firstObject.transform.GetChild(0).gameObject;
                ProxyObjectManipulator proxyObjectManipulator = firstChild.GetComponentInChildren<ProxyObjectManipulator>();
                ObjectManipulator objectManipulator = firstChild.GetComponentInChildren<ObjectManipulator>();
          
                objectManipulator.enabled = true;
                proxyObjectManipulator.enabled = false;
                Debug.Log("ObjectManipulator has been enabled., Proxy disabled");
            }
            else
            {
                Debug.Log("First Attacked Object not found on the target object.");
            }

            
            if (secondObject != null)
            {
                GameObject secondChild = secondObject.transform.GetChild(0).gameObject;
                ProxyObjectManipulator proxyObjectManipulatorSecond = secondChild.GetComponentInChildren<ProxyObjectManipulator>();
                ObjectManipulator objectManipulatorSecond = secondChild.GetComponentInChildren<ObjectManipulator>();
           
                objectManipulatorSecond.enabled = true;
                proxyObjectManipulatorSecond.enabled = false;
                Debug.Log("ObjectManipulatorSecond has been enabled, Proxy Disabled.");
            
            }
           
            else
            {
                Debug.Log("Second Attacked not found on the target object.");
            }
            scriptAdded = false;
            remoteObjectpresence = false;
        }

          if (remoteObjectpresence == false)
        {
            // check for number of remote objects created
            var remoteObjectList = GameObject.FindGameObjectsWithTag("ObjectFinder");
            Debug.Log("remoteObjectListLength" + remoteObjectList.Length);
            if (remoteObjectList.Length >= 6)
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
       // int targetIndex = Random.Range(2, listOfRemoteObjects.Length);
        Debug.Log("In CheckRemoteObjectPresence // targetIndex: " );

        // GameObject localRemoteObject = GameObject.Find("RemoteObject(Clone)/BaseMesh");
        GameObject localFirstObject = listOfRemoteObjects[listOfRemoteObjects.Length - 1];
        GameObject localSecondObject = listOfRemoteObjects[listOfRemoteObjects.Length  - 4];

        if (localFirstObject && localSecondObject && remoteObjectpresence == false) 
        {
            remoteObjectpresence = true;
            firstObject = localFirstObject;
            secondObject = localSecondObject;
        }
    }
}



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Microsoft.MixedReality.Toolkit.UI;

// public class ClickJacking : MonoBehaviour
// {
//     private bool remoteObjectPresence = false;
//     private bool scriptAdded = false;
//     private GameObject firstObject;
//     private GameObject secondObject;

//     void Update()
//     {
//         if (remoteObjectPresence)
//         {
//             HandleObjectManipulators();
//         }
//         else
//         {
//             CheckForRemoteObjects();
//         }
//     }

//     void HandleObjectManipulators()
//     {
//         if (firstObject != null && secondObject != null)
//         {
//             UpdateManipulator(firstObject, false);
//             UpdateManipulator(secondObject, false);

//             if (!scriptAdded)
//             {
//                 AddProxyManipulator(firstObject, secondObject);
//                 AddProxyManipulator(secondObject, firstObject);

//                 scriptAdded = true;
//             }
//         }

//         else 
//         {
//             UpdateManipulator(firstObject, true);
//             UpdateManipulator(secondObject, true);

//             RemoveProxyManipulator(firstObject, false);
//             RemoveProxyManipulator(secondObject, false);

//             remoteObjectPresence = false;
//             scriptAdded = false;


//         }
//     }

//     void CheckForRemoteObjects()
//     {
//         var remoteObjectList = GameObject.FindGameObjectsWithTag("ObjectFinder");
//         Debug.Log("remoteObjectListLength: " + remoteObjectList.Length);
        
//         if (remoteObjectList.Length >= 6)
//         {
//             Debug.Log("Length Condition Met");
//             AssignRemoteObjects(remoteObjectList);
//         }
//     }

//     void AssignRemoteObjects(GameObject[] listOfRemoteObjects)
//     {
//         GameObject localFirstObject = listOfRemoteObjects[listOfRemoteObjects.Length - 1];// different for each client (2,3 and 4,5)
//         GameObject localSecondObject = listOfRemoteObjects[listOfRemoteObjects.Length - 4];

//         if (localFirstObject && localSecondObject)
//         {
//             remoteObjectPresence = true;
//             firstObject = localFirstObject;
//             secondObject = localSecondObject;
//         }
//     }

//     void UpdateManipulator(GameObject targetObject, bool enable)
//     {
//         var objectManipulator = targetObject.GetComponentInChildren<ObjectManipulator>();
//         if (objectManipulator != null)
//         {
//             objectManipulator.enabled = enable;
//             Debug.Log($"{(enable ? "Enabled" : "Disabled")} ObjectManipulator on {targetObject.name}");
//         }
//         else
//         {
//             Debug.Log($"ObjectManipulator not found on {targetObject.name}");
//         }
//     }

//     void AddProxyManipulator(GameObject targetObject, GameObject otherObject)
//     {
//         var proxyManipulator = targetObject.AddComponent<ProxyObjectManipulator>();
//         proxyManipulator.targetObject = otherObject;

//         UpdateManipulator(targetObject, false);
//         Debug.Log($"Added ProxyObjectManipulator to {targetObject.name} targeting {otherObject.name}");
//     }


//      void RemoveProxyManipulator(GameObject targetObject, bool enable)
//     {
//         var objectManipulator = targetObject.GetComponentInChildren<ProxyObjectManipulator>();
//         if (objectManipulator != null)
//         {
//             objectManipulator.enabled = enable;
//             Debug.Log($"{(enable ? "Enabled" : "Disabled")} ObjectManipulator on {targetObject.name}");
//         }
//         else
//         {
//             Debug.Log($"ObjectManipulator not found on {targetObject.name}");
//         }
//     }
// }


