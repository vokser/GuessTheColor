using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCubeColor : MonoBehaviour
{
    private Color cubeColor;

    private void Start()
    {
        Renderer mainRenderer = GetComponent<Renderer>();
        cubeColor = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        if (mainRenderer != null)
        {
            mainRenderer.material.color = cubeColor;
        }
    }
}
