using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * The goal of this class is to automatically draw the right wall tile based on nearby wall tiles.
 * It uses raycasts to other WallControllers to see what directional sprite to display. Just set up
 * the WallSprites List with the correct direction and sprite for each combination.
 * Up, UpRight, UpDown, UpLeft, Right, RightDown, RightLeft, Down, DownLeft, Left, All, None
 */
public class WallController : MonoBehaviour {
    public List<WallSprite> WallSprites = new List<WallSprite>();

    public WallDirection WallDirection = new WallDirection();
    private SpriteRenderer SpriteRenderer;


    void Start() {
        SpriteRenderer = this.GetComponentInChild<SpriteRenderer>("Sprite");
        WallDirection = DetermineWallDirection();
        SetWallSprite(WallDirection);
    }

    WallDirection DetermineWallDirection() {
        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, Vector2.up, 1);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1);
        RaycastHit2D hitBottom = Physics2D.Raycast(transform.position, Vector2.down, 1);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1);

        WallDirection wallDirection = new WallDirection() {
            Up = hitTop.collider != null && hitTop.collider.GetComponent<WallController>() != null,
            Right = hitRight.collider != null && hitRight.collider.GetComponent<WallController>() != null,
            Down = hitBottom.collider != null && hitBottom.collider.GetComponent<WallController>() != null,
            Left = hitLeft.collider != null && hitLeft.collider.GetComponent<WallController>() != null
        };

        return wallDirection;
    }

    void SetWallSprite(WallDirection wallDirection) {
        foreach (var wallSprite in WallSprites) {
            if (wallDirection.Equals(wallSprite.WallDirection)) {
                SpriteRenderer.sprite = wallSprite.Sprite;
                break;
            }
        }
    }

    void Update() {
        // Can comment the following back in to be able to move the blocks to watch them change (like in the editor),
        // but obviously this has a performance cost.

        //WallDirection = DetermineWallDirection();
        //SetWallSprite(WallDirection);

        // Nice way to see what is determining the wall direction.
        //WallDirection.DrawDebug(transform);
    }
}

[Serializable]
public class WallSprite {
    public Sprite Sprite;
    public WallDirection WallDirection;
}

[Serializable]
public class WallDirection {
    public bool Up;
    public bool Right;
    public bool Down;
    public bool Left;


    public void DrawDebug(Transform transform) {
        if (Up) {
            Debug.DrawRay(transform.position, Vector2.up, Color.red);
        }
        if (Right) {
            Debug.DrawRay(transform.position, Vector2.right, Color.green);
        }
        if (Down) {
            Debug.DrawRay(transform.position, Vector2.down, Color.yellow);
        }
        if (Left) {
            Debug.DrawRay(transform.position, Vector2.left, Color.blue);
        }
    }

    public override bool Equals(object obj) {
        var testItem = obj as WallDirection;

        if (testItem == null) {
            return false;
        }

        return Up == testItem.Up && Right == testItem.Right && Down == testItem.Down && Left == testItem.Left;
    }

    public override int GetHashCode() {
        unchecked {
            int hash = 17;
            hash = hash * 23 + Up.GetHashCode();
            hash = hash * 23 + Right.GetHashCode();
            hash = hash * 23 + Down.GetHashCode();
            hash = hash * 23 + Left.GetHashCode();
            return hash;
        }
    }
}
