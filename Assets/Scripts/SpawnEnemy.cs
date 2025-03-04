using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // ���������� ��� ������ � NavMesh
using YG.Example;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab; // ������ �����
    public Transform[] spawnPoints;          // ����� ������ ������
    private float timer = 10f;               // ������ ������

    private void Start()
    {
        int childCount = transform.childCount;
        spawnPoints = new Transform[childCount];

        // �������� ��� �������� ����������
        for (int i = 0; i < childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        SpawnEnemies();
    }

    private void Update()
    {
        // ��������� ������
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemies();
            timer = 10f; // ���������� ������
        }
    }

    public void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // ������� ��� ������ �����
            Vector3 spawnPosition = spawnPoint.position + new Vector3(10f, 0f, 0f);

            // ���������, ��� ������� ��������� �� NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 5f, NavMesh.AllAreas))
            {
                spawnPosition = hit.position; // ��������� ������� �� ������ ��������� ����� �� NavMesh

                // ������� ����� � ������� �� NavMesh
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"����� ������ {spawnPosition} �� ������� �� NavMesh.");
            }
        }
    }
}
