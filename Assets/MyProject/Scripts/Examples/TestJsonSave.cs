using System.IO;
using UnityEngine;

public class TestJsonSave : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private string _saveFile = "saveFile";
    private string _path;
    public static TestJsonSave Instance = null;

    private void Start()
    {
        Instance = this;

        _path = Path.Combine(UnityEngine.Windows.Directory.localFolder, _saveFile + ".json");
        if (File.Exists(_path))
            Load();
        else
            Save();

    }

    public void Load()
    {
        string json = File.ReadAllText(_path);
        Vector3 pos = JsonUtility.FromJson<Vector3>(json);
        _player.position = pos;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_player.position);
        File.WriteAllText(_path, json);
    }

}
