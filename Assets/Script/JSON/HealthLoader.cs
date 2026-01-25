using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HealthLoader : MonoBehaviour
{
    [SerializeField] private Health health; // ссылка на компонент Health
    [SerializeField] private string url = "https://drive.google.com/uc?export=download&id=1iVLpwNENWhFiNx-NHsYb23v3E_jvr0ZU";

    private void Start()
    {
        StartCoroutine(LoadHealthData());
    }

    private IEnumerator LoadHealthData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка загрузки JSON: " + request.error);
            yield break;
        }

        string jsonText = request.downloadHandler.text;
        Debug.Log("JSON загружен:\n" + jsonText);

        CharacterStats stats = JsonUtility.FromJson<CharacterStats>(jsonText);

        if (stats != null)
        {
            health.SetMaxHealth(stats.maxHealth);
        }
        else
        {
            Debug.LogError("Не удалось распарсить JSON");
        }
    }
}

[System.Serializable]
public class CharacterStats
{
    public float maxHealth;
}