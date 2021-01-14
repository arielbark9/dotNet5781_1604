using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    public class XMLTools
    {
        static string dir = @"..\Data\";
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region SaveLoadWithXElement
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}");
            }
        }
        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}");
            }
        }
        #endregion
        #region IDs Configuration
        public static int GetAndIncrementRunningNum<T>()
        {
            int res = 0;
            string fp = @"..\Configuration\CONFIG.xml";
            XElement configRoot;
            try
            {
               configRoot  = XElement.Load(fp);
            }
            catch
            {
                throw new DO.XMLFileLoadCreateException(fp, $"fail to load xml file: {fp}");
            }
            XElement runningNum = configRoot.Element("ids-entities").Element(typeof(T).Name);
            res = int.Parse(runningNum.Value);
            runningNum.Value = (int.Parse(runningNum.Value) + 1).ToString();
            configRoot.Save(dir+fp);
            return res;
        }
        #endregion
        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}");
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}");
            }
        }
        #endregion
    }
}
