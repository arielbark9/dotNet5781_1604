using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DLAPI
{
    static class DLConfig
    {
        const string CONFIG_PATH = @"..\Configuration\CONFIG.xml";
        public class DLPackage
        {
            public string Name;
            public string PkgName;
            public string NameSpace;
            public string ClassName;
        }
        internal static string DLName;
        internal static Dictionary<string, DLPackage> DLPackages;
        static DLConfig()
        {
            XElement root = XElement.Load(CONFIG_PATH);
            XElement dlConfig = root.Element("dlconfig");
            DLName = dlConfig.Element("dl").Value;
            DLPackages = (from pkg in dlConfig.Element("dl-packages").Elements()
                          let tmp1 = pkg.Attribute("namespace")
                          let nameSpace = tmp1 == null ? "DL" : tmp1.Value
                          let tmp2 = pkg.Attribute("class")
                          let className = tmp2 == null ? pkg.Value : tmp2.Value
                          select new DLPackage()
                          {
                              Name = "" + pkg.Name,
                              PkgName = pkg.Value,
                              NameSpace = nameSpace,
                              ClassName = className
                          }).ToDictionary(p => "" + p.Name, p => p);
        }
    }
    /// <summary>
    /// Represents errors during DalApi initialization
    /// </summary>
    [Serializable]
    public class DLConfigException : Exception
    {
        public DLConfigException(string message) : base(message) { }
        public DLConfigException(string message, Exception inner) : base(message, inner) { }
    }
}
