using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
    public GameObject enemiesWaveBar;
    public EnemyBase zombieEnemy;
    public Camera mainCamera;
    public List<EnemyBase> listOfEnemies=new List<EnemyBase>();

    
    public float spawnTimeOfEnemy;
    private float spawnEnemyTimer;
    
    public List<int> wavesKillEnemyTarget=new List<int>();


    private void Start()
    {
        EventManager.enemyKilledEvent += UpdateEnemiesWaveBar;
    }

    private void OnDestroy()
    {
        EventManager.enemyKilledEvent -= UpdateEnemiesWaveBar;
    }

    private void Update()
    {
        if (spawnEnemyTimer <= 0)
        {
            for (int i = 0; i < GameConstant.WaveNo*2; i++)
            {
               SpawnEnemy(); 
            }

            spawnEnemyTimer = spawnTimeOfEnemy;
        }
        spawnEnemyTimer -= Time.deltaTime;
    }


    private float percentEnemy = 0;
    public void UpdateEnemiesWaveBar()
    {
        percentEnemy = GameConstant.noEnemiesKilled*1f / wavesKillEnemyTarget[GameConstant.WaveNo - 1];
        enemiesWaveBar.transform.localScale=new Vector3(percentEnemy,1,0);
        if (percentEnemy >= 1)
        {
            EventManager.InvokeWaveCompletedEvent();
            Time.timeScale = 0;
            GameConstant.noEnemiesKilled = 0;
        }
    }


    public void SpawnEnemy()
    {
        foreach (EnemyBase enemy in listOfEnemies)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.transform.position = CalculateSpawnPosition();
                enemy.gameObject.SetActive(true);
                return;
            }
        }
        EnemyBase zombie= Instantiate(zombieEnemy, CalculateSpawnPosition(), Quaternion.identity, zombieEnemy.transform.parent);
        zombie.gameObject.SetActive(true);
        listOfEnemies.Add(zombie);
        Vector3 CalculateSpawnPosition()
        {
            float cameraHeight = mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            // Set spawn distance outside the camera's view
            float spawnDistance =Random.Range(2,3.1f); // Distance is set to zero

            // Randomly select a side of the camera to spawn the object
            int sideIndex = Random.Range(0, 4); // 0: Top, 1: Right, 2: Bottom, 3: Left

            Vector3 spawnPosition = Vector3.zero;

            switch (sideIndex)
            {
                case 0: // Top
                    spawnPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), cameraHeight + spawnDistance, 0f);
                    break;
                case 1: // Right
                    spawnPosition = new Vector3(cameraWidth + spawnDistance, Random.Range(-cameraHeight, cameraHeight), 0f);
                    break;
                case 2: // Bottom
                    spawnPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), -cameraHeight - spawnDistance, 0f);
                    break;
                case 3: // Left
                    spawnPosition = new Vector3(-cameraWidth - spawnDistance, Random.Range(-cameraHeight, cameraHeight), 0f);
                    break;
            }

            return spawnPosition;
        }   
    }
}
