using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;


public class OcclusionAutomatedAttack : MonoBehaviour
{
   // private float spawnInterval = 4;
    // private float startDelay = 400.0f;
    private float startDelay = 10.0f;


    //public GameObject cancelButton;
    public GameObject remote;
    public GameObject cube;
   // private Renderer cancelButtonRenderer;
    private Renderer CubeRenderer;
 
    private bool first = true;
 
 
    // Start is called before the first frame update
    void Start()
    {
        float randomTimer = Random.Range(30f, 60f);
    
        CubeRenderer = cube.GetComponent<Renderer>();
       // cancelButtonRenderer = cancelButton.GetComponent<Renderer>();
        
        InvokeRepeating("Show", startDelay, randomTimer);
        Debug.Log("In Start");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Show()
    {
    
        if (first == true)
        {
            // cancelButtonRenderer.enabled = true;
            
            remote.transform.InverseTransformPoint(cube.transform.position);
            CubeRenderer.enabled = !CubeRenderer.enabled;
            Debug.Log("In SHOW");
            first = false;

        }
    }

    public void Hide()
    {
      

      //  cancelButtonRenderer.enabled = false;
        CubeRenderer.enabled = false;
    
        Debug.Log("In HIDE");
    }

}
