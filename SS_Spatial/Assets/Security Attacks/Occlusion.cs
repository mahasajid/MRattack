using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;


public class flicker : MonoBehaviour
{
    private float spawnInterval = 4;
    private float startDelay = 1.0f;
   // float randomTimer = Random.Range(3f, 5f);
    // private MagicLeapInputs _magicLeapInputs;
    // private MagicLeapInputs.ControllerActions _controllerActions;

    //public GameObject cube;
    private Renderer DebugCubeRenderer;
 
 
 
    // Start is called before the first frame update
    void Start()
    {
         float randomTimer = Random.Range(5f, 20f);

        // initialize InputActionAsset, ControllerActions using ML Input
        // _magicLeapInputs = new MagicLeapInputs();
        // _magicLeapInputs.Enable();
        // _controllerActions = new MagicLeapInputs.ControllerActions(_magicLeapInputs);
    
        DebugCubeRenderer = GetComponent<Renderer>();

        // // Subscribe to controller events
        // _controllerActions.Bumper.performed += HandleOnBumper;
        
        InvokeRepeating("Show", startDelay, randomTimer);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


 void Show()
    {
        DebugCubeRenderer.enabled =  !DebugCubeRenderer.enabled;;
    }


}
