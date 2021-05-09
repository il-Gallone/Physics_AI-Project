using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerController : MonoBehaviour
{
    //racer's stats
    public float maxSpeed;
    public float acceleration;
    public float maxTorque;
    public float handling;
    public float weight;

    //terrain speed modifier
    public float multiplier = 1;

    //used for AI pathfinding/Player Position Tracking
    public Rigidbody2D rigid2D;
    public GameObject[] checkpoints;
    public GameObject[] nodes;
    public LayerMask mask;

    //Various variables for tracking purposes
    public int currentLap = 0;
    public float[] dists;
    public int checkpointNum = 0;
    public Vector3 checkpointPos;
    public float angle = 0;
    public Vector3 dist;
    public float distAngle;
    public float shortest = Mathf.Infinity;
    public Vector3 closestTarget;

    // Start is called before the first frame update
    void Awake()
    {
        //Setting the racer's stats and finding the checkpoints before the race starts
        rigid2D.mass = weight;
        rigid2D.drag = 1 / weight;
        rigid2D.angularDrag = 2 * weight;
        nodes = GameObject.FindGameObjectsWithTag("Node");
        checkpoints = new GameObject[GameObject.FindGameObjectsWithTag("Checkpoint").Length];
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i] = GameObject.Find("Checkpoint (" + i + ")");
        }
        mask = LayerMask.GetMask("Offroad");
    }

    public void CalculateNodeDistance()
    {
        checkpointPos = checkpoints[checkpointNum].transform.position;
        int absc = 0;

        //find closest node to the checkpoints

        float absShortest = Mathf.Infinity;
        dists = new float[nodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            dists[i] = Mathf.Infinity;
            try
            {
                //uses Djikstra's algorithm to find the closest node to the checkpoint
                RaycastHit2D hit = (Physics2D.Raycast(checkpointPos, nodes[i].transform.position - checkpointPos, Vector3.Distance(nodes[i].transform.position, checkpointPos), mask));
                if (!hit)
                {
                    if (Vector3.Distance(nodes[i].transform.position, checkpointPos) < absShortest)
                    {
                        absShortest = Vector3.Distance(nodes[i].transform.position, checkpointPos);
                        absc = nodes[i].GetComponent<Node>().number;
                    }
                }
            }
            catch { }
        }

        //calculate distances between nodes and checkpoints via other nodes
        dists[absc] = 0;
        List<GameObject> updateNodes = new List<GameObject>();
        List<GameObject> updateNodes2 = new List<GameObject>();
        updateNodes.Add(GameObject.Find("Node (" + absc + ")"));
        while (updateNodes.Count != 0)
        {
            for (int i = 0; i < updateNodes.Count; i++)
            {
                List<Node> nodes2 = updateNodes[i].GetComponent<Node>().connectedNodes;
                for (int n = 0; n < nodes2.Count; n++)
                {
                    if (dists[nodes2[n].GetComponent<Node>().number] > dists[updateNodes[i].GetComponent<Node>().number] + Vector3.Distance(updateNodes[i].transform.position, nodes2[n].transform.position))
                    {
                        dists[nodes2[n].GetComponent<Node>().number] = dists[updateNodes[i].GetComponent<Node>().number] + Vector3.Distance(updateNodes[i].transform.position, nodes2[n].transform.position);
                        updateNodes2.Add(GameObject.Find("Node (" + nodes2[n].GetComponent<Node>().number + ")"));
                    }
                }
            }

            updateNodes.Clear();
            for (int i = 0; i < updateNodes2.Count; i++)
            {
                updateNodes.Add(updateNodes2[i]);
            }
            updateNodes2.Clear();
        }
    }

    public void FindClosestToCheckpoint()
    {

        //find node with smallest value (closest to checkpoints)
        shortest = Mathf.Infinity;
        int c1 = 0;
        for (int c = 0; c < nodes.Length; c++)
        {
            try
            {
                GameObject node = GameObject.Find("Node (" + c + ")");
                //bool hit2 = false;
                //RaycastHit2D hit = (Physics2D.Raycast(this.transform.position, node.transform.position - this.transform.position, Vector3.Distance(node.transform.position, this.transform.position), mask));
                //if (Vector3.Distance(node.transform.position, this.transform.position) > 1.5)
                //{
                //hit2 = true;
                //}

                bool allCorners = true;
                float scale = 1;
                for (float x = scale * -transform.localScale.x / 2; x <= scale * transform.localScale.x / 2; x += scale * transform.localScale.x)
                {
                    for (float y = scale * -transform.localScale.x / 2; y <= scale * transform.localScale.x / 2; y += scale * transform.localScale.x)
                    {
                        Vector3 start = transform.position + new Vector3(x / 4, y / 4, 0);
                        Vector3 end = node.transform.position + new Vector3(x / 4, y / 4, 0);
                        RaycastHit2D hit = Physics2D.Raycast(start, end - start, Vector3.Distance(start, end), mask);
                        if (hit)
                        {
                            allCorners = false;
                        }
                    }
                }
                if (allCorners)
                {
                    if (dists[c] < shortest)
                    {
                        shortest = dists[c];
                        c1 = c;
                    }
                }


                /*if (!(hit || hit2))
                {
                    if (dists[c]  < shortest)
                    {
                        shortest2 = shortest;
                        c2 = c1;
                        shortest = dists[c];
                        c1 = c;
                    }
                    else if (dists[c] < shortest2)
                    {
                        shortest2 = dists[c] ;
                        c2 = c;
                    }
                }*/
            }
            catch { }
        }
        //Edgecase incase the AI can't find a node through normal means, just find the closest node overall
        if (shortest == Mathf.Infinity)
        {
            float[] dists2 = new float[nodes.Length];
            for (int c = 0; c < nodes.Length; c++)
            {
                try
                {
                    GameObject node = GameObject.Find("Node (" + c + ")");
                    dists2[c] = Vector3.Distance(node.transform.position, transform.position);
                    if (dists2[c] < shortest)
                    {
                        shortest = dists2[c];
                        c1 = c;
                    }
                }
                catch { }
            }
        }

        //if close enough go directly to checkpoints
        GameObject n1 = GameObject.Find("Node (" + c1 + ")");
        closestTarget = n1.transform.position;

        RaycastHit2D finalhit = Physics2D.Raycast(checkpointPos, this.transform.position - checkpointPos, Vector3.Distance(this.transform.position, checkpointPos), mask);
        if (!finalhit)
        {
            if (Vector3.Distance(this.transform.position, checkpointPos) < 0.5)
            {
                closestTarget = checkpointPos;
            }
        }
    }

    public void FindDistances()
    {
        //calculating the angles and distances between player and checkpoints
        dist = closestTarget - this.transform.position;
        distAngle = Mathf.Atan(dist.y / dist.x) + ((dist.x < 0) ? Mathf.PI : 0);
        angle = (this.transform.rotation.eulerAngles.z + 90) *Mathf.Deg2Rad;
        while (angle > Mathf.PI) angle -= 2 * Mathf.PI;
        while (angle < -Mathf.PI) angle += 2 * Mathf.PI;
        //Debug.Log("----------------");
        //Debug.Log(distAngle * 180.0f / 3.14159265f);
        //Debug.Log(angle * 180.0f / 3.14159265f);
        distAngle -= angle;
        //Debug.Log(distAngle * 180.0f / 3.14159265f);
        while (distAngle > Mathf.PI) distAngle -= 2 * Mathf.PI;
        while (distAngle < -Mathf.PI) distAngle += 2 * Mathf.PI;
        //Original Code created by Joshua, used a manually typed value for pi, which while clever is slightly less readable
    }

    public void CheckForNearbyCheckpoint()
    {
        //If really close to a checkpoint progress through the track
        float distance = Vector3.Distance(transform.position, checkpoints[checkpointNum].transform.position);

        if (distance < 2)
            checkpointNum++;
        //reaching the end of the list means we've done a lap
        if (checkpointNum >= checkpoints.Length)
        {
            checkpointNum = 0;
            currentLap++;
        }
    }
}

