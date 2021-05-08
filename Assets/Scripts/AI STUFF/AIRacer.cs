using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacer : RacerController {

    public float angle = 0;
    public GameObject[] target;
    public int targetNum = 0;
    public Vector3 targetPos;
    public Vector3 lasttargetPos;
    public Vector3 dist;
    public float distAngle;
    public float shortest = Mathf.Infinity;
    public Vector3 closestTarget;
    private int cooldown = 0;
    public float[] dists;
    public int seekCounter = 0;
    GameObject[] nodes;
    LayerMask mask;
    LayerMask racerMask;

    public float Distance;

    void Start() {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        mask = LayerMask.GetMask("Offroad");
        racerMask = LayerMask.GetMask("Racer");
    }

    //returns distance between 2 points
    float distance(Vector3 v1, Vector3 v2) {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));
    }

    void Update() {
        targetPos = target[targetNum].transform.position;
        cooldown--;
        int absc = 0;
        if (lasttargetPos != targetPos) {
            lasttargetPos = targetPos;

            //find closest node to the target

            float absShortest = Mathf.Infinity;
            dists = new float[nodes.Length];
            for (int i = 0; i < nodes.Length; i++) {
                dists[i] = Mathf.Infinity;
                try {
                    RaycastHit2D hit = (Physics2D.Raycast(targetPos, nodes[i].transform.position - targetPos, distance(nodes[i].transform.position, targetPos), mask));
                    if (!hit) {
                        if (distance(nodes[i].transform.position, targetPos) < absShortest)
                        {
                            absShortest = distance(nodes[i].transform.position, targetPos);
                            absc = nodes[i].GetComponent<Node>().number;
                        }
                    }
                } catch { }
            }

            //calculate distances between nodes and target via other nodes
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
                        if (dists[nodes2[n].GetComponent<Node>().number] > dists[updateNodes[i].GetComponent<Node>().number] + distance(updateNodes[i].transform.position, nodes2[n].transform.position))
                        {
                            dists[nodes2[n].GetComponent<Node>().number] = dists[updateNodes[i].GetComponent<Node>().number] + distance(updateNodes[i].transform.position, nodes2[n].transform.position);
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



            //find node with smallest value (closest to target)
            shortest = Mathf.Infinity;
        int c1 = 0;
        for (int c = 0; c < nodes.Length; c++) {
            try {
                GameObject node = GameObject.Find("Node (" + c + ")");
                //bool hit2 = false;
                //RaycastHit2D hit = (Physics2D.Raycast(this.transform.position, node.transform.position - this.transform.position, distance(node.transform.position, this.transform.position), mask));
                //if (distance(node.transform.position, this.transform.position) > 1.5)
                //{
                    //hit2 = true;
                //}

                bool allCorners = true;
                float scale = 1;
                for (float x = scale*-transform.localScale.x / 2; x <= scale * transform.localScale.x / 2; x += scale * transform.localScale.x) {
                    for (float y = scale * -transform.localScale.x / 2; y <= scale * transform.localScale.x / 2; y += scale * transform.localScale.x) {
                        Vector3 start = transform.position + new Vector3(x / 4, y / 4, 0);
                        Vector3 end = node.transform.position + new Vector3(x / 4, y / 4, 0);
                        RaycastHit2D hit = Physics2D.Raycast(start, end - start, distance(start, end), mask);
                        if (hit) {
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
            } catch { }
        }
        if(shortest == Mathf.Infinity)
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

        //if close enough go directly to target
        GameObject n1 = GameObject.Find("Node (" + c1 + ")");
        closestTarget = n1.transform.position;


        RaycastHit2D finalhit = Physics2D.Raycast(targetPos, this.transform.position - targetPos, distance(this.transform.position, targetPos), mask);
        if (!finalhit) {
            if (distance(this.transform.position, targetPos) < 0.5) {
                closestTarget = targetPos;
            }
        }
        Debug.DrawLine(closestTarget, transform.position);

        //steering
        dist = closestTarget - this.transform.position;
        distAngle = Mathf.Atan(dist.y / dist.x) + ((dist.x < 0) ? 3.14159265f : 0);
        angle = (this.transform.rotation.eulerAngles.z+90) * 3.14159265f/180.0f;
        while (angle > 3.14159265f) angle -= 2 * 3.14159265f;
        while (angle < -3.14159265f) angle += 2 * 3.14159265f;
        //Debug.Log("----------------");
        //Debug.Log(distAngle * 180.0f / 3.14159265f);
        //Debug.Log(angle * 180.0f / 3.14159265f);
        distAngle -= angle;
        //Debug.Log(distAngle * 180.0f / 3.14159265f);
        while (distAngle > 3.14159265f) distAngle -= 2 * 3.14159265f;
        while (distAngle < -3.14159265f) distAngle += 2 * 3.14159265f;


        //if (distAngle < 0) {
        //    angle -= turnspeed;
        //} else {
        //    angle += turnspeed;
        //}


        rigid2D.AddForce(transform.up * acceleration * multiplier * weight * Time.deltaTime);

        RaycastHit2D left = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, 30) * transform.up, 1, mask);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -30) * transform.up, 1, mask);
        if(left)
        {
            rigid2D.AddTorque(-handling * weight * Time.deltaTime);
        }
        if(right)
        {
            rigid2D.AddTorque(handling * weight * Time.deltaTime);
        }


        //rigid2D.AddForce(transform.up * acceleration * 1 * Time.deltaTime);
        if (rigid2D.velocity.magnitude > maxSpeed * multiplier)
        {
            rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * multiplier * weight * Time.deltaTime);
        }
        
        if(Mathf.Abs(distAngle*Mathf.Rad2Deg) > Mathf.Abs(rigid2D.angularVelocity/handling))
            rigid2D.AddTorque(Mathf.Sign(distAngle)*Mathf.Min(handling, Mathf.Abs(distAngle*Mathf.Rad2Deg)) * weight * Time.deltaTime);
        else
            rigid2D.AddTorque(Mathf.Sign(-distAngle)*Mathf.Min(handling, rigid2D.angularVelocity) * weight * Time.deltaTime);
        if (rigid2D.angularVelocity > maxTorque)
        {
            rigid2D.AddTorque(-handling * weight * Time.deltaTime);
        }
        if (rigid2D.angularVelocity < -maxTorque)
        {
            rigid2D.AddTorque(handling * weight * Time.deltaTime);
        }

        rigid2D.velocity = Quaternion.Euler(0, 0, rigid2D.angularVelocity * Time.deltaTime) * rigid2D.velocity / (Mathf.Abs(distAngle * distAngle / 500.0f) + (Mathf.Abs(rigid2D.angularVelocity * rigid2D.angularVelocity / 5000000.0f)) + 1);

        //if (mag == 0) {
        //    rigid2D.velocity = (transform.up * -5);
        //}
        //float dot = Mathf.Abs(Vector3.Dot(Vector3.Normalize(transform.up), Vector3.Normalize(rigid2D.velocity)));
        //float mag = rigid2D.velocity.magnitude * dot;
        //rigid2D.AddForce(rigid2D.velocity * -1 * dot);
        //rigid2D.AddForce(transform.up * mag * 1 * dot );

        //this.transform.position = this.transform.position + new Vector3(speed * Time.deltaTime * Mathf.Cos(angle), speed * Time.deltaTime * Mathf.Sin(angle), 0);

        Distance = Vector3.Distance(transform.position, target[targetNum].transform.position);

        if (Distance < 1)
            targetNum++;
        if (targetNum >= target.Length)
            targetNum = 0;
    }

}
