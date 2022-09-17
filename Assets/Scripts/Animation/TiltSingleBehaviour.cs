using UnityEngine;
using System.Collections;

public class TiltSingleBehaviour : StateMachineBehaviour
{
	public string xParameter = "tiltX";

	public float minInterval = 1.0f;
	public float maxInterval = 4.0f;

	public float minValue = -1f;
	public float maxValue = 1f;

	public float minTimeBetweenInterval = 0.0f;
	public float maxTimeBetweenInterval = 0.0f;

	private float nextTime = 0.0f;
	private float startTime = 0.0f;
	private float intervalDuration = 1.0f;

	private float target = 0;
	private float source = 0;

	private int xParameterHash;

	private float nextIntervalTime;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		xParameterHash = Animator.StringToHash(xParameter);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (Time.time > nextIntervalTime)
		{
			source = animator.GetFloat(xParameter);
			target = Random.value;
			target = Mathf.Lerp(minValue, maxValue, Mathf.InverseLerp(-1.0f, 1.0f, target));

			startTime = Time.time;
			intervalDuration = Random.Range(minInterval, maxInterval) + 0.01f;
			nextTime = startTime + intervalDuration;

			nextIntervalTime = nextTime + Random.Range(minTimeBetweenInterval, maxTimeBetweenInterval);

		}
		float timeLerp = (Time.time - startTime) / intervalDuration;
		//timeLerp = Mathf.SmoothStep(0.0f, 1.0f, timeLerp);
		float current = Mathf.Lerp(source, target, timeLerp);
		animator.SetFloat(xParameterHash, current);
	}

}
