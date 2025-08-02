using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class car : MonoBehaviour
{
    private static car instance = null;
    public int pos ;
    public float speed;
    public manager m;
    public float min_cost;
    public List<int> route;

    public float moveSpeed = 5f;
    private Coroutine moveCoroutine;
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

    public static car Instance
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
    public void setInit()
    {
        Vector3 ESSpos = graph_manager.Instance.getESSpoint();
        transform.position = ESSpos - new Vector3(0,0.3f,0);
        min_cost = 10000000000000000000.0f;
    }
    private void deep(int pos,float cost,List<int> deact_noe,List<int> rou)
    {
        if (deact_noe.Contains(pos))
        {
            deact_noe.Remove(pos);
        }
        if (deact_noe.Count <= 0) {
            if (cost < min_cost)
            {
                min_cost = cost;
                route = rou.ToList();
                string s = "tes:";
                foreach (int n in rou)
                {
                    s += n.ToString() + ", ";
                }
                Debug.Log(s);
            }
            rou.RemoveAt(rou.Count - 1);
            return;
        }

        float[,] adj_road = graph_manager.Instance.Graph.adj_grid_matrix;
        for(int i = 0;i < adj_road.GetLength(0); i++){

       
            if (adj_road[i, pos] != 0) {
                float tem_cost = 0;
                foreach(int n in deact_noe)
                {
                    tem_cost += graph_manager.Instance.Graph.nodes[n].economy_value + graph_manager.Instance.Graph.nodes[n].man_value;     
                }
                float time = adj_road[i, pos] / speed;
                if(cost+tem_cost*time > min_cost)
                {
                    rou.RemoveAt(rou.Count-1);
                    return;
                }
                rou.Add(i);

                deep(i, cost + tem_cost * time, deact_noe,rou.ToList());

            }
        }
        if (rou.Count == 0) {
            return;
                }
        rou.RemoveAt(rou.Count - 1);
    }
    public void MoveTo(int n_pos)
    {

        Vector3 targetPosition = graph_manager.Instance.Graph.nodes[n_pos].transform.position;
        // 이미 이동 중이면 기존 코루틴 종료
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveCoroutine(targetPosition,n_pos));
    }

    private IEnumerator MoveCoroutine(Vector3 targetPosition,int n_pos)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // 선형 보간으로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        transform.position = targetPosition; // 정확한 위치 정렬
        graph_manager.Instance.Graph.nodes[n_pos].Activate();
        m.GetComponent<manager>().flag = 0;
        moveCoroutine = null;
    }

    public void startSerch()
    {/*
        deep(this.pos,0,graph_manager.Instance.Graph.deactivate_nodes,new List<int>());
        string s = "rou";
        foreach(int n in route)
        {
            s += n.ToString() +", ";
        }
        Debug.Log(s);*/
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
