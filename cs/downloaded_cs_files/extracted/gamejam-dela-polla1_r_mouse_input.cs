using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r_mouse_input : MonoBehaviour
{

    public int maxSeeds = 1;
    public float autoSpeed = 0.6f;
    public r_power_up_handler inputPowerValue;
    public Camera cam;
    public RaySoundHandler sound;
    public List<GameObject> seeds;

    bool stop = false;


    private void Start()
    {
        maxSeeds = SerializeIntValue.L("seed").value;
        inputPowerValue.SetPower();
    }


    private void OnMouseDown()
    {
        ThrowSeed();
        stop = false;

        this.tt("mouseActivation").Add(autoSpeed,
            (tt) =>
            {

                if (!stop)
                {
                    ThrowSeed();
                }
                else
                {
                    this.tt("mouseActivation").Stop();
                }

            }).Repeat().Immutable();
    }

    //private void OnMouseExit()
    //{
    //    //write.b("0 _ " + name);
    //    this.tt("mouseActivation").Stop();
    //    stop = true;
    //}

    private void OnMouseUp()
    {
        this.tt("mouseActivation").Stop();
        stop = true;
    }

    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //    }
    //}

    private void OnMouseOver()
    {

    }

    void ThrowSeed()
    {

        int activeSeedsNum = 0;

        for (int i = 0; i < seeds.Count; i++)
        {
            if (seeds[i].activeSelf)
            {
                activeSeedsNum++;
            }
        }

        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        // write.b("0 _ " + pos);
        Vector3 targetPos = new Vector3(pos.x, pos.y, 0);

        if (targetPos.y < 3.1f) targetPos = new Vector3(pos.x, 3.1f, 0);

        if (activeSeedsNum < maxSeeds)
        {
            for (int i = 0; i < seeds.Count; i++)
            {

                if (!seeds[i].activeSelf)
                {
                    sound.PlaySound("throe");
                    seeds[i].SetActive(true);
                    seeds[i].transform.position = targetPos;
                    break;
                }
            }
        }
    }

}