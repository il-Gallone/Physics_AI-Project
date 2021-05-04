using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMiniMap : MonoBehaviour
{
    public GameObject objectToActivate;
    bool active = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            active = !active;
            objectToActivate.SetActive(active);
        }
    }
}
