using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;

    [SerializeField]
    public float xMin;

    [SerializeField]
    public float xMax;

    [SerializeField]
    public float yMin;

    [SerializeField]
    public float yMax;
    private float initialPlayerYAxis;
    
    private void Start() {
        initialPlayerYAxis = player.transform.position.y;
    }

    private void Update() {
        
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin/* + (player.transform.position.y - initialPlayerYAxis)*/, yMax);

        Vector3 currentPosition = new Vector3(x, y, gameObject.transform.position.z);
        transform.position = currentPosition;
    }
}
