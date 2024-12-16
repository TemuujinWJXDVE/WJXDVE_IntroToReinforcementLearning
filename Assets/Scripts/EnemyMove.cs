using TreeEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour 
{
    public float speed = 10.0f;
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
