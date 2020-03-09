using UnityEditor;
using UnityEditor.Build;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        public class OnBuildTargetChange : IActiveBuildTargetChanged
        {
            public int callbackOrder => 0;

            public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
            {
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

                if (!defines.Contains(SettingsInspector.TMP_ENABLED))
                {
                    if (EditorUtility.DisplayDialog("Enable Text mesh pro", "Do you want to enable Text mesh pro on the new build target platform?", "Yes", "No"))
                    {
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, $"{defines};{SettingsInspector.TMP_ENABLED}");
                    }
                }
            }
        }
    }
}