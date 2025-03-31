using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ActionListEditor : EditorWindow {

	public ActionList inventoryItemList;
	private int viewIndex = 1;
	private float secondsToDelay = 0.0f;

	[MenuItem ("Window/Action List Editor %#e")]
	static void  Init () 
	{
		EditorWindow.GetWindow (typeof (ActionListEditor));
	}

	void  OnEnable () {
		if(EditorPrefs.HasKey("ObjectPath")) 
		{
			string objectPath = EditorPrefs.GetString("ObjectPath");
			inventoryItemList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(ActionList)) as ActionList;
		}

	}

	void  OnGUI () {
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Action List Editor", EditorStyles.boldLabel);
		if (inventoryItemList != null) {
			if (GUILayout.Button("Show Action List")) 
			{
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = inventoryItemList;
			}
		}
		if (GUILayout.Button("Open Action List")) 
		{
			OpenItemList();
		}
		if (GUILayout.Button("New Action List")) 
		{
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = inventoryItemList;
		}
		GUILayout.EndHorizontal ();


		if (inventoryItemList == null) 
		{
			GUILayout.BeginHorizontal ();
			GUILayout.Space(10);
			if (GUILayout.Button("Create New Action List", GUILayout.ExpandWidth(false))) 
			{
				CreateNewItemList();
			}
			if (GUILayout.Button("Open Existing Action List", GUILayout.ExpandWidth(false))) 
			{
				OpenItemList();
			}
			GUILayout.EndHorizontal ();
		}

		GUILayout.Space(20);

		if (inventoryItemList != null) 
		{
			GUILayout.BeginHorizontal ();

			GUILayout.Space(10);

			if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
			{
				if (viewIndex > 1)
					viewIndex --;
			}
			GUILayout.Space(5);
			if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
			{
				if (viewIndex < inventoryItemList.Actions.Count) 
				{
					viewIndex ++;
				}
			}

			GUILayout.Space(60);

			if (GUILayout.Button("Add Action", GUILayout.ExpandWidth(false))) 
			{
				AddItem();
			}
			if (GUILayout.Button("Delete Action", GUILayout.ExpandWidth(false))) 
			{
				DeleteItem(viewIndex - 1);
			}

			if (GUILayout.Button("Add Action In The Next Position", GUILayout.ExpandWidth(false))) 
			{
				AddActionAtPosition(viewIndex - 1);
			}

			GUILayout.EndHorizontal ();
			if (inventoryItemList.Actions == null)
				Debug.Log("wtf");
			if (inventoryItemList.Actions.Count > 0) 
			{
				GUILayout.BeginHorizontal ();
				secondsToDelay = EditorGUILayout.FloatField ("Delay X Seconds All Actions", secondsToDelay, GUILayout.ExpandWidth(false));
				if (GUILayout.Button("Add X Secs to all Actions from here", GUILayout.ExpandWidth(false))) 
				{
					AddSeconds(viewIndex - 1);
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.Actions.Count);
				//Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
				EditorGUILayout.LabelField ("of   " +  inventoryItemList.Actions.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
				GUILayout.EndHorizontal ();
				inventoryItemList.Actions[viewIndex-1].Name = EditorGUILayout.TextField("Name", inventoryItemList.Actions[viewIndex-1].Name, GUILayout.ExpandWidth(false));
				inventoryItemList.Actions[viewIndex-1].TypeOfAction = (ActionType) EditorGUILayout.EnumPopup ("Type of Action", inventoryItemList.Actions[viewIndex-1].TypeOfAction, GUILayout.ExpandWidth(false));
				inventoryItemList.Actions[viewIndex-1].ExecuteTime = EditorGUILayout.FloatField("Execute Time", inventoryItemList.Actions[viewIndex-1].ExecuteTime, GUILayout.ExpandWidth(false));
				inventoryItemList.Actions[viewIndex-1].Actor = (ActorType) EditorGUILayout.EnumPopup ("Actor", inventoryItemList.Actions[viewIndex-1].Actor, GUILayout.ExpandWidth(false));
				if(inventoryItemList.Actions[viewIndex-1].TypeOfAction == ActionType.Move){
					inventoryItemList.Actions[viewIndex-1].EffectPosition = EditorGUILayout.ObjectField ("Start Position", inventoryItemList.Actions[viewIndex-1].EffectPosition, typeof (Transform), false) as Transform;
					inventoryItemList.Actions[viewIndex-1].EndPosition = EditorGUILayout.ObjectField ("End Position", inventoryItemList.Actions[viewIndex-1].EndPosition, typeof (Transform), false) as Transform;
					inventoryItemList.Actions[viewIndex-1].Movement = EditorGUILayout.Vector3Field ("Movement", inventoryItemList.Actions[viewIndex-1].Movement, GUILayout.ExpandWidth(false));
					inventoryItemList.Actions[viewIndex-1].MoveFirst = (bool)EditorGUILayout.Toggle("Move First", inventoryItemList.Actions[viewIndex-1].MoveFirst, GUILayout.ExpandWidth(false));
				}

				if(inventoryItemList.Actions[viewIndex-1].TypeOfAction == ActionType.Rotate){
					inventoryItemList.Actions[viewIndex-1].RotationDegrees = EditorGUILayout.Vector3Field ("Rotation Degrees", inventoryItemList.Actions[viewIndex-1].RotationDegrees, GUILayout.ExpandWidth(false));
				}

				if(inventoryItemList.Actions[viewIndex-1].TypeOfAction == ActionType.Build){
					inventoryItemList.Actions[viewIndex-1].TypeOfBuild = (BuildType) EditorGUILayout.EnumPopup ("Type of Build", inventoryItemList.Actions[viewIndex-1].TypeOfBuild, GUILayout.ExpandWidth(false));
				}
				if(inventoryItemList.Actions[viewIndex-1].TypeOfAction == ActionType.Curtine){
					inventoryItemList.Actions[viewIndex-1].ActionsOfCurtine = (CurtineActions) EditorGUILayout.EnumPopup ("Action of the Curtine", inventoryItemList.Actions[viewIndex-1].ActionsOfCurtine, GUILayout.ExpandWidth(false));
				}
				GUILayout.Space(10);

				GUILayout.BeginHorizontal ();
				inventoryItemList.Actions[viewIndex-1].IsPlayable = (bool)EditorGUILayout.Toggle("Is Playable", inventoryItemList.Actions[viewIndex-1].IsPlayable, GUILayout.ExpandWidth(false));
				if(inventoryItemList.Actions[viewIndex-1].IsPlayable){
					inventoryItemList.Actions[viewIndex-1].UIActionList = EditorGUILayout.ObjectField ("Interface Action Sequence", inventoryItemList.Actions[viewIndex-1].UIActionList, typeof (UI.ActionList), false) as UI.ActionList;
				}
				GUILayout.EndHorizontal ();

				GUILayout.Space(10);

				GUILayout.BeginHorizontal ();
				if(inventoryItemList.Actions[viewIndex-1].TypeOfAction == ActionType.PlayAnimation){
					inventoryItemList.Actions[viewIndex-1].TypeOfAnimation = (AnimationType) EditorGUILayout.EnumPopup ("Type of Animation", inventoryItemList.Actions[viewIndex-1].TypeOfAnimation, GUILayout.ExpandWidth(false));
				}
				if(inventoryItemList.Actions[viewIndex-1].TypeOfAction == ActionType.PlaySound){
					inventoryItemList.Actions[viewIndex-1].TypeOfSound = (SoundType) EditorGUILayout.EnumPopup ("Type of Sound", inventoryItemList.Actions[viewIndex-1].TypeOfSound, GUILayout.ExpandWidth(false));
				}
				GUILayout.EndHorizontal ();
				if (GUILayout.Button("Play From Here", GUILayout.ExpandWidth(false))) 
				{
					PlayFromHere(viewIndex - 1);
				}
				GUILayout.Space(10);

			} 
			else 
			{
				GUILayout.Label ("This Inventory List is Empty.");
			}
		}
		if (GUI.changed) 
		{
			EditorUtility.SetDirty(inventoryItemList);
		}
	}

	void CreateNewItemList () 
	{
		// There is no overwrite protection here!
		// There is No "Are you sure you want to overwrite your existing object?" if it exists.
		// This should probably get a string from the user to create a new name and pass it ...
		viewIndex = 1;
		inventoryItemList = CreateActionList.Create();
		if (inventoryItemList) 
		{
			inventoryItemList.Actions = new List<Action>();
			string relPath = AssetDatabase.GetAssetPath(inventoryItemList);
			EditorPrefs.SetString("ObjectPath", relPath);
		}
	}

	void OpenItemList () 
	{
		string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
		if (absPath.StartsWith(Application.dataPath)) 
		{
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			inventoryItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(ActionList)) as ActionList;
			if (inventoryItemList.Actions == null)
				inventoryItemList.Actions = new List<Action>();
			if (inventoryItemList) {
				EditorPrefs.SetString("ObjectPath", relPath);
			}
		}
	}

	void AddItem () 
	{
		Action newItem = new Action();
		newItem.Name = "Test";
		inventoryItemList.Actions.Add (newItem);
		viewIndex = inventoryItemList.Actions.Count;
	}

	void AddActionAtPosition(int index){
		Action action = new Action();
		action.Name = "Action " + (index + 2);
		action.ExecuteTime = inventoryItemList.Actions[index].ExecuteTime;
		action.Actor = inventoryItemList.Actions[index].Actor;
		inventoryItemList.Actions.Insert(index + 1, action);
		viewIndex = index + 2;
	}

	void AddSeconds(int index){
		for (int i = index + 1; i < inventoryItemList.Actions.Count; i++) {
			inventoryItemList.Actions[i].ExecuteTime += secondsToDelay;
		}
	}

	void DeleteItem (int index) 
	{
		inventoryItemList.Actions.RemoveAt (index);
	}

	void PlayFromHere(int index){
		GameControlManager.Instance.TimeToInit = inventoryItemList.Actions[index].ExecuteTime;
		GameControlManager.Instance.indexAction = index;

	}

}
