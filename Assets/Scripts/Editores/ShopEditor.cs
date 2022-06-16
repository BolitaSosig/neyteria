using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

//[CustomEditor(typeof(Shop))]
[CanEditMultipleObjects]
public class ShopEditor : Editor
{
    SerializedProperty shop;

    void OnEnable()
    {
        shop = serializedObject.FindProperty("shop");
    }

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        // Load and clone a visual tree from UXML
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editores/ShopEditor.uxml");
        visualTree.CloneTree(myInspector);

        ((ObjectField)myInspector.ElementAt(0)).objectType = typeof(Modulo);

        // Return the finished inspector UI
        return myInspector;
    }
}
