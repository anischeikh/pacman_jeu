using System;
using System.Xml;

namespace pacman.Class_xml_manager;

public static class XmlScoreHandler
{
    
    private static XmlDocument _xmlDoc;      

    private static XmlNode _element;
    
    //nom du fichier XML (avec le PATH) et le nom de l'élément (ne pas oublier les //)!!!
    public static void IncrementElement(string nomFichierXml, string nomElement)
    {
        try
        {
            _xmlDoc = new XmlDocument();
            _xmlDoc.Load(nomFichierXml);
            _element = _xmlDoc.SelectSingleNode(nomElement);
            
            if (_element != null && int.TryParse(_element.InnerText, out int score))
            {
                score++;
                _element.InnerText = score.ToString();
                _xmlDoc.Save(nomFichierXml);
            }
            else
            {
                Console.WriteLine("Element "+nomElement+" pas trouvé!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        
    } 
}