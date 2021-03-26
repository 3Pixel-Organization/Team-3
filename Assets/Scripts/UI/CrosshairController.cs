using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrosshairController : MonoBehaviour
{
    /* Control the Settings for the Crosshair */

    public Camera cam;
    public GameObject player;
    public GameObject gun;

    // Display the color of the Enemy under the Crosshair.
    public EnemyController enemyController;

    public RawImage crosshairEnemyColorDisplay;

    // public TextMeshProUGUI enemyColorCrosshair;
    /// <summary>
    /// Returns a raycast.
    /// </summary>
    /// <returns>Raycast</returns>
    // public RaycastHit hit()
    // {
    //     if (Physics.Raycast(r_ray, out r_hit))
    //     {
    //         Debug.Log("Raycasting on: " + r_hit.collider.name);
    //         return r_hit;
    //     }
    //     else
    //         return r_hit;
    // }

    void Update()
    {
        ChangeCrosshairColor();
    }

    // **DO NOT EDIT**
    // /// <summary>
    // /// Create a Ray from the Camera to track the Enemy. 
    // /// </summary>
    // public void RaycastEnemy()
    // {
    //     // Raycast to check the Enemy targetted. 

    //     Ray ray = cam.ViewportPointToRay(new Vector3(0, 0, 0));
    //     RaycastHit hit;

    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         Debug.DrawRay(cam.gameObject.transform.position, player.gameObject.transform.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);
    //         Debug.Log("Raycasting on: " + hit.transform.gameObject.name);
    //     }
    // }
    //  **DO NOT EDIT THE ABOVE PIECE OF CODE**

    /// <summary>
    /// Change the color of the color of the Crosshair displaying the Enemies color.
    /// </summary>
    public void ChangeCrosshairColor()
    {
        crosshairEnemyColorDisplay.color = gun.gameObject.GetComponent<MeshRenderer>().material.color;
    }
}
