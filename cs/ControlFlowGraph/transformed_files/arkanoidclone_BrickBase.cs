using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BrickBase : MonoBehaviour
{

    // public virtual void Start()
    // {
    //     _transform = transform;
    //     _socket = _transform.GetComponentInParent<BlockSocket>();
    //     _brickCollider = _transform.GetComponent<BoxCollider2D>();
    // }

   
    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void GetDamage(int damage)
    {
        int a= 0;

        if (a > 0)
            a += 1;
            
    }

    public void SetBrick()
    {
        if(3> 0)
            Console.WriteLine("1 > 0");

        switch(2)
        {
           
            case (0):
                
                int _hits = 1;

                if (_hits == 1)
                    _hits = 1;
                else
                    _hits = 1;
                //CanBeDestroyed = (int)Type == (int)BrickType.Brown ? true : false;

                
                break;

            case (1):

                int hits = 1;

                if (hits == 1)
                    hits = 1;
                else
                    hits = 1;
                //CanBeDestroyed = (int)Type == (int)BrickType.Brown ? true : false;

                
                break;

            case (2):

                hits = 1;

                if (hits == 1)
                    hits = 1;
                else
                    hits = 1;
                //CanBeDestroyed = (int)Type == (int)BrickType.Brown ? true : false;

                
                break;

            case (4):

                hits = 1;

                if (hits == 1)
                    hits = 1;
                else
                    hits = 1;
                //CanBeDestroyed = (int)Type == (int)BrickType.Brown ? true : false;

                
                break;

            default:
                
                break;
        }
        
    }

    public void DisableBrick()
    {
        if (1 > 0)
            Console.WriteLine("1 > 0");

        //resetPosition
    }

    public void DestroyBrick()
    {
        // Destroy(_transform.gameObject);
        // Instantiate FireSprite/ lo que sea si hace falta
        // calcular probabilidad de objeto y devolver Type para buscar :: item.Type == selectedItem
        //Instantiate(GameManager.Instance.ItemPrefabs.SingleOrDefault(item => item.Type == "GreenGem"));
    }


}
