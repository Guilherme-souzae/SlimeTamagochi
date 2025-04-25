using System.IO;
using UnityEngine;

public class SlimeFileManagement : MonoBehaviour
{
    private static string Caminho => Application.persistentDataPath + "/slime.json";

    private void Start()
    {
        Carregar();
    }

    private void OnApplicationQuit()
    {
        JsonHolder jsonHolder = new JsonHolder();
        jsonHolder.stats = SlimeLogic.Instance.GetStats();
        Salvar(jsonHolder);
    }

    private void OnApplicationPause()
    {
        JsonHolder jsonHolder = new JsonHolder();
        jsonHolder.stats = SlimeLogic.Instance.GetStats();
        Salvar(jsonHolder);
    }

    private static void Salvar(JsonHolder data)
    {
        SlimeStats stats = SlimeLogic.Instance.GetStats();

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Caminho, json);
    }

    private void Carregar()
    {
        if (File.Exists(Caminho))
        {
            string json = File.ReadAllText(Caminho);
            JsonHolder buffer = JsonUtility.FromJson<JsonHolder>(json);
            
            SlimeLogic.Instance.SetStats(buffer.stats);
        }
    }
}
