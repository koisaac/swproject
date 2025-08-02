using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface generate_calamity
{
    void generate(graph_info info);
    public List<int> Deactivate_nodes();
    public List<int> Deactivate_roads();
    public List<int> Deactivate_grids();

}

public class calamity_manager : MonoBehaviour
{
    public GameObject generate_Calamity;

    private static calamity_manager instance = null;




    public void make()
    {

        graph_manager manager = graph_manager.Instance;

        generate_Calamity.GetComponent<generate_calamity>().generate(manager.info);

        List<int> deact_nodes = generate_Calamity.GetComponent<generate_calamity>().Deactivate_nodes();
        List<int> deact_road = generate_Calamity.GetComponent<generate_calamity>().Deactivate_roads();
        List<int> deact_grid = generate_Calamity.GetComponent<generate_calamity>().Deactivate_grids();


        manager.Deactivate_node(deact_nodes);
        manager.Deactivate_road(deact_road);
        manager.Deactivate_grid(deact_grid);


    }

    // Start is called before the first frame update
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static calamity_manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
