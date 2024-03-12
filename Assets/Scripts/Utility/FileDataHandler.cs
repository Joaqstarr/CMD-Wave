using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public SaveData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        SaveData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Deserialize
                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);

            }
            catch (Exception e) 
            { 
                Debug.LogError("Error occured when trying to load data from file: "+ fullPath +"\n"+ e);
            }
        }

        string imagePath = Path.Combine(_dataDirPath, loadedData._imageName);

        if (File.Exists(imagePath))
        {
            byte[] bytes = File.ReadAllBytes(imagePath);

            Texture2D fogTexture = new Texture2D(1,1);
            fogTexture.LoadImage(bytes);
            loadedData._fogOfWarTexture = fogTexture;
 
                
            
        }

        return loadedData;
    }

    public void Save(SaveData saveData)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        string imagePath = Path.Combine(_dataDirPath, saveData._imageName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize C# Game Data
            string dataToStore = JsonUtility.ToJson(saveData, false);

            //Write file

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }





        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);

        }

        //fog
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        byte[] bytes = saveData._fogOfWarTexture.EncodeToPNG();
        File.WriteAllBytes(imagePath, bytes);


    }
}
