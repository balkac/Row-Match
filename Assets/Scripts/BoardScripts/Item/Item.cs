using System;
using DG.Tweening;
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
    private bool _isMatch;
    public bool IsMatch => _isMatch;
    public Action<Item> OnItemMoved;
    private bool _canMove = true;
    private void Start()
    {
        _grid = FindObjectOfType<Grid>();
        GameManager.Instance.OnGameEnded += OnGameEnded;
        TargetX = (int)transform.position.x;
        TargetY = (int)transform.position.y;
        Column = TargetX;
        Row = TargetY;
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.OnGameEnded -= OnGameEnded;
    }

    private void OnGameEnded(bool isHighScore)
    {
        _canMove = false;
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
        }
        else
        {
            //Directly set Position
            _tempPosition = new Vector2(TargetX, transform.position.y);
            transform.position = _tempPosition;
        }

        if (Mathf.Abs(TargetY - transform.position.y) > .01f)
        {
            //Move Towards the target
            _tempPosition = new Vector2(transform.position.x, TargetY);
            transform.position = Vector2.Lerp(transform.position, _tempPosition, 
                0.1f);
        }
        else
        {
            //Directly set Position
            _tempPosition = new Vector2(transform.position.x, TargetY);
            transform.position = _tempPosition;
        }
        
    }

    private void OnMouseDown()
    {
        if (!_canMove)
        {
            return;
        }
        _firstTouchPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition);
    }

    private void OnMouseUp()
    {
        if (!_canMove)
        {
            return;
        }
        _finalTouchPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(_finalTouchPosition.y - _firstTouchPosition.y,
            _finalTouchPosition.x - _firstTouchPosition.x) * 180/Mathf.PI;
        
        if (TryMoveItem())
        {
            OnItemMoved?.Invoke(this);
        }
       
    }

    private bool TryMoveItem()
    {
        Item otherItem = null;
        if (_swipeAngle == 0)
        {
            return false;
        }
        if (_swipeAngle > -45f && _swipeAngle <= 45f && Column+1< _grid.Width)
        {
            //Right Swipe
            otherItem = _grid.AllItems[Column + 1, Row];
            if (otherItem==null)
            {
                return false;
            }
            otherItem.Column -= 1;
            Column += 1;
        }
        else if (_swipeAngle > 45f && _swipeAngle <= 135f &&  Row+1 < _grid.Height)
        {
            //Up Swipe
            otherItem = _grid.AllItems[Column, Row+1];
            if (otherItem==null)
            {
                return false;
            }
            otherItem.Row -= 1;
            Row += 1;
        }
        else if ((_swipeAngle > 135f || _swipeAngle <= -135f) && Column > 0)
        {
            //Left Swipe
            otherItem = _grid.AllItems[Column - 1, Row];
            if (otherItem==null)
            {
                return false;
            }
            otherItem.Column += 1;
            Column -= 1;
        }
        else if (_swipeAngle < -45f && _swipeAngle >= -135f && Row > 0)
        {
            //Down Swipe
            otherItem = _grid.AllItems[Column, Row-1];
            if (otherItem==null)
            {
                return false;
            }
            otherItem.Row += 1;
            Row -= 1;
        }

        if (otherItem != null)
        {
            (transform.parent, otherItem.transform.parent) = (otherItem.transform.parent, transform.parent);
            _grid.AllItems[Column, Row] = this;
            _grid.AllItems[otherItem.Column, otherItem.Row] = otherItem;
            return true;
        }

        return false;
    }

    public void DisableItem()
    {
        GameObject disableObject = Instantiate(_disableItem,transform.parent.position,Quaternion.identity,transform.parent);
        disableObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).OnComplete(
            () =>
            {
                disableObject.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            });
        _isMatch = true;
        Destroy(this.gameObject);
    }
    
}