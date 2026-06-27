using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class DevToolsReleaseExporter
{
	private const string DevToolsAssetPath = "Assets/_ExternalAssets/DevTools";
	private const string ExperimentalAssetPath = "Assets/_ExternalAssets/DevTools/Extensions/Experimental";
	private const string TestsAssetPath = "Assets/_ExternalAssets/DevTools/Tests";

	[MenuItem("Tools/DevTools/Export as .unitypackage")]
	public static void ExportAsUnityPackage()
	{
		string releasesDir = GetReleasesDirectory();
		string dateStr = DateTime.Now.ToString("yyyy-MM-dd");

		// Пакет 1: DevTools (без папок Experimental, Editor и Tests)
		string packagePath1 = Path.Combine(releasesDir, $"DevTools-{dateStr}.unitypackage");
		string[] assets1 = GetAssetsUnderPath(DevToolsAssetPath)
			.Where(path => !path.StartsWith(ExperimentalAssetPath, StringComparison.OrdinalIgnoreCase))
			.Where(path => !path.StartsWith(TestsAssetPath, StringComparison.OrdinalIgnoreCase))
			.ToArray();

		AssetDatabase.ExportPackage(assets1, packagePath1, ExportPackageOptions.Default);

		// Пакет 2: DevTools_Experimental (с Experimental, без Editor и Tests)
		string packagePath2 = Path.Combine(releasesDir, $"DevTools_Experimental-{dateStr}.unitypackage");
		string[] assets2 = GetAssetsUnderPath(DevToolsAssetPath)
			.Where(path => !path.StartsWith(TestsAssetPath, StringComparison.OrdinalIgnoreCase))
			.ToArray();

		AssetDatabase.ExportPackage(assets2, packagePath2, ExportPackageOptions.Default);

		Debug.Log($"[.unitypackage] Сборка завершена. Файлы сохранены в: {releasesDir}");
		EditorUtility.DisplayDialog("Успех", $"Пакеты успешно созданы в папке Releases/:\n\n1. DevTools-{dateStr}.unitypackage\n2. DevTools_Experimental-{dateStr}.unitypackage", "OK");
	}

	[MenuItem("Tools/DevTools/Export as .zip")]
	public static void ExportAsZip()
	{
		string projectRoot = Path.GetDirectoryName(Application.dataPath);
		string releasesDir = GetReleasesDirectory();
		string dateStr = DateTime.Now.ToString("yyyy-MM-dd");

		string tempDir = Path.Combine(projectRoot, "Temp_ReleaseBuild");

		Action cleanTemp = () =>
		{
			if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
		};

		// --- Архив 1: DevTools (Стабильный, без Experimental, Editor и Tests) ---
		cleanTemp();
		string zipPath1 = Path.Combine(releasesDir, $"DevTools-{dateStr}.zip");
		if (File.Exists(zipPath1)) File.Delete(zipPath1);

		string tempDevToolsDir1 = Path.Combine(tempDir, "DevTools");
		CopyDirectory(Path.Combine(Application.dataPath, "_ExternalAssets/DevTools"), tempDevToolsDir1);

		// Удаляем Experimental и его .meta
		string tempExperimental1 = Path.Combine(tempDevToolsDir1, "Extensions/Experimental");
		if (Directory.Exists(tempExperimental1)) Directory.Delete(tempExperimental1, true);
		string tempExperimentalMeta1 = Path.Combine(tempDevToolsDir1, "Extensions/Experimental.meta");
		if (File.Exists(tempExperimentalMeta1)) File.Delete(tempExperimentalMeta1);

		// Удаляем Tests и его .meta
		string tempTests1 = Path.Combine(tempDevToolsDir1, "Tests");
		if (Directory.Exists(tempTests1)) Directory.Delete(tempTests1, true);
		string tempTestsMeta1 = Path.Combine(tempDevToolsDir1, "Tests.meta");
		if (File.Exists(tempTestsMeta1)) File.Delete(tempTestsMeta1);

		// Удаляем Editor
		DeleteEditorFolders(tempDevToolsDir1);

		ZipFile.CreateFromDirectory(tempDir, zipPath1);

		// --- Архив 2: DevTools_Experimental (Экспериментальный, с сохранением Experimental, без Editor и Tests) ---
		cleanTemp();
		string zipPath2 = Path.Combine(releasesDir, $"DevTools_Experimental-{dateStr}.zip");
		if (File.Exists(zipPath2)) File.Delete(zipPath2);

		string tempDevToolsDir2 = Path.Combine(tempDir, "DevTools");
		CopyDirectory(Path.Combine(Application.dataPath, "_ExternalAssets/DevTools"), tempDevToolsDir2);

		// Папку Experimental НЕ удаляем! Она остается внутри Extensions.

		// Удаляем Tests и его .meta
		string tempTests2 = Path.Combine(tempDevToolsDir2, "Tests");
		if (Directory.Exists(tempTests2)) Directory.Delete(tempTests2, true);
		string tempTestsMeta2 = Path.Combine(tempDevToolsDir2, "Tests.meta");
		if (File.Exists(tempTestsMeta2)) File.Delete(tempTestsMeta2);

		// Удаляем Editor
		DeleteEditorFolders(tempDevToolsDir2);

		ZipFile.CreateFromDirectory(tempDir, zipPath2);

		cleanTemp();

		Debug.Log($"[.zip] Архивация завершена. Файлы сохранены в: {releasesDir}");
		EditorUtility.DisplayDialog("Успех", $"ZIP архивы успешно созданы в папке Releases/:\n\n1. DevTools-{dateStr}.zip\n2. DevTools_Experimental-{dateStr}.zip", "OK");
	}

	private static string GetReleasesDirectory()
	{
		string projectRoot = Path.GetDirectoryName(Application.dataPath);
		string releasesDir = Path.Combine(projectRoot, "Releases");
		if (!Directory.Exists(releasesDir))
		{
			Directory.CreateDirectory(releasesDir);
		}
		return releasesDir;
	}

	private static string[] GetAssetsUnderPath(string rootPath)
	{
		if (!Directory.Exists(rootPath)) return Array.Empty<string>();

		return Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories)
			.Select(p => p.Replace('\\', '/'))
			.Where(p => !p.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
			.Where(p => !IsInsideEditorFolder(p))
			.ToArray();
	}

	private static bool IsInsideEditorFolder(string path)
	{
		string[] segments = path.Split('/');
		return segments.Any(s => s.Equals("Editor", StringComparison.OrdinalIgnoreCase));
	}

	private static void DeleteEditorFolders(string rootDir)
	{
		if (!Directory.Exists(rootDir)) return;

		var editorDirs = Directory.GetDirectories(rootDir, "Editor", SearchOption.AllDirectories);
		foreach (var dir in editorDirs)
		{
			if (Directory.Exists(dir))
			{
				Directory.Delete(dir, true);
			}
			string metaFile = dir + ".meta";
			if (File.Exists(metaFile))
			{
				File.Delete(metaFile);
			}
		}
	}

	private static void CopyDirectory(string sourceDir, string destinationDir)
	{
		Directory.CreateDirectory(destinationDir);
		foreach (string file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
		{
			string relativePath = file.Substring(sourceDir.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
			string destFile = Path.Combine(destinationDir, relativePath);
			string destDir = Path.GetDirectoryName(destFile);
			if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
			File.Copy(file, destFile, true);
		}
	}
}