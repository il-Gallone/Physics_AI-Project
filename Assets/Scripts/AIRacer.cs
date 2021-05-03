using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacer : RacerController {

    public float angle = 0;
    public float turnspeed = 0.01f;
    public float speed = 1f;
    public GameObject target;
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

    void Start() {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        mask = LayerMask.GetMask("wall");
    }

    //returns distance between 2 points
    float distance(Vector3 v1, Vector3 v2) {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y));
    }

    void Update() {
        targetPos = target.transform.position;
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
        float shortest2 = Mathf.Infinity;
        int c1 = 0;
        int c2 = 0;
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
                float scale = 3;
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
                if (allCorners) {
                    if (dists[c] < shortest) {
                        shortest2 = shortest;
                        c2 = c1;
                        shortest = dists[c];
                        c1 = c;
                    } else if (dists[c] < shortest2) {
                        shortest2 = dists[c];
                        c2 = c;
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

        //if close enough go directly to target
        GameObject n1 = GameObject.Find("Node (" + c1 + ")");
        if (c1 == absc) {
            if (distance(this.transform.position, n1.transform.position) < 0.5) {
                c2 = c1;
            }
        }
        c2 = c1;
        GameObject n2 = GameObject.Find("Node (" + c2 + ")");
        closestTarget = (n1.transform.position + n2.transform.position) / 2.0f;


        RaycastHit2D finalhit = Physics2D.Raycast(targetPos, this.transform.position - targetPos, distance(this.transform.position, targetPos), mask);
        if (!finalhit) {
            if (distance(this.transform.position, targetPos) < 0.5) {
                closestTarget = targetPos;
            }
        }

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


        RaycastHit2D forward = Physics2D.Raycast(transform.position, transform.up, 1, mask);
        RaycastHit2D left = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, 30) * transform.up , 1, mask);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -30) * transform.up , 1, mask);
        
        if (forward) {
            rigid2D.AddForce(-transform.up * acceleration * 1 * Time.deltaTime);
            //Debug.DrawRay(transform.position, transform.up, Color.red);

        } else {
            rigid2D.AddForce(transform.up * acceleration * 1 * Time.deltaTime);
            //Debug.DrawRay(transform.position, transform.up, Color.green);
        }

        float mag = rigid2D.velocity.magnitude;
        if (left) {
            rigid2D.AddTorque(Mathf.Max(mag, 2) * handling * -1 / 2.0f * Time.deltaTime);
           // Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, 30) * transform.up, Color.red);
        } else {
            //Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, 30) * transform.up, Color.green);
        }

        
        if (right) {
            rigid2D.AddTorque(Mathf.Max(mag, 2) * handling * 1 / 2.0f * Time.deltaTime);
            //Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -30) * transform.up, Color.red);
        } else {
            //Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -30) * transform.up, Color.green);
        }

        //rigid2D.AddForce(transform.up * acceleration * 1 * Time.deltaTime);
        if (rigid2D.velocity.magnitude > maxSpeed)
        {
            rigid2D.AddForce(-rigid2D.velocity.normalized * acceleration * Time.deltaTime);
        }
        
        rigid2D.AddTorque(Mathf.Max(mag, 2) * handling * (Mathf.Sign(distAngle)/4.0f + distAngle/2.0f) * Time.deltaTime);
        if (rigid2D.angularVelocity > maxTorque)
        {
            rigid2D.AddTorque(-handling * Time.deltaTime);
        }
        if (rigid2D.angularVelocity < -maxTorque)
        {
            rigid2D.AddTorque(handling * Time.deltaTime);
        }
        rigid2D.angularVelocity *= 0.99f;

        //float mag = rigid2D.velocity.magnitude;
        rigid2D.velocity = (transform.up*mag)/(Mathf.Abs(distAngle* distAngle/500.0f) + (Mathf.Abs(rigid2D.angularVelocity * rigid2D.angularVelocity / 5000000.0f))+1);

        //if (mag == 0) {
        //    rigid2D.velocity = (transform.up * -5);
        //}
        //float dot = Mathf.Abs(Vector3.Dot(Vector3.Normalize(transform.up), Vector3.Normalize(rigid2D.velocity)));
        //float mag = rigid2D.velocity.magnitude * dot;
        //rigid2D.AddForce(rigid2D.velocity * -1 * dot);
        //rigid2D.AddForce(transform.up * mag * 1 * dot );

        //this.transform.position = this.transform.position + new Vector3(speed * Time.deltaTime * Mathf.Cos(angle), speed * Time.deltaTime * Mathf.Sin(angle), 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if hit a wall just turn 180 degrees
        if (cooldown < 0) {
            angle += 3.14159265f;
            cooldown = 2;
        }
    }
}
