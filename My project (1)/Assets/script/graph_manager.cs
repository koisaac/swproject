using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graph_manager : MonoBehaviour
{
    public graph Graph;
    public GameObject graphSetSorce;

    // Start is called before the first frame update
    void Start()
    {
        Graph.make_graph(graphSetSorce.GetComponent<graph_set_sorce>().make_GraphSet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
