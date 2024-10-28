using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class MedalsSave : MonoBehaviour {

    public static MedalsSave inst;

    void Awake()
    {
        if(inst == null)
            inst = this;
    }

	public bool Save()
	{
#if UNITY_EDITOR
        var path = Application.persistentDataPath + "/medals.bin";
        var fileStream = new FileStream(path, FileMode.Create);

        string dataToSave = "";

        for(int i = 0; i < MedalsManager.medalsManager.obtainedMedals.Count; i++)
        {
            if(i == MedalsManager.medalsManager.obtainedMedals.Count - 1)
            {
                if (dataToSave != "")
                    dataToSave += MedalsManager.medalsManager.obtainedMedals[i];
                else
                    dataToSave = MedalsManager.medalsManager.obtainedMedals[i];
            }
            else
            {
                if (dataToSave != "")
                    dataToSave += MedalsManager.medalsManager.obtainedMedals[i] + "\n";
                else
                    dataToSave = MedalsManager.medalsManager.obtainedMedals[i] + "\n";
            }
        }

        byte[] prefsData = Encoding.ASCII.GetBytes(
            dataToSave
        );
        fileStream.Write(prefsData, 0, prefsData.Length);
        fileStream.Close();
        return true;
#else
        WiiU.SaveCommand cmd = WiiU.Save.SaveCommand(WiiU.Save.commonAccountNo);

        long freespace = 0;
        WiiU.Save.FSStatus status = cmd.GetFreeSpaceSize(out freespace, WiiU.Save.FSRetFlag.None);
        if (status != WiiU.Save.FSStatus.OK)
            return false;

        long needspace = Mathf.Max(1024 * 1024, WiiU.PlayerPrefsHelper.rawData.Length);

        if (freespace < needspace)
        {
            // not enough free space
            return false;
        }
        else
        {
            var path = WiiU.Save.commonAccountPath + "/medals.bin";
            var fileStream = new FileStream(path, FileMode.Create);

            string dataToSave = "";

            for(int i = 0; i < MedalsManager.medalsManager.obtainedMedals.Count; i++)
            {
                if(i == MedalsManager.medalsManager.obtainedMedals.Count - 1)
                {
                    if (dataToSave != "")
                        dataToSave += MedalsManager.medalsManager.obtainedMedals[i];
                    else
                        dataToSave = MedalsManager.medalsManager.obtainedMedals[i];
                }
                else
                {
                    if (dataToSave != "")
                        dataToSave += MedalsManager.medalsManager.obtainedMedals[i] + "\n";
                    else
                        dataToSave = MedalsManager.medalsManager.obtainedMedals[i] + "\n";
                }
            }

            byte[] prefsData = Encoding.ASCII.GetBytes(
                dataToSave
            );
            fileStream.Write(prefsData, 0, prefsData.Length);
            fileStream.Close();

            // It is very important to flush quota, otherwise filesystem changes will be discarded upon reboot
            status = cmd.FlushQuota(WiiU.Save.FSRetFlag.None);
            if (status != WiiU.Save.FSStatus.OK)
                return false;
        }

        return true;
#endif
    }

    public bool Load()
    {
        try
        {
            using (var fileStream = new FileStream(WiiU.Save.commonAccountPath + "/medals.bin", FileMode.Open))
            {
                var dataSize = (int)fileStream.Length;

                if (dataSize <= 0)
                {
                    return false;
                }
                byte[] data = new byte[dataSize];

                if (fileStream.Read(data, 0, dataSize) < dataSize)
                {
                    return false;
                }

                string dataStr = Encoding.Default.GetString(data);
                char[] breakLine = "\n".ToCharArray();

                foreach(var lines in dataStr.Split(breakLine))
                {
                    PlayerPrefs.SetString(lines, "obtained");
                    MedalsManager.medalsManager.obtainedMedals.Add(lines);
                    MedalsManager.medalsManager.medalsToShow.Add(lines);
                    MedalsManager.medalsManager.medalsDescToShow.Add("null");
                    MedalsManager.medalsManager.medalsIconToShow.Add(null);
                }

                fileStream.Close();

                return true;
            }
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }
}
