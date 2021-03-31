using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    public TMP_Dropdown m_Dropdown;

    public AudioSource backgroundAudio;

    public AudioClip bg1;
    public AudioClip bg2;
    public AudioClip bg3;

    void Start()
    {
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });

    }

    public void Resume()
    {
        EventBroker.CallResumeGame();
    }

    public void Quit()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void Restart()
    {
        EventBroker.CallResumeGame();
        EventBroker.CallInitiateGame(gameController.InitialEnemyCount);
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        //0,1,2
        switch (change.value)
        {
            case 0:
                backgroundAudio.clip = bg1;
                break;
            case 1:
                backgroundAudio.clip = bg2;
                break;
            case 2:
                backgroundAudio.clip = bg3;
                break;
            default: break;
        }
        backgroundAudio.Play();
    }

}
