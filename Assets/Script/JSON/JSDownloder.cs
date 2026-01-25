using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class JSDownloder : MonoBehaviour
{
    private string url = "https://drive.google.com/uc?export=download&id=1iVLpwNENWhFiNx-NHsYb23v3E_jvr0ZU";

    void Start()
    {
        StartCoroutine(LoadJson());
    }

    private IEnumerator LoadJson()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка загрузки JSON: " + request.error);
        }
        else
        {
            string jsonText = request.downloadHandler.text;
            Debug.Log("JSON загружен:\n" + jsonText);

            // Здесь можно распарсить JSON, если нужно
            // Например:
            // MyData data = JsonUtility.FromJson<MyData>(jsonText);
        }
    }
}