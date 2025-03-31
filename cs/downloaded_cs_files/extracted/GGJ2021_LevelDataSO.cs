using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelDataSO : ScriptableObject
{
    public UnityEvent onLeftIndexUpdated, onRightIndexUpdated, onCorrectMatch,
                      onWrongMatch, onStorageUpdate, onNewCombinationAdded;
    [SerializeField]
    public List<Left> leftList;
    [SerializeField]
    public List<Right> rightList;
    public int leftIndex, rightIndex = 0;
    public Left[] storaged;


    public void Setup()
    {
        leftList = new List<Left>();
        rightList = new List<Right>();
        storaged = new Left[2];
        leftIndex = rightIndex = 0;
    }

    public void Randomize()
    {
        leftList.Shuffle();
        rightList.Shuffle();
    }

    public bool AddCombination(AnimalSO leftAnimal, AnimalSO rightAnimal, ItemSO item)
    {

        if (leftAnimal.Equals(rightAnimal))
        {
            return false;
        }

        AnimationClip animLeft = leftAnimal.getRandomAnimation();
        TraitSO trait = rightAnimal.getRandomTrait();

        Left objectLeft = new Left(animLeft, item, trait);

        //Creation object right/receiver
        AnimationClip animRight = rightAnimal.getRandomAnimation();
        Right objectRight = new Right(animRight, item, rightAnimal);

        //Check that no other same animal is in right list with same item
        if (rightList.IndexOf(objectRight) >= 0) return false;


        if (leftList == null)
        {
            leftList = new List<Left>();
        }
        else if (leftList.Count == 0)
        {
            leftList.Add(objectLeft);
            leftIndex = 0;
        }
        else
        {
            AddToLeft(objectLeft);
        }

        if (rightList == null)
        {
            rightList = new List<Right>();
        }
        else if (rightList.Count == 0)
        {
            rightList.Add(objectRight);
            rightIndex = 0;
        }
        else
        {
            AddToRight(objectRight);
        }
        onNewCombinationAdded.Invoke();
        return true;

    }

    public void AddCombinationWithoutChecking(AnimalSO leftAnimal, AnimalSO rightAnimal, ItemSO item)
    {

        Debug.Log("Adding animals without checking");
        AnimationClip animLeft = leftAnimal.getRandomAnimation();
        TraitSO trait = rightAnimal.getRandomTrait();

        Left objectLeft = new Left(animLeft, item, trait);

        //Creation object right/receiver
        AnimationClip animRight = rightAnimal.getRandomAnimation();

        Right objectRight = new Right(animRight, item, rightAnimal);

        AddToLeft(objectLeft);
        AddToRight(objectRight);
        onNewCombinationAdded.Invoke();

    }

    private void AddToLeft(Left newLeft)
    {
        //Addition to lists, index random
        if (leftList == null)
        {
            leftList = new List<Left>();
        }
        else if (leftList.Count == 0)
        {
            leftList.Add(newLeft);
        }
        else
        {
            int randomIndex = Random.Range(0, leftList.Count - 1);
            leftList.Insert(randomIndex, newLeft);
            if (leftIndex >= randomIndex)
            {
                RotateLeft(1);
            }
        }
        onLeftIndexUpdated.Invoke();
    }

    private void AddToRight(Right newRight)
    {
        //Addition to lists, index random
        if (rightList.Count == 0)
        {
            rightList.Add(newRight);
        }
        else
        {
            int randomIndex = Random.Range(0, rightList.Count - 1);
            rightList.Insert(Random.Range(0, rightList.Count - 1), newRight);
            if (rightIndex >= randomIndex)
            {
                RotateRight(1);
            }
        }
        onRightIndexUpdated.Invoke();
    }

    public void RotateLeft(int amountToRotate)
    {
        if (amountToRotate == 0) return;

        // Update index depending on value (positive or negative)
        leftIndex = mod(leftIndex + amountToRotate, leftList.Count);

        //Raise event onLeftIndexUpdated
        onLeftIndexUpdated.Invoke();

    }
    public void RotateRight(int amountToRotate)
    {
        if (amountToRotate == 0) return;

        rightIndex = mod(rightIndex + amountToRotate, rightList.Count);

        //Raise event onRightIndexUpdated
        onRightIndexUpdated.Invoke();
    }

    public bool Match(int storageIndex)
    {
        if (storaged == null)
        {
            storaged = new Left[2];
        }
        TraitSO traitLeft = storaged[storageIndex].trait;
        ItemSO itemLeft = storaged[storageIndex].item;

        List<TraitSO> traitsRight = rightList[rightIndex].animal.traits;
        ItemSO itemRight = rightList[rightIndex].item;
        bool conditionMatch = false;

        //If item isnt equal Wrong Match
        if (!itemLeft.Equals(itemRight))//Item not correct
        {
            conditionMatch = false;
        }
        else//Item correct
        {
            for (int count = 0; count < traitsRight.Count; count++)
            {
                if (traitsRight[count] == traitLeft)//Trait found in rightAnimal
                {
                    conditionMatch = true;
                }
            }
        }
        //Trait not found in rightAnimal
        if (conditionMatch)
        {
            onCorrectMatch.Invoke();
        }
        else
        {
            onWrongMatch.Invoke();
        }

        //Delete left and right
        RemoveFromStorage(storageIndex);
        RemoveFromRigth(rightList[rightIndex]);
        return conditionMatch;
    }

    public void StorageAction(int storageIndex)
    {
        // If the storage index is full, match
        // if not, storage current left instance and update shit
        // Invoke event updating storage graphics 
        if (storaged[storageIndex] != null)
        {
            Match(storageIndex);
        }
        else
        {
            storaged[storageIndex] = leftList[leftIndex];
            RemoveFromLeft(leftList[leftIndex]);
        }
        onStorageUpdate.Invoke();

    }

    public int mod(int x, int m)
    {
        if (m == 0) return x;
        return (x % m + m) % m;
    }


    public void RemoveFromStorage(int indexStorage)
    {
        storaged[indexStorage] = null;
    }
    public void RemoveFromLeft(Left leftToRemove)
    {
        leftList.Remove(leftToRemove);
        if (leftIndex == leftList.Count)
        {
            RotateLeft(-1);
        }
        onLeftIndexUpdated.Invoke();
    }

    public void RemoveFromRigth(Right rigthToRemove)
    {
        rightList.Remove(rigthToRemove);
        if (rightIndex == rightList.Count)
        {
            RotateRight(-1);
        }
        onRightIndexUpdated.Invoke();
    }

    public Left getCurrentLeft()
    {
        return leftList[leftIndex];
    }

    public Right getCurrentRight()
    {
        return rightList[rightIndex];
    }

    public Left getItemStoraged(int storageIndex)
    {
        return storaged[storageIndex];
    }
}
