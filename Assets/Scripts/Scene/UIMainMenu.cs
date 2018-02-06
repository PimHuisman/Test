using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIMainMenu : MonoBehaviour
{
    public AudioMixer menuMusicMixer;

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicLvL(float musicLvL)
    {
        menuMusicMixer.SetFloat("MenuMusic", musicLvL);
    }
}
