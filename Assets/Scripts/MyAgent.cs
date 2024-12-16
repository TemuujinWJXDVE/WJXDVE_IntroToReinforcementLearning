using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MyAgent : Agent
{
    public GameObject BulletPrefab;
    private int SpawnTime = 30;
    private int counter = 0;

    public GameObject Spawner;
    float m_speed = 20;
    private Vector3 startingPosition = new Vector3 (0, 0.3f, -4);
    private float boundXLeft = -9f;
    private float boundXRight = 9f;

    public int wallCounter = 0;

    private enum ACTIONS
    {
        LEFT = 0,
        NOTHING = 1,
        RIGHT = 2
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = startingPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actions = actionsOut.DiscreteActions;

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == -1)
        {
            actions[0] = (int)ACTIONS.LEFT;
        }
        else if (horizontal == +1)
        {
            actions[0] = (int)ACTIONS.RIGHT;
        }
        else
        {
            actions[0] = (int)ACTIONS.NOTHING;
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.DiscreteActions[0];
        switch (actionTaken)
        {
            case (int)ACTIONS.LEFT:
                // move left
                if (transform.localPosition.x > boundXLeft)
                {
                    transform.Translate(Vector3.left * m_speed * Time.fixedDeltaTime);
                }
                break;
            case (int)ACTIONS.RIGHT:
                // move right
                if (transform.localPosition.x < boundXRight)
                {
                    transform.Translate(Vector3.right * m_speed * Time.fixedDeltaTime);
                }
                break;
            case (int)ACTIONS.NOTHING:
                // do nothing
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            var parent = Spawner.transform;
            int numberOfChildren = parent.childCount;
            for (int i = 0; i < numberOfChildren; i++)
            {
                if (parent.GetChild(i).tag == "Enemy")
                    Destroy(parent.GetChild(i).gameObject);
            }
            AddReward(-1.0f);
            EndEpisode();
            wallCounter = 0;
        }
    }

    private void FixedUpdate()
    {
        if (counter > SpawnTime)
        {
            var bullet = Instantiate(
                BulletPrefab,
                transform.position,
                BulletPrefab.transform.rotation);
            Destroy(bullet, 1);
            counter = 0;
        }
        else
        {
            counter++;
        }
        Debug.Log(wallCounter);
        if (wallCounter >= 10)
        {
            var parent = Spawner.transform;
            int numberOfChildren = parent.childCount;
            for (int i = 0; i < numberOfChildren; i++)
            {
                if (parent.GetChild(i).tag == "Enemy")
                    Destroy(parent.GetChild(i).gameObject);
            }
            AddReward(-1.0f);
            EndEpisode();
            wallCounter = 0;
        }
    }
}
