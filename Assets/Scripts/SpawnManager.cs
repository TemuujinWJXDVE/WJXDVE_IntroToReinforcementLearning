using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private int SpawnTime = 30;
    private int counter = 0;

    private void FixedUpdate()
    {
        if (counter > SpawnTime)
        {
            float xPosition = Random.Range(-9, 9);
            var enemy = Instantiate(
                EnemyPrefab,
                transform.position + new Vector3(xPosition, 0.3f, 4),
                Quaternion.Euler(180f, 0f, 0f),
                gameObject.transform);
            counter = 0;
        }
        else
        {
            counter++;
        }
    }
}
