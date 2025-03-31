using UnityEngine;
using System.Collections;
using UnityEngine.AI;//先加入AI

//并没有什么用的cs
public class Test1 : MonoBehaviour
{

    // Use this for initialization
    private NavMeshAgent navMeshAgent;
    public GameObject player;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();//获取navmeshagent
    }
    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
        if(!navMeshAgent.enabled && (player.transform.position - transform.position).magnitude>10)
        {
            navMeshAgent.enabled = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag.CompareTo("weapon") == 0)
        {
            
            Vector3 offset = collider.gameObject.transform.position - transform.position;
            //Vector3 offset2 = new Vector3(350, 30, 350);
            Vector3 offsetf = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;
            offsetf = (offsetf + new Vector3(0, -1, 0)).normalized * 10 ;
            Debug.Log("????");
            //rb.MovePosition(offset2);
            navMeshAgent.enabled = false;
            //Vector3 movement = -offset.normalized * 10 * Time.deltaTime;
            rb.AddForce(-offsetf * 50);
            //GetComponent<Rigidbody>().velocity += movement;
            //Debug.Log("???" + (transform.position + movement) + " " + player.transform.position);
        }
    }
}
