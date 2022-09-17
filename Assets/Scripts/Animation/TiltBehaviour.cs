using UnityEngine;
using System.Collections;


public class TiltBehaviour : StateMachineBehaviour
{
	public string xParameter = "tiltX";
	public string yParameter = "tiltY";

	public float minInterval = 1.0f;
	public float maxInterval = 4.0f;

	public float minValue = -1f;
	public float maxValue = 1f;

	public float minY = -1f;
	public float maxY = 1f;

	public float minTimeBetweenInterval = 0.0f;
	public float maxTimeBetweenInterval = 0.0f;

	private float nextTime = 0.0f;
	private float startTime = 0.0f;
	private float intervalDuration = 1.0f;

	private Vector2 target = Vector2.zero;
	private Vector2 source = Vector2.zero;

	private int xParameterHash;
	private int yParameterHash;

	private float nextIntervalTime;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		xParameterHash = Animator.StringToHash(xParameter);
		yParameterHash = Animator.StringToHash(yParameter);
		nextIntervalTime = 0.0f;
		source = new Vector2(animator.GetFloat(xParameter), animator.GetFloat(yParameter));
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (Time.time > nextIntervalTime)
		{
			source = new Vector2(animator.GetFloat(xParameter), animator.GetFloat(yParameter));
			target = Random.insideUnitCircle;
			target.x = Mathf.Lerp(minValue, maxValue, Mathf.InverseLerp(-1.0f, 1.0f, target.x));
			target.y = Mathf.Lerp(minY, maxY, Mathf.InverseLerp(-1.0f, 1.0f, target.y));
			startTime = Time.time;
			intervalDuration = Random.Range(minInterval, maxInterval) + 0.01f;
			nextTime = startTime + intervalDuration;

			nextIntervalTime = nextTime + Random.Range(minTimeBetweenInterval, maxTimeBetweenInterval);

		}
		float timeLerp = (Time.time - startTime) / intervalDuration;
		//timeLerp = Mathf.SmoothStep(0.0f, 1.0f, timeLerp);
		Vector2 current = Vector2.Lerp(source, target, timeLerp);
		animator.SetFloat(xParameterHash, current.x);
		animator.SetFloat(yParameterHash, current.y);
	}

}
