using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using static UnityEngine.Mathf;
using Vector3 = UnityEngine.Vector3;

public class Bot : MonoBehaviour
{
	private enum Moves
	{
		Walk,
		Light,
		TurnLeft,
		TurnRight,
		Jump
	}

	private enum ObstacleType
	{
		Block,
		Wall
	}
	
	private const float RAYCAST_MAX_DISTANCE = 1.7f;
	private const float RAYCAST_ORIGIN_BLOCK_Y = -0.6f;
	private const float RAYCAST_ORIGIN_WALL_Y = 1.7f;
	
	private float duration = 0.5f;
	private float stepSize = -1.7f;
	private float jumpHeight = 1f;
	private float endRotation;
	private Vector3 targetPosition;


	void Awake()
	{
		GetComponent<CapsuleCollider>().enabled = false;
		targetPosition = transform.position;
		endRotation = transform.eulerAngles.y;
	}


	public IEnumerator Move (CommandData command)
	{
		Vector3 currentPosition = transform.position;
		switch (command.Value)
		{
			case (int)Moves.Walk:
				yield return StartCoroutine(Walk(transform.right));
				break;
			case (int)Moves.Light:
				Light();
				break;
			case (int)Moves.TurnLeft:
				yield return StartCoroutine(Turn(-90f));
				break;
			case (int)Moves.TurnRight:
				yield return StartCoroutine(Turn(90f));
				break;
			case (int)Moves.Jump:
				yield return StartCoroutine(Jump());
				break;
		}
		yield return new WaitForSeconds(0.2f); //	TODO: Check without this line
	}
	
	private IEnumerator Walk(Vector3 direction)
	{
		if (!CheckForObstacle(ObstacleType.Block) && !CheckForObstacle(ObstacleType.Wall))
		{
			Vector3 startPosition  = transform.position;
			targetPosition = targetPosition + (direction * stepSize);
			float elapsedTime = 0;
         
			while (elapsedTime < duration)
			{
				transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
	}

	private IEnumerator Turn(float degree)
	{
		float startRotation = transform.eulerAngles.y;
		endRotation = startRotation + degree;
		float elapsedTime = 0;
		
		while (elapsedTime  < duration)
		{
			elapsedTime += Time.deltaTime;
			float yRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / duration);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
			yield return null;
		}
	}

	private void Light()
	{
		GetComponent<CapsuleCollider>().enabled = true;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Block"))
		{
			Block block = collider.transform.GetComponent<Block>();
			block.StartLightSequence();
		}
	}

	private IEnumerator Jump()
	{
		if (CheckForObstacle(ObstacleType.Wall))
		{
			yield return StartCoroutine(JumpDirection(transform.up));
			yield return StartCoroutine(JumpDirection(-transform.up));
		}
		else
		{
			if (CheckForObstacle(ObstacleType.Block))
			{
				yield return StartCoroutine(JumpDirection(transform.up));
				yield return StartCoroutine(Walk(transform.right));
			}
			else
			{
				yield return StartCoroutine(Walk(transform.right));
				yield return StartCoroutine(JumpDirection(-transform.up));
			}
		}	
		
	}

	private IEnumerator JumpDirection(Vector3 direction)
	{
		Vector3 startPosition  = transform.position;
		targetPosition = targetPosition + (direction * jumpHeight);
		float elapsedTime = 0;
         
		while (elapsedTime < duration)
		{
			transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

	private bool CheckForObstacle(ObstacleType obstacleType)
	{
		Vector3 raycastOriginPoint = transform.position;
		RaycastHit hit;

		switch (obstacleType)
		{
			case ObstacleType.Block:
				raycastOriginPoint += new Vector3(0, RAYCAST_ORIGIN_BLOCK_Y, 0);
				break;
			case ObstacleType.Wall:
				raycastOriginPoint += new Vector3(0, RAYCAST_ORIGIN_WALL_Y, 0);
				break;
		}
		
		if (Physics.Raycast(origin: raycastOriginPoint, direction: -transform.right, out hit, 
			    maxDistance: RAYCAST_MAX_DISTANCE, layerMask: Physics.AllLayers,
			    queryTriggerInteraction: QueryTriggerInteraction.UseGlobal))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
