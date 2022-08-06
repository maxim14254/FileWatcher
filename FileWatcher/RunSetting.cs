using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FileWatcher
{
    [Serializable]
    internal class RunSetting
    {
        internal string Path { get; set; }
        internal int? ProcessId { get; set; }
        internal bool UAC { get; set; }

        internal RunSetting(string path, int? processId, bool uac)
        {
            Path = path;
            ProcessId = processId;
            UAC = uac;
        }

        internal void Serializable()
        {
            BinaryFormatter formatter = new BinaryFormatter();
   
            using (FileStream fs = new FileStream("RunSetting.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }
        internal void Deserializable()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("RunSetting.dat", FileMode.OpenOrCreate))
            {
                var runSetting = (RunSetting)formatter.Deserialize(fs);
                Path = runSetting.Path;
                ProcessId = runSetting.ProcessId;
                UAC = runSetting.UAC;
            }
        }
    }
}
