using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public class graphSet_to_file : MonoBehaviour, graph_set_sorce
{
    public string json_path;
    // Start is called before the first frame update
    public GraphSet make_GraphSet()
    {
        TextAsset JSONFile = Resources.Load<TextAsset>(json_path);
        GraphSet temp = JsonConvert.DeserializeObject<GraphSet>(JSONFile.text);
        return temp;
    }
}
