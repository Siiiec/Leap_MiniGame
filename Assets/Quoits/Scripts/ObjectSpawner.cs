using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ObjectSpawner : MonoBehaviour
{
    public InteractionBehaviour prefab;

    public void Spawn()
    {
        Spawn(10f);
    }

    public void Spawn(float deleteTime)
    {
        var obj = Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(obj.gameObject, deleteTime);
    }

    
}
