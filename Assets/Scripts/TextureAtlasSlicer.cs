using System;
using System.Collections.Generic;
using System.Xml;
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
//			PerformSlice()
		}

		GUI.enabled = enabled;
	}

	private void PerformSlice () {
		XmlDocument document = new XmlDocument();
		document.LoadXml(xmlAsset.text);

		XmlElement root = document.DocumentElement;

		if (root.Name == "TextureAtlas") {
			Texture2D texture = AssetDatabase.LoadMainAssetAtPath(importer.assetPath) as Texture2D;
			int textureHeight = texture.height;
			bool failed = false;
			List<SpriteMetaData> metaDataList = new List<SpriteMetaData>();

			foreach (XmlNode childNode in root.ChildNodes) {
				if (childNode.Name == "SubTexture") {
					try {
						int width = Convert.ToInt32(childNode.Attributes["width"].Value);
						int height = Convert.ToInt32(childNode.Attributes["height"].Value);
						int x = Convert.ToInt32(childNode.Attributes["x"].Value);
						int y = textureHeight - (height + Convert.ToInt32(childNode.Attributes["y"].Value));

						SpriteMetaData spriteMetaData = new SpriteMetaData {
							alignment = (int) spriteAlignment,
							border = new Vector4(),
							name = childNode.Attributes["name"].Value,
//							pivot = GetPivotValue(spriteAlignment, customOffset),
							rect = new Rect(x, y, width, height)
						};

						metaDataList.Add(spriteMetaData);
					} catch (Exception exception) {
						failed = true;
						Debug.LogException(exception);
					}
				}
			}

			if (!failed) {
				importer.spriteImportMode = SpriteImportMode.Multiple;
				importer.spritesheet = metaDataList.ToArray();

				EditorUtility.SetDirty(importer);

				try {
					AssetDatabase.StartAssetEditing();
					AssetDatabase.ImportAsset(importer.assetPath);
				} finally {
					AssetDatabase.StopAssetEditing();
					Close();
				}
			}
		} else {
			Debug.LogError("XML needs to have a 'TextureAtlas' root node!");
		}
	}
}
