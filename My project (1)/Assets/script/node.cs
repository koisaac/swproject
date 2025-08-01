using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class node : MonoBehaviour
{
    // Start is called before the first frame update


    public string node_name;
    public float man_min_value;
    public float man_max_value;

    public float economy_min_value;
    public float economy_max_value;

    private float man_value;
    private float economy_value;

    public float Man_value { get => man_value; }
    public float Economy_value { get => economy_value;}

    void Start()
    {
        
    }

    public void init()
    {
        man_value = Random.Range(man_min_value, man_max_value);
        economy_value = Random.Range(economy_min_value, economy_max_value);


        Transform sphere = transform.GetChild(0);
        Transform text = sphere.GetChild(0);

        text.gameObject.GetComponent<TMP_Text>().text = node_name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
