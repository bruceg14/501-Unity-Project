using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UIManager : MonoBehaviour
{
  private static UIManager _instance;
  public static UIManager Instance
  {
    get
    {
      if(_instance == null)
      {
        Debug.LogError("The UI Manager is NULL");
      }

      return _instance;
    }
  }
  public List<GameObject> listObjects;
  public int couter = 0;
  public Case activeCase;
  public ClientInfoPanel clientInfoPanel;
  public GameObject borderPanel;
  public LocationPanel LocationPanel;
  public TakePhotoPanel TakePhotoPanel;
  public OverviewPanel OverviewPanel;
  private void Awake()
  {
    _instance = this;
  }

  public void CreateNewCase()
  {
    activeCase = new Case();
    int caseId = Random.Range(0,1000);
    string stringCaseID = caseId.ToString();
    activeCase.CaseId = stringCaseID;

    clientInfoPanel.gameObject.SetActive(true);
    borderPanel.gameObject.SetActive(true);
  }
  public void SubmitButton()
  {
    Case awsCase = new Case();
    awsCase.CaseId = activeCase.CaseId;
    awsCase.name = activeCase.name;
    awsCase.date = activeCase.date;
    awsCase.location = activeCase.location;
    awsCase.locationNotes = activeCase.locationNotes;
    awsCase.photoTaken = activeCase.photoTaken;
    awsCase.photoNotes = activeCase.photoNotes;

    BinaryFormatter bf = new BinaryFormatter();
    string filePath = Application.persistentDataPath + "/case#"+ awsCase.CaseId + ".dat";
    FileStream file = File.Create(filePath);
    bf.Serialize(file, awsCase);
    file.Close();

    //Debug.Log(Application.persistentDataPath);
    AWSManager.Instance.UploadToS3(filePath, awsCase.CaseId);

  }
}
