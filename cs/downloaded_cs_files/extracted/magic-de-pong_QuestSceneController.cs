using UnityEngine;
using System.Collections;

public class QuestSceneController : MonoBehaviour
{
    #region enum

    #endregion

    #region const

    #endregion

    #region public property

    #endregion

    #region private property

    #endregion

    #region public method

    public void OnTopPressed()
    {
        Application.LoadLevel(Scenes.Top);
    }

    #endregion

    #region private method

    IEnumerator WaitAndDamaged()
    {
        yield return new WaitForSeconds(3);

//        EnemyController.Instance.OnDamaged(1);
    }

    #endregion

    #region event

    void Start()
    {
        EnemyGenerator.Instance.Initialize();
        EnemyGenerator.Instance.AppearNext();
        GameManager.Instance().Initialize();
        SoundManager.Instance.PlayQuestBGM();

//        StartCoroutine(WaitAndDamaged());
    }

    void Update()
    {
        GameManager.Instance().Update(Time.deltaTime);
    }

    #endregion
}
