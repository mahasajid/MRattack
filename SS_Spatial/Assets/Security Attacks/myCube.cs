using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCube : MonoBehaviour
{
    private Renderer DebugCubeRenderer;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
         DebugCubeRenderer = cube.GetComponent<Renderer>();
         DebugCubeRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        DebugCubeRenderer = cube.GetComponent<Renderer>();
         DebugCubeRenderer.enabled = true;
    }
}
