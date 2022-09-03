using System;
using UnityEngine;

public class CameraSizer : MonoBehaviour
{
    private Grid _grid;

    private void Start()
    {
        _grid = FindObjectOfType<Grid>();
        if (_grid != null)
        {
            RepositionCamera(_grid.Width, _grid.Height);
        }
    }

    private void RepositionCamera(float x, float y)
    {
        Vector2 tempPosition = new Vector2(Mathf.Round((x-0.01f) / 2f), Mathf.Round(y / 2));
        transform.position = new Vector3(tempPosition.x, tempPosition.y, transform.position.z);
    }
    
}