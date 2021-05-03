using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //All nodes that this node can see
    public List<Node> connectedNodes;
    public int number;

    void Start()
    {
        number = int.Parse((this.name.Split('(')[1]).Split(')')[0]);

        //Find all nodes
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        //Loop for checking connected nodes
        for (int i = 0; i < nodes.Length; i++)
        {
            //Making sure the node doesn't check if it's connected to itself.
            if(nodes[i] != gameObject)
            {
                //Check is every corner of this node visible to every corner of the node we're checking?
                bool allCorners = true;
                for(float x = -transform.localScale.x /2; x <= transform.localScale.x / 2; x+= transform.localScale.x)
                {
                    for(float y = -transform.localScale.x / 2; y<= transform.localScale.x / 2; y+= transform.localScale.x)
                    {
                        //Check all 4 corners
                        Vector2 start = (Vector2)transform.position + new Vector2(x/4,  y/4);
                        Vector2 end = (Vector2)nodes[i].transform.position + new Vector2(x/4,  y/4);
                        if(Physics2D.Linecast(start, end))
                        {
                            //A corner doesn't connect
                            allCorners = false;
                        }
                    }
                }
                //If all corners are visible then add the node we checked to connected nodes
                if(allCorners)
                {
                    connectedNodes.Add(nodes[i].GetComponent<Node>());
                }
            }
        }
    }

}
