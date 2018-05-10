using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

public class TEST_BeginEndGrasp : MonoBehaviour
{

    InteractionBehaviour interaction;


    // Use this for initialization
    void Start()
    {
        interaction = GetComponent<InteractionBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            interaction.ReleaseFromGrasp();
        }
        else if (Input.GetKey(KeyCode.B))
        {
            var tempControllers = new List<InteractionController>();
            tempControllers.Add(interaction.closestHoveringController);
            interaction.BeginGrasp(tempControllers);
        }
    }
}
