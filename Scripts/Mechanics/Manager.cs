using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool isOver = false;
    int Widht;
    int Height;
    public bool isFullScreen;

    public AudioMixer audioMixer;
    public Toggle volToggle;

    private void Start()
    {
        audioMixer.SetFloat("Volume", -10f);
    }

    public void Change(int index)
    {
        SceneManager.LoadScene(index);
        Debug.Log(index.ToString());
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void SetWidth(int x)
    {
        Widht = x;
    }

    public void SetHeight(int x)
    {
        Height = x;
    }

    public void SetRes()
    {
        Screen.SetResolution(Widht, Height, isFullScreen);
    }

    public void SetScreen(bool YN)
    {
        isFullScreen = YN;
        Screen.fullScreen = true;
    }

    public void OpenHyperlink(string link)
    {
        Application.OpenURL(link);
    }

    public void VolumeManagment(bool volume)
    {
        if(volume == true)
        {
            audioMixer.SetFloat("Volume", -80);
        }
        else
        {
            audioMixer.SetFloat("Volume", -10);
        }
    }

}
