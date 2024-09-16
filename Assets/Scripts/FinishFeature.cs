using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFeature : MonoBehaviour
{
    public Light directLight;
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public Material daySkyboxMaterial;
    public Material nightSkyboxMaterial;
    public float lightIntense = 10.0f;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.skybox=daySkyboxMaterial;
            directLight.intensity=lightIntense;
            if (enemy != null)
            {
                Destroy(enemy);
                Destroy(enemy2);
                Destroy(enemy3);
            }
        }
    }
}
