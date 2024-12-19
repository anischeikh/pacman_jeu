<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns="http://www.w3.org/2001/XMLSchema-instance">

    <xsl:output method="html" indent="yes"/>

    <!-- Titre de la page -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Hi-Scores</title>
                <style>
                    table {
                        width: 50%;
                        border-collapse: collapse;
                        margin: 25px 0;
                        font-size: 18px;
                        text-align: left;
                    }
                    th, td {
                        padding: 12px;
                        border: 1px solid #ddd;
                    }
                    th {
                        background-color: #f2f2f2;
                    }
                </style>
            </head>
            <body>
                <h1>Hi-Scores</h1>
                <table>
                    <thead>
                        <tr>
                            <th>Player Name</th>
                            <th>Game Date</th>
                            <th>Score</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Appliquer le template pour chaque élément <Game> et trier par score -->
                        <xsl:apply-templates select="Games/Game">
                            <xsl:sort select="Score" data-type="number" order="descending"/>
                        </xsl:apply-templates>
                    </tbody>
                </table>
            </body>
        </html>
    </xsl:template>

    <!-- Template pour chaque jeu -->
    <xsl:template match="Game">
        <tr>
            <td><xsl:value-of select="PlayerName"/></td>
            <td><xsl:value-of select="GameDate"/></td>
            <td><xsl:value-of select="Score"/></td>
        </tr>
    </xsl:template>
</xsl:stylesheet>
