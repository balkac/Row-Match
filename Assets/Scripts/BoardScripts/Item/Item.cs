using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public EItem ItemType;
    public GameObject _disableItem;
    public int Column;
    public int Row;
    public int TargetX;
    public int TargetY;
    private Grid _grid;
    private Vector2 _firstTouchPosition;
    private Vector2 _finalTouchPosition;
    private float _swipeAngle = 0;
    private Vector2 _tempPosition;
    public Action<Item> OnItemMoved;
    private void Start()
    {
        _grid = FindObjectOfType<Grid>();
        TargetX = (int)transform.position.x;
        TargetY = (int)transform.position.y;
        Column = TargetX;
        Row = TargetY;
    }

    private void Update()
    {
        TargetX = Column;
        TargetY = Row;

        if (Mathf.Abs(TargetX - transform.position.x) > .01f)
        {
            //Move Toward the target
            _tempPosition = new Vector2(TargetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, _tempPosition,
                0.1f);
            OnItemMoved?.Invoke(this);
        }
        else
        {
            //Directly set Position
            _tempPosition = new Vector2(TargetX, transform.position.y);
            transform.position = _tempPosition;
            _grid.AllItems[Column, Row] = gameObject;
            
        }

        if (Mathf.Abs(TargetY - transform.position.y) > .01f)
        {
            //Move Towards the target
            _tempPosition = new Vector2(transform.position.x, TargetY);
            transform.position = Vector2.Lerp(transform.position, _tempPosition, 
                0.1f);
            OnItemMoved?.Invoke(this);
        }
        else
        {
            //Directly set Position
            _tempPosition = new Vector2(transform.position.x, TargetY);
            transform.position = _tempPosition;
            _grid.AllItems[Column, Row] = gameObject;
        }
        
    }

    private void OnMouseDown()
    {
        _firstTouchPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition);
    }

    private void OnMouseUp()
    {
        _finalTouchPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(_finalTouchPosition.y - _firstTouchPosition.y,
            _finalTouchPosition.x - _firstTouchPosition.x) * 180/Mathf.PI;
        MoveItem();
    }

    private void MoveItem()
    {
        if (_swipeAngle > -45f && _swipeAngle <= 45f && Column < _grid.Width)
        {
            //Right Swipe
            GameObject otherItem = _grid.AllItems[Column + 1, Row];
            (transform.parent, otherItem.transform.parent) = (otherItem.transform.parent, transform.parent);
            otherItem.GetComponent<Item>().Column -= 1;
            Column += 1;
        }
        else if (_swipeAngle > 45f && _swipeAngle <= 135f &&  Row < _grid.Height)
        {
            //Up Swipe
            GameObject otherItem = _grid.AllItems[Column, Row+1];
            (transform.parent, otherItem.transform.parent) = (otherItem.transform.parent, transform.parent);
            otherItem.GetComponent<Item>().Row -= 1;
            Row += 1;
        }
        else if ((_swipeAngle > 135f || _swipeAngle <= -135f) && Column > 0)
        {
            //Left Swipe
            GameObject otherItem = _grid.AllItems[Column - 1, Row];
            (transform.parent, otherItem.transform.parent) = (otherItem.transform.parent, transform.parent);
            otherItem.GetComponent<Item>().Column += 1;
            Column -= 1;
        }
        else if (_swipeAngle < -45f && _swipeAngle >= -135f && Row > 0)
        {
            //Down Swipe
            GameObject otherItem = _grid.AllItems[Column, Row-1];
            (transform.parent, otherItem.transform.parent) = (otherItem.transform.parent, transform.parent);
            otherItem.GetComponent<Item>().Row += 1;
            Row -= 1;
        }
    }


    public void DisableItem()
    {
        Instantiate(_disableItem,transform.parent.position,Quaternion.identity,transform.parent);
        Destroy(this.gameObject);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}