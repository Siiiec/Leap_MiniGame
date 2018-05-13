using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Leap.Unity.Examples;

public class GraspingSpawner : MonoBehaviour
{

    public InteractionBehaviour objA, objB;

    public InteractionBehaviour prefab;

    InteractionBehaviour intObj;

    InteractionHand leftHand;

    InteractionHand rightHand;

    bool _swapScheduled;

    // Use this for initialization
    void Start()
    {
        objA = GetComponent<InteractionBehaviour>();

        intObj = GetComponent<InteractionBehaviour>();
        intObj.OnPerControllerGraspBegin += OnGraspBegin;
        intObj.OnPerControllerGraspEnd += OnGraspEnd;

        PhysicsCallbacks.OnPostPhysics += onPostPhysics;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGraspBegin(InteractionController controller)
    {
        if (controller.isLeft)
        {
            if (leftHand == null)
                leftHand = controller.intHand;
        }
        else
        {
            if (rightHand == null)
                rightHand = controller.intHand;
        }

        if (leftHand != null && rightHand != null)
        {
            objB = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            scheduleSwap();
        }
    }

    void OnGraspEnd(InteractionController controller)
    {
        if (controller.isLeft)
        {
            if (leftHand.Equals(controller.intHand))
                leftHand = null;
        }
        else
        {
            if (rightHand.Equals(controller.intHand))
                rightHand = null;
        }
    }

    private void scheduleSwap()
    {
        _swapScheduled = true;
    }

    private void onPostPhysics()
    {
        //Swapping when both objects are grasped is unsupported
        if (objA == null || objB == null) return;

        if (objA.isGrasped && objB.isGrasped) { return; }

        if (_swapScheduled && (objA.isGrasped || objB.isGrasped))
        {

            // Swap "a" for "b"; a will be whichever object is the grasped one.
            InteractionBehaviour a, b;
            if (objA.isGrasped)
            {
                a = objA;
                b = objB;
            }
            else
            {
                a = objB;
                b = objA;
            }

            // (Optional) Remember B's pose and motion to apply to A post-swap.
            var bPose = new Pose(b.rigidbody.position, b.rigidbody.rotation);
            var bVel = b.rigidbody.velocity;
            var bAngVel = b.rigidbody.angularVelocity;

            // Match the rigidbody pose of the originally held object before swapping.
            // If it exists, always use the latestScheduledGraspPose to perform a SwapGrasp!
            // This prevents subtle slippage with non-kinematic objects that may experience
            // gravity forces, drag, or hit other objects, which can leak into the new
            // grasping pose when the SwapGrasp is performed.
            if (a.latestScheduledGraspPose.HasValue)
            {
                b.rigidbody.position = a.latestScheduledGraspPose.Value.position;
                b.rigidbody.rotation = a.latestScheduledGraspPose.Value.rotation;
            }
            else
            {
                b.rigidbody.position = a.rigidbody.position;
                b.rigidbody.rotation = a.rigidbody.rotation;
            }

            // Swap!
            a.graspingController.SwapGrasp(b);

            // Move A over to where B was, and for fun, let's give it B's motion as well.
            a.rigidbody.position = bPose.position;
            a.rigidbody.rotation = bPose.rotation;
            a.rigidbody.velocity = bVel;
            a.rigidbody.angularVelocity = bAngVel;
        }

        _swapScheduled = false;
    }
}
