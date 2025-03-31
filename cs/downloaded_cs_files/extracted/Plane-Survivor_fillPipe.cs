using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillPipe : MonoBehaviour {

    [SerializeField] AudioClip explosionSFX;
    [SerializeField] ParticleSystem smoke2;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] ParticleSystem WaterFirst;
    [SerializeField] ParticleSystem WaterFlow;
    [SerializeField] AudioClip waterSFX;
    Inundator inundator;
    BoxCollider2D myBox;
    GameSession gameSession;
    GameObject pipe_big;
    GameObject pipes_full;
    GameObject text;

    void Start()
    {
        myBox = GetComponent<BoxCollider2D>();
        inundator = GameObject.Find("Lava").GetComponent<Inundator>();
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        pipe_big = GameObject.Find("pipe_big");
        pipes_full = GameObject.Find("pipes_Full");
        text = GameObject.Find("text");
        pipes_full.SetActive(false);
        WaterFlow.Stop();
        smoke.Stop();
        smoke2.Stop();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!gameSession.enoughBottles())
            {
                gameSession.showWarning();
            }
            else
            {
                Destroy(myBox);
                text.SetActive(false);
                WaterFirst.Stop();
                smoke2.Play();
                smoke.Play();
                AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position); 
                StartCoroutine(enableSmoke());
            }
        }
    }

    IEnumerator enableSmoke()
    {
        yield return new WaitForSeconds(0.5f);
        gameSession.decrease5NumBottles();
        pipes_full.SetActive(true);
        AudioSource.PlayClipAtPoint(waterSFX, Camera.main.transform.position);
        WaterFlow.Play();
        yield return new WaitForSeconds(0.5f);
        smoke2.Stop();
        pipe_big.SetActive(false);
        inundator.LavaDown();
        yield return new WaitForSeconds(1f);
        pipes_full.SetActive(false);
        smoke.Stop();
        yield return new WaitForSeconds(1f);
        WaterFlow.Stop();
    }
   
}
