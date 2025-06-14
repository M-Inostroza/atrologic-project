using UnityEditor;
using UnityEngine;

namespace ES3Editor
{
	public static class ES3SettingsEditor
	{
		public static void Draw(ES3SerializableSettings settings)
		{
			var style = EditorStyle.Get;

			settings.location = (ES3.Location)EditorGUILayout.EnumPopup("Location", settings.location);
			// If the location is File, show the Directory.
			if(settings.location == ES3.Location.File)
				settings.directory = (ES3.Directory)EditorGUILayout.EnumPopup("Directory", settings.directory);

			if (settings.location == ES3.Location.Cache)
			{
                EditorGUILayout.BeginVertical(style.area);
                EditorGUILayout.LabelField("Store cached data:");

                EditorGUILayout.BeginVertical(style.area);
                settings.storeCacheAtEndOfEveryFrame = EditorGUILayout.Toggle("At end of every frame", settings.storeCacheAtEndOfEveryFrame);
				settings.storeCacheOnApplicationQuit = EditorGUILayout.Toggle("On Application Quit", settings.storeCacheOnApplicationQuit);
				settings.storeCacheOnApplicationPause = EditorGUILayout.Toggle("On Application Pause", settings.storeCacheOnApplicationPause);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

        settings.path = EditorGUILayout.TextField("Default File Path", settings.path);

			EditorGUILayout.Space();

			settings.encryptionType = (ES3.EncryptionType)EditorGUILayout.EnumPopup("Encryption", settings.encryptionType);
			settings.encryptionPassword = EditorGUILayout.TextField("Encryption Password", settings.encryptionPassword);

            EditorGUILayout.Space();

            settings.compressionType = (ES3.CompressionType)EditorGUILayout.EnumPopup("Compression", settings.compressionType);

            EditorGUILayout.Space();
			
			settings.saveChildren = EditorGUILayout.Toggle("Save GameObject Children", settings.saveChildren);
			
			EditorGUILayout.Space();

			if(settings.showAdvancedSettings = EditorGUILayout.Foldout(settings.showAdvancedSettings, "Advanced Settings"))
			{
				EditorGUILayout.BeginVertical(style.area);

				settings.format = (ES3.Format)EditorGUILayout.EnumPopup("Format", settings.format);
                if (settings.format == ES3.Format.JSON)
                    settings.prettyPrint = EditorGUILayout.Toggle(new GUIContent("Pretty print JSON"), settings.prettyPrint);
				settings.bufferSize = EditorGUILayout.IntField("Buffer Size", settings.bufferSize);
				settings.memberReferenceMode = (ES3.ReferenceMode)EditorGUILayout.EnumPopup("Serialise Unity Object fields", settings.memberReferenceMode);
                settings.serializationDepthLimit = EditorGUILayout.IntField("Serialisation Depth", settings.serializationDepthLimit);
                settings.postprocessRawCachedData = EditorGUILayout.Toggle(new GUIContent("Postprocess raw cached data"), settings.postprocessRawCachedData);

                EditorGUILayout.Space();

				EditorGUILayout.EndVertical();
			}
		}
    }
}
