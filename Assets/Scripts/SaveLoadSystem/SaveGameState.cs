using System.Text;
using System.IO;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class SaveGameState : MonoBehaviour
{
    public static bool DoSave(byte[] data)
    {
        WiiU.SaveCommand cmd = WiiU.Save.SaveCommand(WiiU.Save.accountNo);

        long freespace = 0;
        WiiU.Save.FSStatus status = cmd.GetFreeSpaceSize(out freespace, WiiU.Save.FSRetFlag.None);
        if (status != WiiU.Save.FSStatus.OK)
            return false;

        long needspace = Mathf.Max(1024 * 1024, data.Length);

        if (freespace < needspace)
        {
            // not enough free space
            return false;
        }
        else
        {
            var path = Application.persistentDataPath + "/data.bin";
            var fileStream = new FileStream(path, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();

            // It is very important to flush quota, otherwise filesystem changes will be discarded upon reboot
            status = cmd.FlushQuota(WiiU.Save.FSRetFlag.None);
            if (status != WiiU.Save.FSStatus.OK)
                return false;
        }

        return true;
    }

    public static string DoLoad()
    {
        try
        {
            using (var fileStream = new FileStream(Application.persistentDataPath + "/data.bin", FileMode.Open))
            {
                var dataSize = (int)fileStream.Length;

                if (dataSize <= 0)
                {
                    return string.Empty;
                }

                byte[] data = new byte[dataSize];

                if (fileStream.Read(data, 0, dataSize) < dataSize)
                {
                    return string.Empty;
                }

                string json = Encoding.UTF8.GetString(data);

                fileStream.Close();

                return json;
            }
        }
        catch (FileNotFoundException)
        {
            return string.Empty;
        }
    }
}