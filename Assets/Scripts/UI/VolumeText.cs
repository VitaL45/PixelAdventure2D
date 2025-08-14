using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] string volumeName;
    [SerializeField] string textIntro; //Sound: or Music:
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volume = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volume.ToString();
    }
}
