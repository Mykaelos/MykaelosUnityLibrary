using UnityEngine;

namespace MykaelosUnityLibrary.Features {

    /**
     * Creates a single layer of an infinite parallax scrolling background for a static camera.
     * 
     * The goal was to make a background layer that scrolls forever without the issues of texture wrapping or juggling multiple sprite objects, while also dynamically scaling to the camera, and is extremely easy to setup. This is solved by using a tiled sprite that is tiled to twice as wide/tall as the camera view, and then moving and wrapping that sprite to simulate movement. An external script is needed to handle the effective "World Position" and to call SetWorldPosition(). DebugMove was included to quickly test this script, and provide a way to determine the right Depth to use.
     * 
     * SetWorldPosition(Vector2 position) should be called as needed (Update() or player movement) to update the parallax layer's position.
     * 
     * Set Depth from 0 to 1 to change how fast the parallax layer moves compared to the WorldPosition.
     *  0   = No movement. Layer appears static and max distance.
     *  0-1 = Partial movement. Layer appears more distant, closer to zero.
     *  1   = 100% movement. Layer appears close and moves in step with the position.
     *  >1  = Faster movement. Layer appear to be in the foreground.
     * 
     * Either Sprite or ParallaxSpriteObject should be set. If ParallaxSpriteObject exists, then it will be used as the parallax layer. Otherwise a new child GameObject will be created with the supplied Sprite.
     */
    public class InfiniteParallaxBackground : MonoBehaviour {
        [Tooltip("Parallax depth of the layer, effectively the layer speed percentage." +
            "\n  0   = No movement. Layer appears static and max distance." +
            "\n  0-1 = Partial movement. Layer appears more distant, closer to zero." +
            "\n  1   = 100% movement. Layer appears close and moves in step with the position." +
            "\n  >1  = Faster movement. Layer appear to be in the foreground.")]
        [Range(0, 1)]
        public float Depth = 1f;

        [Tooltip("Sprite used to create parallax sprite GameObject.\nRequired unless ParallaxSpriteObject is provided.")]
        public Sprite Sprite;

        [Tooltip("GameObject to be controlled for parallax effect. Must have a SpriteRenderer with a Sprite.\nOptional. If null, this GameOject will be created automatically using the Sprite parameter.")]
        public GameObject ParallaxSpriteObject;

        private Vector2 ScrollAreaSize;


        /**
         * Updates the parallax layer's position based on the WorldPosition and Depth.
         * This should be updated as often as needed (on player movement, or every Update()).
         * 
         * position: Simulated world position.
         */
        public void SetWorldPosition(Vector2 position) {
            UpdateSpriteObject(position);
        }

        public void Setup() {
            if (ParallaxSpriteObject == null) {
                ParallaxSpriteObject = CreateSpriteObject(Sprite);
            }
            var parallaxSpriteRenderer = ParallaxSpriteObject.GetComponent<SpriteRenderer>();
            var spriteSize = parallaxSpriteRenderer.sprite.bounds.size;

            var cameraViewRect = Camera.main.VisibleWorldRect();
            float width = Mathf.Ceil(cameraViewRect.size.x / spriteSize.x) * spriteSize.x;
            float height = Mathf.Ceil(cameraViewRect.size.y / spriteSize.y) * spriteSize.y;

            ScrollAreaSize = new Vector2(width, height);
            parallaxSpriteRenderer.size = ScrollAreaSize * 2f; // Make the spriteRenderer twice as large as the viewable area to allow wrapping by moving it.
        }

        private void Awake() {
            Setup();
        }

        private GameObject CreateSpriteObject(Sprite sprite) {
            var background = new GameObject("ParallaxSprite");
            background.transform.SetParent(transform);
            background.transform.position = Vector3.zero;

            var spriteRenderer = background.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.tileMode = SpriteTileMode.Continuous;

            return background;
        }

        private void UpdateSpriteObject(Vector2 position) {
            ParallaxSpriteObject.transform.position = new Vector2(
                Mathf.Repeat(position.x * Depth, ScrollAreaSize.x) - ScrollAreaSize.x / 2f,
                Mathf.Repeat(position.y * Depth, ScrollAreaSize.y) - ScrollAreaSize.y / 2f
            ) * -1; // Flip the direction.
        }

    #if UNITY_EDITOR
        #region Debug Methods
        [Header("Debug")]
        public bool DebugMove = false;
        public float DebugMoveSpeed = 10f;
        public Vector2 DebugPosition = Vector2.zero;


        private void Update() {
            if (DebugMove) {
                UpdateDebugInput();
            }
        }

        private void UpdateDebugInput() {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
                DebugPosition += Vector2.up * DebugMoveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                DebugPosition += Vector2.down * DebugMoveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
                DebugPosition += Vector2.left * DebugMoveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
                DebugPosition += Vector2.right * DebugMoveSpeed * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus)) {
                DebugMoveSpeed += 1f;
            }
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) {
                DebugMoveSpeed -= 1f;
            }

            SetWorldPosition(DebugPosition);
        }
        #endregion
    #endif
    }
}
