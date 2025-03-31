using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public static bool tutorial;
    public bool isTutorialButton = false;
    public int beatsToSwitch;
    public GameObject container1, container2;
    public static Dictionary<string, string> tutorialNames = new Dictionary<string,string>
    { 
        { "Recoil Only", "tutorial_rock" },
        { "TeleportOnly", "tutorial_jump" }, 
        { "FourQuadsCopy", "tutorial_quad" },
        { "Hackeysack", "tutorial_airball" }
    };
    public string firstLevelScene;
    public TextMeshPro timeInd;
    bool pIn = false;
    int pInBeat = -1;
    public bool loadFirstLevel, beatCounter;

    public GameObject enemy;
    GameObject[] enemies = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnBeat(int beat)
    {
        if (pIn)
            if (beatCounter)
            {
                pInBeat--;
            } else
            {
                foreach (GameObject g in enemies)
                {
                    if (g!=null)
                        g.SendMessage("OnBeat", beat);
                }
            }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (!isTutorialButton)
        {
            if (pIn)
            {
                if (beatCounter)
                {

                    timeInd.text = "Countdown:" + pInBeat;
                    if (pInBeat == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        if (loadFirstLevel)
                        {
                            FirstLevel();
                        }
                        else
                        {
                            container1.SetActive(!container1.activeSelf);
                            container2.SetActive(!container2.activeSelf);
                        }
                    }
                }
                else
                {
                    int enemyCount = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (enemies[i] != null)
                        {
                            enemyCount++;
                        }
                    }
                    timeInd.text = "Enemies Left: " + enemyCount;
                    if (enemyCount == 0)
                    {
                        this.GetComponent<SpriteRenderer>().color = Color.red;
                        if (loadFirstLevel)
                        {
                            FirstLevel();
                        }
                        else
                        {
                            container1.SetActive(!container1.activeSelf);
                            container2.SetActive(!container2.activeSelf);
                        }
                    }
                }
            }
            else
            {
                timeInd.text = "";
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (isTutorialButton)
            {
                tutorial = !tutorial;
                timeInd.text = "Tutorial " + (tutorial ? "ON" : "OFF");
            }
            else
            {
                if (!beatCounter)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        enemies[i - 1] = Instantiate(enemy, new Vector3(10 * Mathf.Pow(-1, i), 5 * i * Mathf.Pow(-1, i + i / 2 - 2 * (i / 4)), 40), this.transform.rotation);
                        enemies[i - 1].GetComponent<EnemyAI>().Setup(this.gameObject, this.gameObject, this.gameObject);
                    }
                }
                pIn = true;
                pInBeat = beatsToSwitch;
                GetComponent<Animator>().SetBool("PlayerIn", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pIn = false;
            pInBeat = -1;
            if (!beatCounter)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Destroy(enemies[i - 1]);
                }
            }
            GetComponent<Animator>().SetBool("PlayerIn", false);
        }
    }
    public static bool isGauntlet = false;
    public void FirstLevel()
    {
        string sceneToGo = firstLevelScene;
        if (!SceneManager.GetActiveScene().name.Contains("tutorial"))
        {
            isGauntlet = container1.activeSelf;
        }
        print("ISG " + isGauntlet);
        if (!SceneManager.GetActiveScene().name.Contains("tutorial") && tutorial)
        {
            sceneToGo = tutorialNames[sceneToGo];
        }
        ChangeScene(sceneToGo);

    }
    public void ChangeScene(string sceneName)
    {
        string oldScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        LiveOnLoad.name = sceneName;

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        print("SceneName "+ sceneName);
        print("oldScene " + oldScene);
        //SceneManager.UnloadSceneAsync(oldScene);
    }
}