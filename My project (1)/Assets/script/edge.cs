using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edge : MonoBehaviour
{
    public Material activate;
    public Material deactivate;

    public int first_node;
    public int second_node;
    // Start is called before the first frame update

    public void setActivate()
    {
        gameObject.GetComponent<MeshRenderer>().material = activate;
    }
    public void setDeactivate()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = deactivate;
    }

    private void Awake()
    {
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
