using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_HashSet : MonoBehaviour
{


    HashSet<int> hash = new HashSet<int>();

    // Use this for initialization
    void Start()
    {
        hash.Add(321);
        hash.Add(3);
        hash.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
