using TreeEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour 
{
    private float speed = 3.0f;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
