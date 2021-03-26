using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /* Manages the UI */

    public ProjectileShooter playerProjectileShooter;
    public GameObject gun;

    public TextMeshProUGUI enemiesRemainingText;

    public RawImage gunSprite;

    public GameObject pausePanel;

    public KeyCode pauseKey = KeyCode.P & KeyCode.Escape;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Count the number of Enemies active in the Scene.
        int remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // Debug.Log("Number of enemies remaining: " + remainingEnemies.ToString());

        enemiesRemainingText.text = remainingEnemies.ToString();

        // Debug.Log("Color of the Gun: " + gun.GetComponent<Material>().color.ToString());
        gunSprite.color = gun.GetComponent<MeshRenderer>().material.color;

        /* Pause Menu */
        if (Input.GetKeyDown(pauseKey))
        {
            PauseGame();
        }

    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Debug.Log("Game Paused!");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Debug.Log("Game Resumed!");
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}