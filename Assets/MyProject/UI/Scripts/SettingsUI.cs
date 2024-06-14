using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Scrollbar _master;
    [SerializeField] private Scrollbar _music;

    public const string MasterVolumeSettings = "MasterVolume";
    public const string MusicVolumeSettings = "MusicVolume";

    private void OnEnable()
    {
        _master.value = PlayerPrefs.GetFloat(MasterVolumeSettings, 1);
        _music.value = PlayerPrefs.GetFloat(MusicVolumeSettings, 1);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void Save()
    {
        float masterVolume = _master.value;
        float musicVolume = _music.value;

        _mixer.SetFloat("Master", Mathf.Lerp(-80, 0, _master.value));
        _mixer.SetFloat("Music", Mathf.Lerp(-80, 0, _music.value));

        PlayerPrefs.SetFloat(MasterVolumeSettings, masterVolume);
        PlayerPrefs.SetFloat(MusicVolumeSettings, musicVolume);
        PlayerPrefs.Save();
    }
}
