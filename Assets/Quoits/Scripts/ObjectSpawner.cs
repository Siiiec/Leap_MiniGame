using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ObjectSpawner : MonoBehaviour
{
    public InteractionBehaviour prefab;

    public Vector3 spawnPos = Vector3.zero;

    public void Spawn()
    {
        Spawn(10f);
    }

    public void Spawn(float deleteTime)
    {
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
