using System;
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
        Salvar();
    }

    private void OnApplicationPause()
    {
        Salvar();
    }

    private static void Salvar()
    {
        JsonHolder buffer = new JsonHolder();
        buffer.stats = SlimeLogic.Instance.GetStats();
        buffer.lastTime = DateTime.Now;

        string json = JsonUtility.ToJson(buffer);
        File.WriteAllText(Caminho, json);
    }

    private void Carregar()
    {
        if (File.Exists(Caminho))
        {
            string json = File.ReadAllText(Caminho);
            JsonHolder buffer = JsonUtility.FromJson<JsonHolder>(json);
            
            SlimeLogic.Instance.SetStats(buffer.stats);
            SlimeTimers.Instance.PassiveStatsUpdate(buffer.lastTime);
        }
    }
}
