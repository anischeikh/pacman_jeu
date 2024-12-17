using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

public class XmlValidator
{
    public static bool ValidateXml(string xmlFilePath, string xsdFilePath)
    {
        try
        {
            Console.WriteLine("Début de la validation...");

            // Vérifier que les fichiers existent
            if (!File.Exists(xmlFilePath) || !File.Exists(xsdFilePath))
            {
                Console.WriteLine("Erreur : Un des fichiers (XML ou XSD) est introuvable.");
                return false;
            }

            Console.WriteLine($"Fichier XML : {Path.GetFullPath(xmlFilePath)}");
            Console.WriteLine($"Fichier XSD : {Path.GetFullPath(xsdFilePath)}");

            // Charger le schéma XSD
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdFilePath);
            Console.WriteLine("Schéma XSD chargé avec succès.");

            // Créer un lecteur XML
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(schemas);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += ValidationCallback;

            // Lire et valider le fichier XML
            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                while (reader.Read()) { }
            }

            Console.WriteLine("Validation réussie : Aucun problème détecté.");
            return true;
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.WriteLine($"Erreur de validation XML : {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur générale : {ex.Message}");
            return false;
        }
    }

    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Warning)
        {
            Console.WriteLine($"Avertissement : {e.Message}");
        }
        else
        {
            Console.WriteLine($"Erreur de validation : {e.Message}");
            throw new XmlSchemaValidationException(e.Message);
        }
    }
}
