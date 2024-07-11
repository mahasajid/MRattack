using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Mirror;
using System.Linq;



public class FindRemoteObject : MonoBehaviour
{
    // Start is called before the first frame update
     private float startDelay = 10.0f;

    private GameObject remote;
     private GameObject remote2;
    private GameObject cube;
    private GameObject cube2;
    

    private int count = 0;
    private Collider cubeCollider; // Collider component of the cube
    private Collider cube2Collider; // Collider component of the cube

    private Bounds bounds;
    private Bounds bounds2;
    void Start()
    {
        float randomTimer = Random.Range(10.0f, 30.0f);
        InvokeRepeating("Show", startDelay, randomTimer);
        Debug.Log("In Start");

    }

    // Update is called once per frame
    void Update()
    {
        if (cube != null)
        {
            GameObject parentObject = remote.transform.parent.gameObject;
            bounds = GetBoundsWithPadding(parentObject, 1.0f); 
            cube.transform.position = bounds.center; // Set cube position to center of bounds      
            cube.transform.localScale = bounds.extents/2.5f;
        }
    }

    void Show()
    {
         Debug.Log("Show");
        var objects = GameObject.FindGameObjectsWithTag("ObjectFinder");
    
        if (count == 0)
        {
            if (objects.Count() != 0)
            {
                remote = objects.ToList()[Random.Range(0,objects.Count())]; 
                remote = remote.transform.GetChild(0).gameObject;
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                // Make Cube Transparent
                cube.GetComponent<Renderer>().material.color = Color.clear;//new Color(0, 204, 102, 0);
                Color cubeColor = cube.GetComponent<Renderer>().material.color;
                cubeColor.a = 0.0f;
                var transparentMaterial = new Material(Shader.Find("Transparent/Diffuse"));
                transparentMaterial.color = cubeColor; // Transparent white
                
                cube.GetComponent<Renderer>().material = transparentMaterial;

                cube.GetComponent<Renderer>().material.color = cubeColor;
                cube.GetComponent<Renderer>().material.SetFloat("_Mode", 2); // Set material rendering mode to transparent
                cube.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                cube.GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                cube.GetComponent<Renderer>().material.SetInt("_ZWrite", 0);
                cube.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
                cube.GetComponent<Renderer>().material.EnableKeyword("_ALPHABLEND_ON");
                cube.GetComponent<Renderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                //BoxCollider boxCollider = cube.AddComponent<BoxCollider>();
        
                Debug.Log("In SHOW");

                cubeCollider = cube.AddComponent<BoxCollider>();
                cubeCollider.isTrigger = false;
                Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
                Debug.Log(rigidbody);
                GameObject parentObject = remote.transform.parent.gameObject;

                bounds = GetBoundsWithPadding(parentObject, 1.0f); // Calculate bounds including padding
                Debug.Log(remote.transform.position);
                cube.transform.position = bounds.center; // Set cube position to center of bounds
                Debug.Log(cube.transform.position);
                cube.transform.localScale = bounds.size + Vector3.one/30f; // 

                //cube.GetComponent<Renderer>().material.color.a =0;

                Debug.Log("first object");
                count = count + 1;
            }
        }
           

        if (count == 1)
        {
           
            // if (objects.Count() != 0)
            // {

            //     remote2 = objects.ToList()[Random.Range(0,objects.Count())]; 
            //     remote2 = remote2.transform.GetChild(0).gameObject;

            //     remote2.GetComponent<Renderer>().material.color =  new Color(100, 0, 0);
            //     Debug.Log("second object");
            //     count++;

            //     cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //     cube2.GetComponent<Renderer>().material.color = Color.clear;//new Color(0, 204, 102, 0);
            //     Color cube2Color = cube2.GetComponent<Renderer>().material.color;
            //     var transparentMaterial = new Material(Shader.Find("Transparent/Diffuse"));
            //     transparentMaterial.color = new Color(1f, 1f, 1f, 0f); // Transparent white
            //     cube.GetComponent<Renderer>().material = transparentMaterial;
            //     // cube2Color.a = 0.1f;
            //     // cube2.GetComponent<Renderer>().material.color = cube2Color;
            //     // cube2.GetComponent<Renderer>().material.SetFloat("_Mode", 2); // Set material rendering mode to transparent
            //     // cube2.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            //     // cube2.GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //     // cube2.GetComponent<Renderer>().material.SetInt("_ZWrite", 0);
            //     // cube2.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
            //     // cube2.GetComponent<Renderer>().material.EnableKeyword("_ALPHABLEND_ON");
            //     // cube2.GetComponent<Renderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        
            //     Debug.Log("In SHOW");

            //     cube2Collider = cube2.AddComponent<BoxCollider>();
            //     cube2Collider.isTrigger = false;
            //     GameObject parent2Object = remote.transform.parent.gameObject;

            //     bounds2 = GetBoundsWithPadding(parent2Object, 1.0f); // Calculate bounds including padding
            //     Debug.Log(remote2.transform.position);
            //     cube2.transform.position = bounds2.center; // Set cube position to center of bounds
            //     Debug.Log(cube2.transform.position);
            //     cube2.transform.localScale = bounds2.size; // 

                
                
            // }

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

}
