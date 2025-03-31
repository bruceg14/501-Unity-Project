using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEffect : MonoBehaviour
{

    //Holds invuln time
    [SerializeField]
    private float invulnTime;

    //Holds matWhite material
    [SerializeField]
    Material matWhite;

    //Holds current invuln time
    private float invulnTimer = 0;

    //Hold is player is invulnerable
    public static bool invuln = false;

    //Holds default shader
    private Material matDefault;

    //Component Reference
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        //Get default material
        matDefault = sr.material;
    }

    IEnumerator flashesCor()
    {
        //Red flash
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;

        //Go in tell time reaches time
        while (invulnTimer < invulnTime)
        {           
            //increment timer
            invulnTimer += Time.deltaTime;

            //Alternate between transparent and not trasnpaert
            if (sr.color == new Color(1.0f, 1.0f, 1.0f, 1.0f))
            {
                sr.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
            else
            {
                sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }

            //Wait increasingly smaller amounts based on timer completion precentage
            if ((0.4f - ((invulnTimer / invulnTime) / 2)) > 0.1)
            {
                yield return new WaitForSeconds(0.4f - ((invulnTimer / invulnTime) / 2));
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }

        //Resets color
        sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //Resets invuln timer
        invulnTimer = 0.0f;
        //Sets invuln back to false
        invuln = false;
    }
   
    public void hitEffectStart()
    {
        //turns on invuln
        invuln = true;
        //Starts flashes coroutine
        StartCoroutine(flashesCor());
    }
}
