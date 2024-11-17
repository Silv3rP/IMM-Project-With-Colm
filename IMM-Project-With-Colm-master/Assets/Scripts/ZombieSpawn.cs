using UnityEngine;
using System.Collections.Generic;

public class RandomOpponentSpawner : MonoBehaviour
{
    public GameObject opponentPrefab; 
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxOpponents = 10;

    private List<GameObject> activeOpponents = new List<GameObject>();
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        InvokeRepeating("SpawnOpponent", 2f, spawnInterval);
    }

    private void SpawnOpponent()
    {
        if (activeOpponents.Count >= maxOpponents)
            return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject opponent = Instantiate(opponentPrefab, spawnPoint.position, Quaternion.identity);
        opponent.GetComponent<OpponentController>().playerHealth = playerHealth;
        activeOpponents.Add(opponent);

        CleanupDefeatedOpponents();
    }

    private void CleanupDefeatedOpponents()
    {
        for (int i = activeOpponents.Count - 1; i >= 0; i--)
        {
            if (activeOpponents[i] == null)
            {
                activeOpponents.RemoveAt(i);
            }
        }
    }
}