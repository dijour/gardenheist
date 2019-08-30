using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPickup : MonoBehaviour
{
    public MonoBehaviour outlining;

    private bool outlineEnabled;

    void OnEnable()
    {
        RemoveOutline();
    }

    void Update()
    {
        outlining.enabled = outlineEnabled;
    }

    public void AddOutline() {
        outlineEnabled = true;
        //outlining.OnEnable();
    }

    public void RemoveOutline() {
        outlineEnabled = false;
        //outlining.OnDisable();
    }
}
