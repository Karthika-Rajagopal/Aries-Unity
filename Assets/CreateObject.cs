using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

// Adding AR Foundation Package
// Adding ARCore XR Plugin Package
[System.Serializable]
public class CreateObject : MonoBehaviour
{
    public string url = "https://dev.wikibedtimestories.com/webservices/ARIES/api/get_all_game_Char.php?page_size=3";

    //Model Class
    public class GetObjectData
    {
        public string Type { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string z { get; set; }
        public string size { get; set; }
    }
    public class RootObject
    {
        public GetObjectData[] users;

    }
    void Start()
    {
        StartCoroutine(GetData_Coroutine());
    }
    void Update()
    {
    }
    IEnumerator GetData_Coroutine()
    {
        RootObject myObject = new RootObject();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);

            var jsonArrayString = request.downloadHandler.text;
            var apiDataList = JsonConvert.DeserializeObject<IList<GetObjectData>>(jsonArrayString);
            foreach (var apidata in apiDataList)
            {
                if (apidata.Type == "Sphere")
                {
                    float sphereX = float.Parse(apidata.x);
                    float sphereY = float.Parse(apidata.y);
                    float sphereZ = float.Parse(apidata.z);
                    GameObject Sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Sphere1.transform.position = new Vector3(sphereX, sphereY, sphereZ);
                }
                if (apidata.Type == "Cube")
                {
                    float cubeX = float.Parse(apidata.x);
                    float cubeY = float.Parse(apidata.y);
                    float cubeZ = float.Parse(apidata.z);
                    GameObject Cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Cube1.transform.position = new Vector3(cubeX, cubeY, cubeZ);
                }
            }
        }
    }
}


