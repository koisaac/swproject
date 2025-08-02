using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class node_state : MonoBehaviour
{
    public Material activate;
    public Material deactivate;

    public void setActivate() {
        gameObject.GetComponent<MeshRenderer>().material = activate;
        
    }
    public void setDeactivate()
    {
        gameObject.GetComponent<MeshRenderer>().material = deactivate;
    }
  
    // Start is called before the first frame update

}
