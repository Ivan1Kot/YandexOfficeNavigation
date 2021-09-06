// filename BuildPostProcessor.cs
// put it in a folder Assets/Editor/
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class BuildPostProcessor {

    [PostProcessBuild (0)]
    public static void ChangeXcodePlist(BuildTarget buildTarget, string path) {

        if (buildTarget == BuildTarget.iOS) {
#if UNITY_IPHONE

            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            PlistElementDict rootDict = plist.root;

            Debug.Log(">> Automation, plist ... <<");

            // example of changing a value:
            // rootDict.SetString("CFBundleVersion", "6.6.6");

            // example of adding a boolean key...
            // < key > ITSAppUsesNonExemptEncryption </ key > < false />
            rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

            File.WriteAllText(plistPath, plist.WriteToString());
#endif
        }
    }

    [PostProcessBuildAttribute(1)]
    public static void OnPostProcessBuild(BuildTarget target, string path) {

        if (target == BuildTarget.iOS) {

            PBXProject project = new PBXProject();
            string sPath = PBXProject.GetPBXProjectPath(path);
            project.ReadFromFile(sPath);
            
            string tn = project.GetUnityFrameworkTargetGuid();
            //string tn = PBXProject.GetUnityTargetName();
            //string g = project.TargetGuidByName(tn);

            ModifyFrameworksSettings(project, tn);

            // modify frameworks and settings as desired
            File.WriteAllText(sPath, project.WriteToString());
        }
    }

    static void ModifyFrameworksSettings(PBXProject project, string g) {
        Debug.Log(">> Automation, Frameworks... <<");

        Debug.Log(g);
        
        project.AddFrameworkToProject(g, "Accelerate.framework", false);
        project.AddFrameworkToProject(g, "ARKit.framework", false);
        
        project.AddFrameworkToProject(g, "libc++.tbd", false);
        
        
        Debug.Log(">> Automation, Settings... <<");

        project.AddBuildProperty(g,
            "OTHER_LDFLAGS",
            "-ObjC");

        project.AddBuildProperty(g,
            "ENABLE_BITCODE",
            "false");
        
    }
}