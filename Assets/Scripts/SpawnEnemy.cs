using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Подключаем для работы с NavMesh
using YG.Example;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab; // Префаб врага
    public Transform[] spawnPoints;          // Точки спавна врагов
    private float timer = 10f;               // Таймер спавна

    private void Start()
    {
        int childCount = transform.childCount;
        spawnPoints = new Transform[childCount];

        // Получаем все дочерние трансформы
        for (int i = 0; i < childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        SpawnEnemies();
    }

    private void Update()
    {
        // Уменьшаем таймер
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemies();
            timer = 10f; // Сбрасываем таймер
        }
    }

    public void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Позиция для спавна врага
            Vector3 spawnPosition = spawnPoint.position + new Vector3(10f, 0f, 0f);

            // Проверяем, что позиция находится на NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 5f, NavMesh.AllAreas))
            {
                spawnPosition = hit.position; // Обновляем позицию на основе найденной точки на NavMesh

                // Спауним врага в позиции на NavMesh
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"Точка спавна {spawnPosition} не найдена на NavMesh.");
            }
        }
    }
}
