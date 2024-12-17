using System;
using System.Xml;
using System.Xml.Schema;

public class XmlValidator
{
    public static bool ValidateXml(string xmlFilePath, string xsdFilePath)
    {
        try
        {
            // Charger le schéma XSD explicitement
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, xsdFilePath); // Namespace null car noNamespaceSchemaLocation

            // Paramètres du lecteur XML pour la validation
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema
            };
            settings.Schemas.Add(schemas);

            // Gestion des erreurs de validation
            settings.ValidationEventHandler += ValidationCallback;

            // Charger et valider le fichier XML
            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                while (reader.Read()) { } // Lire tout le document pour forcer la validation
            }

            Console.WriteLine("Validation réussie : Aucun problème détecté.");
            return true; // Retourne vrai si aucune erreur
        }
        catch (XmlException ex)
        {
            Console.WriteLine($"Erreur XML : {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur inattendue : {ex.Message}");
        }

        return false; // Retourne faux en cas d'erreur
    }

    // Callback pour gérer les erreurs de validation
    private static void ValidationCallback(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
        {
            Console.WriteLine($"Erreur : {e.Message}");
            throw new XmlException(e.Message); // Arrêter immédiatement si erreur
        }
        else
        {
            Console.WriteLine($"Avertissement : {e.Message}");
        }
    }
}
