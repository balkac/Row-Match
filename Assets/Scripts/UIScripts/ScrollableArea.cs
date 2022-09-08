using UnityEngine;

public class ScrollableArea : MonoBehaviour
{
    public Transform ScroolMenu;
    public LevelSectionManager LevelSectionManager;
    public float SwipeSpeed = 20f;
    private Vector2 _firstTouchPosition;
    private Vector2 _currentTouchPosition;
    private Vector2 _tempPosition;
    private Vector2 _scrollMenuFirstPosition;
    private bool _canMove;
    private Camera _camera;
    private float _scrollLimit;

    private void Awake()
    {
        LevelSectionManager.OnSectionsAdded += OnSectionsAdded;
    }

    private void OnSectionsAdded(int sectionWidgetCount)
    {
        _scrollLimit = sectionWidgetCount * 1f;
    }

    private void OnDestroy()
    {
        LevelSectionManager.OnSectionsAdded -= OnSectionsAdded;
    }

    private void Start()
    {
        _scrollMenuFirstPosition = ScroolMenu.transform.position;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_canMove)
        {
            _currentTouchPosition = _camera.ScreenToWorldPoint(
                Input.mousePosition);
            if (_currentTouchPosition.y < _firstTouchPosition.y)
            {
                Vector2 targetPos = new Vector2(ScroolMenu.position.x,
                    (ScroolMenu.position.y - Time.deltaTime * SwipeSpeed));
                if (targetPos.y > _scrollMenuFirstPosition.y)
                {
                    ScroolMenu.position = new Vector3(ScroolMenu.position.x,
                        targetPos.y,ScroolMenu.position.z);
                }
            }
            else if (_currentTouchPosition.y > _firstTouchPosition.y)
            {
                Vector2 targetPos = new Vector2(ScroolMenu.position.x,
                    (ScroolMenu.position.y + Time.deltaTime * SwipeSpeed));
                if (targetPos.y < _scrollMenuFirstPosition.y + _scrollLimit)
                {
                    ScroolMenu.position = new Vector3(ScroolMenu.position.x,
                        targetPos.y,ScroolMenu.position.z);
                }
            }

            _firstTouchPosition = _currentTouchPosition;
        }
    }

    private void OnMouseDown()
    {
        _firstTouchPosition = _camera.ScreenToWorldPoint(
            Input.mousePosition);
        _canMove = true;
    }

    private void OnMouseUp()
    {
        _canMove = false;
    }
    
}