using UnityEngine;

public class DetectCollision : MonoBehaviour 
{
    private MyAgent myAgent;

    void Start()
    {
        myAgent = UnityEngine.Object.FindFirstObjectByType<MyAgent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            if (myAgent != null)
            {
                myAgent.AddReward(0.5f);
            }
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
            myAgent.wallCounter++;
        }
    }
}
