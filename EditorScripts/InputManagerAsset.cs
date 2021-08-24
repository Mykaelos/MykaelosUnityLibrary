#if (UNITY_EDITOR)
using UnityEditor;


// InputManagerAsset to help make programmatic changes to the project's InputManager.asset.
// Partially borrowed with major changes from http://plyoung.appspot.com/blog/manipulating-input-manager-in-script.html
// This class should mostly be used in Editor scripts and won't work at play/run time.
// InputManagerAsset was created to fix Unity's confusing defaults for Horizontal/Vertical's Gravity and Sensitivity.
public class InputManagerAsset {
    private SerializedObject InputManagerSerializedObject;


    public InputManagerAsset() {
        Load();
    }

    // Loads or reloads the InputManager.asset.
    public void Load() {
        InputManagerSerializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
    }

    // Saves any changes to the InputManager.asset.
    // DOES NOT PERMANENTLY SAVE AT RUN TIME, but will at least save until play ends.
    // Must be used in an Editor script to make permanent changes to the project.
    public void Save() {
        InputManagerSerializedObject.ApplyModifiedProperties();
    }

    // Gets the first Axis by DisplayName (Name), like Horizontal, Vertical, Fire1, etc.
    public SerializedProperty GetAxis(string axisDisplayName) {
        SerializedProperty axesArrayProperty = InputManagerSerializedObject.FindProperty("m_Axes.Array");
        return axesArrayProperty.GetChildProperty(axisDisplayName);
    }

    // Unity's Horizontal/Vertical axes defaults the Gravity and Sensitivity to 3, which causes these inputs to change slowly.
    // This can be confusing because player movement seems to lag or Input.getAxis("Horizontal") takes a while to equal 1.
    // Movement button presses should work instantly (like Fire1 and Jump), and this method fixes that.
    public static void FixMovementGravity() {
        var inputManager = new InputManagerAsset();

        inputManager.GetAxis("Horizontal").GetChildProperty("Gravity").floatValue = 1000;
        inputManager.GetAxis("Horizontal").GetChildProperty("Sensitivity").floatValue = 1000;

        inputManager.GetAxis("Vertical").GetChildProperty("Gravity").floatValue = 1000;
        inputManager.GetAxis("Vertical").GetChildProperty("Sensitivity").floatValue = 1000;

        inputManager.Save();
    }
}

public static class SerializedPropertyExtension {


    // Gets the first child property by DisplayName.
    // DisplayName is the name displayed by the Unity interface (like "Horizontal" in the InputManager).
    // To see the property's actual name and path, shift + right click the property and select "Print Property Path".
    public static SerializedProperty GetChildProperty(this SerializedProperty parent, string displayName) {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        child.Next(true);

        do {
            if (child.displayName.Equals(displayName)) {
                return child;
            }
        } while (child.Next(false));

        return null;
    }
}
#endif
