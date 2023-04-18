using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class resetOrigin : MonoBehaviour
{
    public ARSession ARSession;
    public void Click()
    {
        ARSession.Reset();
    }
    
}
