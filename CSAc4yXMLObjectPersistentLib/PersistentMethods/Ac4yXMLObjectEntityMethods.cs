using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using d7p4n4Namespace.Algebra.Class;
using d7p4n4Namespace.Final.Class;
using d7p4n4Namespace.Context.Class;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace d7p4n4Namespace.EFMethods.Class
{
    public class Ac4yXMLObjectEntityMethods : Ac4yXMLObjectAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public Ac4yXMLObjectEntityMethods() { }

        public Ac4yXMLObjectEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public Ac4yXMLObject findFirstById(int id)
        {
            Ac4yXMLObject a = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Ac4yXMLObjects
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Ac4yXMLObject>();

                a = query;
            }
            return a;
        }
		
		public Ac4yXMLObject LoadXmlById(int id)
        {
			Ac4yXMLObject a = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Ac4yXMLObjects
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Ac4yXMLObject>();

                a = query;
            }

            string xml = a.serialization;

            Ac4yXMLObject aResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Ac4yXMLObject));

            StringReader reader = new StringReader(xml);
            aResult = (Ac4yXMLObject)serializer.Deserialize(reader);
            reader.Close();

            return aResult;
        }
		
	public void addNew(Ac4yXMLObject _Ac4yXMLObject)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Ac4yXMLObjects.Add(_Ac4yXMLObject);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(Ac4yXMLObject _Ac4yXMLObject)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Ac4yXMLObject));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _Ac4yXMLObject);

            xml = stringWriter.ToString();

            _Ac4yXMLObject.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Ac4yXMLObjects.Add(_Ac4yXMLObject);

                ctx.SaveChanges();
            }
        }
    }
}
