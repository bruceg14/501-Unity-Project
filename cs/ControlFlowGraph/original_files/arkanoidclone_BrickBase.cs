using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BrickBase : MonoBehaviour, IGolpeable
{
    private Transform _transform;
    [SerializeField]
    private BrickType _type;
    private BlockSocket _socket;
    private bool _canBeDestroyed = false;
    private BoxCollider2D _brickCollider = null;
    private Sprite _brickSprite = null;
    [SerializeField]
    private int _hits = 1;
    private int _hitCount = 0;
    public BrickType Type { get => _type; set => _type = value; }
    public BlockSocket Socket { get => _socket; set => _socket = value; }
    public bool CanBeDestroyed { get => _canBeDestroyed; set => _canBeDestroyed = value; }
    public Sprite BrickSprite { get => _brickSprite; set => _brickSprite = value; }
    public int Hits { get => _hits; }

    public BrickBase(BlockSocket socket)
    {
        _socket = socket;
    }

    public virtual void Start()
    {
        _transform = transform;
        _socket = _transform.GetComponentInParent<BlockSocket>();
        _brickCollider = _transform.GetComponent<BoxCollider2D>();
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int damage)
    {
        _hitCount += damage;

        if (_hitCount >= _hits)
            DestroyBrick();
            
    }

    public void SetBrick()
    {
        if(transform.parent != null)
            transform.position = transform.parent.position;

        switch((int)Type)
        {
           
            case (0):
                
                _hits = 1;

                if ((int)Type != (int)BrickType.Brown)
                    CanBeDestroyed = true;
                else
                    CanBeDestroyed = false;
                //CanBeDestroyed = (int)Type == (int)BrickType.Brown ? true : false;

                _socket.CurrentBrickType = (int)Type;

                break;

            case (1):

                _hits = 2;

                if ((int)Type != (int)BrickType.Brown)
                    CanBeDestroyed = true;
                else
                    CanBeDestroyed = false;

                _socket.CurrentBrickType = (int)Type;

                break;

            case (2):

                _hits = 3;

                if ((int)Type != (int)BrickType.Brown)
                    CanBeDestroyed = true;
                else
                    CanBeDestroyed = false;

                _socket.CurrentBrickType = (int)Type;

                break;

            case (3):
                 
                _hits = -1;

                if ((int)Type != (int)BrickType.Brown)
                    CanBeDestroyed = true;
                else
                    CanBeDestroyed = false;

                _socket.CurrentBrickType = (int)Type;
                
                break;

            default:
                _hits = 1;
                break;
        }
        
    }

    public void DisableBrick()
    {
        if (_hitCount >= _hits)
            _transform.gameObject.SetActive(false);

        //resetPosition
    }

    public void DestroyBrick()
    {
        Destroy(_transform.gameObject);
        // Instantiate FireSprite/ lo que sea si hace falta
        // calcular probabilidad de objeto y devolver Type para buscar :: item.Type == selectedItem
        //Instantiate(GameManager.Instance.ItemPrefabs.SingleOrDefault(item => item.Type == "GreenGem"));
    }


}
