using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tick
{
    class ConfigXmlPassage
    {
        private readonly string path;
        public ConfigXmlPassage(string filename) => path = filename;
        private void SaveObj(XmlDocument xml) => xml.Save(path);
        public string GetLanguage()
        {
            try
            {
                return getXmlObj().FirstChild.NextSibling.FirstChild.InnerText;
            }
            catch (System.AggregateException e)
            {
                throw e;
            }
        }
        public void SetLanguage(string context)
        {
            XmlDocument xml = getXmlObj();
            xml.FirstChild.NextSibling.FirstChild.InnerText = context;
            SaveObj(xml);
        }
        public string GetTheme()
        {
            try
            {
                return getXmlObj().FirstChild.NextSibling.LastChild.InnerText;
            }
            catch(System.AggregateException e)
            {
                throw e;
            }
        }
        public void SetTheme(string context)
        {
            XmlDocument xml = getXmlObj();
            xml.FirstChild.NextSibling.LastChild.InnerText = context;
            SaveObj(xml);
        }

        private XmlDocument getXmlObj()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                return xml;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw e;
            }
        }
    }
}
