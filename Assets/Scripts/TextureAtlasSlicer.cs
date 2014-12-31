using UnityEditor;
using UnityEngine;

public class TextureAtlasSlicer : EditorWindow {
	public TextureImporter importer;
	public SpriteAlignment spriteAlignment = SpriteAlignment.Center;
	public Vector2 customOffset = new Vector2(0.5f, 0.5f);

	[SerializeField] private TextAsset xmlAsset;

	[MenuItem("CONTEXT/TextureImporter/Slice Sprite Using XML")]
	public static void SliceUsingXML (MenuCommand command) {
		TextureImporter textureImporter = command.context as TextureImporter;
		TextureAtlasSlicer window = ScriptableObject.CreateInstance<TextureAtlasSlicer>();

		window.importer = textureImporter;
		window.ShowUtility();
	}

	[MenuItem("CONTEXT/TextureImporter/Slice Sprite Using XML", true)]
	public static bool ValidateSliceUsingXML (MenuCommand command) {
		TextureImporter textureImporter = command.context as TextureImporter;

		// Valid only if texture type is "sprite" or "advanced"
		return textureImporter &&
			(textureImporter.textureType == TextureImporterType.Sprite ||
			 textureImporter.textureType == TextureImporterType.Advanced);
	}

	public TextureAtlasSlicer () {
		title = "Texture Atlas Slicer";
	}

	public void OnGUI () {
		xmlAsset = EditorGUILayout.ObjectField("XML Source", xmlAsset, typeof(TextAsset), false) as TextAsset;
		spriteAlignment = (SpriteAlignment) EditorGUILayout.EnumPopup("Pivot", spriteAlignment);

		bool enabled = GUI.enabled;
		if (spriteAlignment != SpriteAlignment.Custom) {
			enabled = false;
		}

		EditorGUILayout.Vector2Field("Custom Offset", customOffset);

		GUI.enabled = enabled;

		if (xmlAsset == null) {
			GUI.enabled = false;
		}

		if (GUILayout.Button("Slice")) {
			// PerformSlice()
		}

		GUI.enabled = enabled;
	}
}
