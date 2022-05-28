using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyRate.Application.Extension
{
    public static class XmlExtension
    {
        public static T Deserilize<T>(string content)
        {
            StringReader strReader = null;

            XmlSerializer serializer = null;

            System.Xml.XmlTextReader xmlReader = null;

            Object obj = null;
            try
            {
                strReader = new StringReader(content);

                serializer = new XmlSerializer(typeof(T));

                xmlReader = new System.Xml.XmlTextReader(strReader);

                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {

            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return (T)Convert.ChangeType(obj, typeof(T));


        }
    }
}
