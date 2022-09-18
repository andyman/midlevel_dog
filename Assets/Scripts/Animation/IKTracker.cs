using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTracker : MonoBehaviour
{

	public Animator anim;

	public Transform leftHandTracker;
	public float leftHandPositionWeight = 1.0f;
	public Transform rightHandTracker;
	public float rightHandPositionWeight = 1.0f;
	public Transform leftFootTracker;
	public float leftFootPositionWeight = 1.0f;
	public Transform rightFootTracker;
	public float rightFootPositionWeight = 1.0f;

	public Transform headTracker;
	public float headTrackerWeight = 1.0f;

	void OnAnimatorIK(int layerIndex)
	{
		if (leftHandTracker != null)
		{
			anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTracker.position);
			anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
			anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTracker.rotation);
			anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandPositionWeight);
		}
		else
		{
			anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
			anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);

		}

		if (rightHandTracker != null)
		{
			anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTracker.position);
			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandPositionWeight);
			anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTracker.rotation);
			anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandPositionWeight);
		}
		else
		{
			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
			anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);

		}

		if (leftFootTracker != null)
		{
			anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTracker.position);
			anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPositionWeight);
		}
		else
		{
			anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0.0f);
		}

		if (rightFootTracker != null)
		{
			anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTracker.position);
			anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPositionWeight);
		}
		else
		{
			anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0.0f);
		}

		if (headTracker != null)
		{
			anim.SetLookAtPosition(headTracker.position);
			anim.SetLookAtWeight(headTrackerWeight);
		}
		else
		{
			anim.SetLookAtWeight(0.0f);
		}

	}
}
