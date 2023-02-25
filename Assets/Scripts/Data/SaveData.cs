using System.Collections;
using UnityEngine;

namespace Data
{
    public class SaveData 
    {
        public PlayerPosition position;
        public PlayerHealth health; 


        public void Save()
        {
            XML_Serialization.Serialize<SaveData>(this, XMLFileNames.roomTreeFilename);
        }

        public SaveData Load()
        {
            return XML_Serialization.Deserialize<SaveData>(XMLFileNames.roomTreeFilename);
        }
    }
}