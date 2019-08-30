using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRendering : MonoBehaviour
{
    Renderer[] currentRenderers;
 
     void Awake() {
 
        //This assumes that the renderer is active on load
        currentRenderers = GetComponentsInChildren<Renderer>(true);
        SetRendererEnabled(true);
     }
 
     public void SetRendererEnabled(bool enableRenderer) {
         
         for(int x = 0; x < currentRenderers.Length; x++)
             currentRenderers[x].enabled = enableRenderer;
     }
}
