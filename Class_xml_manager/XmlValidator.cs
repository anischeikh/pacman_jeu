using System;
using System.Xml;
using System.Xml.Schema;

public class XmlValidator
{
    public static bool ValidateXml(string xmlFilePath, string xsdFilePath)
    {
        try
        {
            // Charger le schéma XSD
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdFilePath);

            // Créer un lecteur XML
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(schemas);
            settings.ValidationType = ValidationType.Schema;

            // Événement de validation
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

            // Charger et valider le fichier XML
            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                while (reader.Read()) { }
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur de validation : {ex.Message}");
            return false;
        }
    }

    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Warning)
        {
            Console.WriteLine($"Avertissement: {e.Message}");
        }
        else
        {
            Console.WriteLine($"Erreur: {e.Message}");
        }
    }
}