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

    void Update()
    {
        // Count the number of Enemies active in the Scene.
        int remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("Number of enemies remaining: " + remainingEnemies.ToString());

        enemiesRemainingText.text = remainingEnemies.ToString();

        // Debug.Log("Color of the Gun: " + gun.GetComponent<Material>().color.ToString());
        gunSprite.color = gun.GetComponent<MeshRenderer>().material.color;
    }
}