using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Leap.Unity.Examples;
using Leap.Unity;
using Leap;
using Leap.Unity.Query;
using System.Linq;

public class GraspingSpawner : MonoBehaviour
{
    public float minScale = 0.05f;
    public float maxScale = 0.2f;

    public InteractionBehaviour pinchArea, prefab;

    InteractionBehaviour intObj, graspingObj;

    List<InteractionController> controllers;

    bool canInstantiate = true;
    bool canScaling = false;
    

    // Use this for initialization
    void Start()
    {
        pinchArea = GetComponent<InteractionBehaviour>();
        intObj = GetComponent<InteractionBehaviour>();
        controllers = new List<InteractionController>();


        //PhysicsCallbacks.OnPrePhysics += OnPhysics;
    }

    // Update is called once per frame
    void Update()
    {
        var right = LeapHandAccessor.right;
        var left = LeapHandAccessor.left;

        Transform(right, left);
        
        var grasping = intObj.graspingControllers;
        if (grasping.Count == 2 && canInstantiate)
        {
            controllers = grasping.Query().ToList();
            Spawn();
        }

        if (canScaling)
            ResizeGraspingObject();
    }

    void Spawn()
    {
        graspingObj = Instantiate(prefab, transform.position, transform.rotation);
        canInstantiate = false;
        canScaling = true;

        Swap(controllers, graspingObj);
        graspingObj.OnGraspEnd += GraspEnded;
    }

    void ResizeGraspingObject()
    {
        if (graspingObj.graspingHands.Count == 2)
        {
            graspingObj.transform.localScale = Vector3.one * Mathf.Clamp(
                Vector3.Distance(controllers[0].intHand.leapHand.GetPinchPosition(),
                    controllers[1].intHand.leapHand.GetPinchPosition())
                , minScale, maxScale);
        }
        else
            canScaling = false;
    }

    void Transform(Hand right, Hand left)
    {

        if (right != null && left != null)
        {
            //transform.position = UnityVectorExtension.ToVector3((right.StabilizedPalmPosition + left.StabilizedPalmPosition) / 2);
            transform.position = Vector3.Lerp(right.GetPredictedPinchPosition(), left.GetPredictedPinchPosition(), 0.5f);
        }
    }

    void GraspEnded()
    {
        canInstantiate = true;
        if (graspingObj.graspingControllers.Count == 0)
        {
            canInstantiate = true;
            controllers.Clear();
            graspingObj.OnGraspEnd -= GraspEnded;
            graspingObj.OnGraspStay -= GraspStay;
        }
    }

    void GraspStay()
    {
        if (graspingObj.graspingControllers.Count == 0)
        {
            graspingObj.transform.localScale = Vector3.one * 
                controllers.Aggregate(Vector3.zero, (init, a) => { return init - a.intHand.position; }).magnitude;
        }
    }

    void Swap(List<InteractionController> controllers, InteractionBehaviour toGrasping)
    {
        foreach (var c in controllers)
            c.SwapGrasp(toGrasping);
    }
}
