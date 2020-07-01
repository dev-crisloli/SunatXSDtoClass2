using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XSDtoClass
{
    class Program
    {
        static void Main(string[] args)
        {
            InvoiceType invoiceType = new InvoiceType();
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement firma = xmlDocument.CreateElement("ext:firma", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            UBLExtensionType[] uBLExtensionTypes = new UBLExtensionType[]
            {
                new UBLExtensionType{ ExtensionContent = firma}
            };

            UBLVersionIDType uBLVersionIDType = new UBLVersionIDType();
            uBLVersionIDType.Value = "2.1";

            CustomizationIDType customizationIDType = new CustomizationIDType();
            customizationIDType.Value = "2.0";

            ProfileIDType profileIDType = new ProfileIDType();

            profileIDType.schemeName = "SUNAT:Identificador de Tipo de Operación";
            profileIDType.schemeAgencyName = "PE:SUNAT";
            profileIDType.schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo17";
            profileIDType.Value = "0101";

            InvoiceLineType[] invoiceLineTypes = new InvoiceLineType[] {
                new InvoiceLineType { ID= new IDType { Value = "1"} },
                new InvoiceLineType { ID= new IDType { Value = "2"} }
            };
            
            invoiceType.UBLVersionID = uBLVersionIDType;
            invoiceType.UBLExtensions = uBLExtensionTypes;
            invoiceType.CustomizationID = customizationIDType;
            invoiceType.ProfileID = profileIDType;
            invoiceType.InvoiceLine = invoiceLineTypes;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceType));

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            xmlSerializerNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            xmlSerializerNamespaces.Add("ccts","urn:un:unece:uncefact:documentation:2");
            xmlSerializerNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            xmlSerializerNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            xmlSerializerNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            xmlSerializerNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");


            var oStringWriter = new StringWriter();
            xmlSerializer.Serialize(XmlWriter.Create(oStringWriter), invoiceType, xmlSerializerNamespaces);
            string stringXML = oStringWriter.ToString();
            System.IO.File.WriteAllText("XML_Sunat.xml",stringXML);
        }
    }
}
