using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// Emits a ray.
/// </summary>
public class RayEmitter : MonoBehaviour
{
	/// <summary>
	/// Specifies how long the ray could be emitted.
	/// </summary>
	public float range = 10;
	
	/// <summary>
	/// Specifies how bright the ray will have.
	/// </summary>
    public float intensity = 1;

	public int rayIndex = 1;

    void Update()
    {
		RayInfo.Emit(rayIndex, transform.position, transform.forward, range, range, intensity, transform);
    }
            
    void OnDisable()
    {
    	SendMessage ("OnRayStopSent", this, SendMessageOptions.DontRequireReceiver);
    }
    
    private void OnDrawGizmos()
    {
    	Color prevColor = Gizmos.color;
    	Gizmos.color = Color.yellow;
    	Gizmos.DrawRay(transform.position, transform.forward * 4);
    	Gizmos.DrawWireSphere(transform.position, 0.5f);
    	Gizmos.color = prevColor;
    }
}