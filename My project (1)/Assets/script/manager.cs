using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class node_info
{
    public int node_id;
    public float man_value;
    public float eco_value;
    public bool is_activate;
}
public class edge_info
{
    public int node1;
    public int node2;
    public float distance;
}

public class DataPayload
{
    public int count;
    public bool is_end;
}

public class manager : MonoBehaviour
{
    // Start is called before the first frame update
    public int time = 0 ;
    public float cost = 0;

    public int number = 3 ;
    public int count = 0 ;

    public int flag = 0;
    private void setInit()
    {
        time = 0;
        cost = 0;
        flag = 0;
    graph_manager graphManager = graph_manager.Instance;
        graphManager.make();

        car Car = car.Instance;
        Car.setInit();
    }
    private void setCalamity()
    {
        calamity_manager calmanager = calamity_manager.Instance;
        Debug.Log("p");
        calmanager.make();

    }
    void Start()
    {
        setInit();
        setCalamity();

        graph_manager.Instance.print_road_mat();

        car.Instance.startSerch();
    }







    void Update()
    {

        if(count == 4)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
        }
        if (count<number)
        {
            if(flag == 0)
            {
                Thread.Sleep(800);
                flag = 1;
                DataPayload data = new DataPayload
                {
                     count= count
                    ,is_end = false
                };
                count++;
                string jsonData = JsonConvert.SerializeObject(data); // Newtonsoft.Json 사용
                StartCoroutine(SendPostRequest("http://localhost:5000/step", jsonData));
            }
        }
        
    }
    
    IEnumerator SendPostRequest(string url, string jsonData)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("environment 전송");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;

            try
            {
                ServerResponse response = JsonConvert.DeserializeObject<ServerResponse>(responseText); // JSON 파싱
                Debug.Log("action : "+response.next_node.ToString()+" 노드로 이동");
                car.Instance.MoveTo(response.next_node);
            }
            catch (JsonException e)
            {
                Debug.LogError("JSON 파싱 오류: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("POST 실패: " + request.error);
        }
    }
}

public class ServerResponse
{
    public int next_node;
}