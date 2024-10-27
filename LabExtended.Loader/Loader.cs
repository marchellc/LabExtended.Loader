using PluginAPI.Core;
using PluginAPI.Core.Attributes;

using PluginAPI.Loader;

using System;
using System.Linq;
using System.Reflection;

namespace LabExtended.Loader
{
    public class Loader
    {
        [PluginEntryPoint("LabExtended Loader", "1.0.0", "Loads the LabExtended dependency.", "marchellcx")]
        public void Load()
        {
            Info("Loading ..");

            try
            {
                var assembly = AssemblyLoader.Dependencies.FirstOrDefault(x => x.GetName().Name == "LabExtended");

                if (assembly is null)
                {
                    Error("LabExtended assembly has not been loaded.");
                    return;
                }

                var type = assembly.GetTypes().FirstOrDefault(x => x.Name == "ApiLoader");

                if (type is null)
                {
                    Error("ApiLoader type has not been found.");
                    return;
                }

                var method = type.GetMethod("LoaderPoint", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                if (method is null)
                {
                    Error("LoaderPoint method has not been found.");
                    return;
                }

                method.Invoke(null, null);
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        public void Error(object msg)
            => Log.Error(msg.ToString(), "LabExtended");

        public void Info(object msg)
            => Log.Info(msg.ToString(), "LabExtended");

        public void Warn(object msg)
            => Log.Warning(msg.ToString(), "LabExtended");
    }
}