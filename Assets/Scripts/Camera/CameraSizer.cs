using UnityEngine;

public class CameraSizer : MonoBehaviour
{
    private Grid _grid;
    public float AspectRatio = 0.625f;
    public float Padding = 2;
    public GameObject UI;
    public float UIScale = 0.2f;
    private void Awake()
    {
        _grid = FindObjectOfType<Grid>();
        if (_grid != null)
        {
            _grid.OnGridInitialized += OnGridInitialized;
        }
    }

    private void OnApplicationQuit()
    {
        if (_grid != null)
        {
            _grid.OnGridInitialized -= OnGridInitialized;
        }
    }

    private void OnGridInitialized(LevelData obj)
    {
        RepositionCamera(_grid.Width-1, _grid.Height-1);
    }

    private void RepositionCamera(float x, float y)
    {
        Vector2 tempPosition = new Vector2(x/2, y/2);
        transform.position = new Vector3(tempPosition.x, tempPosition.y, transform.position.z);
        if (_grid.Width >= _grid.Height)
        {
            Camera.main.orthographicSize = (_grid.Width / 2 + Padding) / AspectRatio;
        }
        else
        {
            Camera.main.orthographicSize = _grid.Height / 2 + Padding;
        }

        if (Camera.main.orthographicSize>6)
        {
            UI.transform.localScale += Vector3.one * ((Camera.main.orthographicSize - 6) * UIScale);
            transform.position += Vector3.up * ((Camera.main.orthographicSize - 6) * UIScale);
        }
        
    }
    
}