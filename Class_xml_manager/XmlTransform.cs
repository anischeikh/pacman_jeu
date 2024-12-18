using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using pacman;

namespace Pacman.Class_xml_manager
{
    public class XmlTransform
    {
   
        
        public void TransformXmlToHtml(string xmlFilePath, string xsltFilePath, string htmlOutputFilePath)
        {
            try
            {
                // Vérifier que les fichiers XML et XSLT existent
                if (!File.Exists(xmlFilePath))
                {
                    throw new FileNotFoundException($" le  XML  n'existe pas : {xmlFilePath}");
                }

                if (!File.Exists(xsltFilePath))
                {
                    throw new FileNotFoundException($"Le XSLT  n'existe pas : {xsltFilePath}");
                }
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltFilePath);
                using (XmlWriter writer = XmlWriter.Create(htmlOutputFilePath, new XmlWriterSettings { Indent = true }))
                {
                    xslt.Transform(xmlDoc, writer);
                }
                Console.WriteLine($"HTML généré ;) : {htmlOutputFilePath}");
                
                Process.Start(new ProcessStartInfo(htmlOutputFilePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans la transformation : {ex.Message}");
            }
        }
    }
}