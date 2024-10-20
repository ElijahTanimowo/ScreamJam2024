using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField] Material GLDraw;
    public Transform player;
    float camHeight, camWidth;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        cam.aspect = 4.0f / 3.0f;
        camHeight = 2 * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    private void OnPostRender()
    {
        float cameraLeft = transform.position.x - camWidth / 2;
        float cameraBottom = transform.position.y - camHeight / 2;

        GL.PushMatrix();
        GLDraw.SetPass(0);
        GL.LoadOrtho();

        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, new Vector2(camWidth, camHeight), 0, LayerMask.GetMask("Wall"));
        foreach (Collider2D collision in collisions)
        {
            Wall wall = collision.GetComponent<Wall>();
            float left = (wall.left - cameraLeft) / camWidth;
            float top = (wall.top - cameraBottom) / camHeight;
            float right = (wall.right - cameraLeft) / camWidth;
            float bottom = (wall.bottom - cameraBottom) / camHeight;

            if (player.transform.position.x <= wall.centerX && player.transform.position.y <= wall.centerY)
                DrawShadow(left, bottom, right, top);
            if (player.transform.position.x <= wall.centerX && player.transform.position.y >= wall.centerY)
                DrawShadow(left, top, right, bottom);
            if (player.transform.position.x >= wall.centerX && player.transform.position.y >= wall.centerY)
                DrawShadow(right, top, left, bottom);
            if (player.transform.position.x >= wall.centerX && player.transform.position.y <= wall.centerY)
                DrawShadow(right, bottom, left, top);
        }

        GL.PopMatrix();
    }

    void DrawShadow(float x1, float y1, float x2, float y2)
    {

        float x = 0.5f, y = 0.5f;
        int projected_length = 100;
        float projx1 = x2 + (x2 - x) * projected_length;
        float projy1 = y1 + (y1 - y) * projected_length;
        float projx2 = x1 + (x1 - x) * projected_length;
        float projy2 = y2 + (y2 - y) * projected_length;

        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.black);

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(x2, y1, 0));
        GL.Vertex(new Vector3(projx1, projy1, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(projx2, projy1, 0));
        GL.Vertex(new Vector3(projx1, projy1, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(x1, y2, 0));
        GL.Vertex(new Vector3(projx2, projy2, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(projx2, projy1, 0));
        GL.Vertex(new Vector3(projx2, projy2, 0));

        GL.End();
    }
}
