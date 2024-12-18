<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <!-- Application des templates à la racine -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Game Data</title>
                <style>
                    .section {
                    margin: 20px 0;
                    }
                    .sub-header {
                    font-weight: bold;
                    }
                </style>
            </head>
            <body>
                <xsl:apply-templates select="Game1"/>
            </body>
        </html>
    </xsl:template>

    <!-- Template pour la racine du jeu 'Game1' -->
    <xsl:template match="Game1">
        <div class="section">
            <h2 class="sub-header">Points</h2>
            <!-- Appliquer le template pour les 'points' -->
            <xsl:apply-templates select="points"/>
        </div>

        <!-- Afficher les fantômes -->
        <div class="section">
            <h2 class="sub-header">Ghosts</h2>
            <!-- Appliquer le template pour les 'ghosts' -->
            <xsl:apply-templates select="ghosts"/>
        </div>
    </xsl:template>

    <!-- Template pour traiter les 'points' -->
    <xsl:template match="points">
        <xsl:apply-templates select="point"/>
    </xsl:template>

    <!-- Template pour chaque élément 'point' -->
    <xsl:template match="point">
        <p><strong>Point Size:</strong> <xsl:value-of select="size"/>,
            <strong>Position:</strong> (<xsl:value-of select="position/x"/>, <xsl:value-of select="position/y"/>)</p>
    </xsl:template>

    <!-- Template pour traiter les 'ghosts' -->
    <xsl:template match="ghosts">
        <xsl:apply-templates select="ghost"/>
    </xsl:template>

    <!-- Template pour chaque élément 'ghost' -->
    <xsl:template match="ghost">
        <p><strong>Ghost Speed:</strong> <xsl:value-of select="speed"/>,
            <strong>Direction:</strong> (<xsl:value-of select="direction/x"/>, <xsl:value-of select="direction/y"/>)</p>
    </xsl:template>

    <!-- Template pour afficher les autres données comme les tailles, l'état, etc. -->
    <xsl:template match="taille">
        <div class="section">
            <h2 class="sub-header">Size</h2>
            <p><strong>Size:</strong> <xsl:value-of select="."/></p>
        </div>
    </xsl:template>

    <xsl:template match="state">
        <div class="section">
            <h2 class="sub-header">Game State</h2>
            <p><strong>State:</strong> <xsl:value-of select="."/></p>
        </div>
    </xsl:template>

    <xsl:template match="gameOverOption">
        <div class="section">
            <h2 class="sub-header">Game Over Option</h2>
            <p><strong>Game Over Option:</strong> <xsl:value-of select="."/></p>
        </div>
    </xsl:template>

    <!-- Template pour afficher les options de sélection -->
    <xsl:template match="SelectOption">
        <div class="section">
            <h2 class="sub-header">Select Option</h2>
            <p><strong>Option:</strong> <xsl:value-of select="."/></p>
        </div>
    </xsl:template>

    <!-- Template pour afficher les informations sur le joueur -->
    <xsl:template match="player_name">
        <div class="section">
            <h2 class="sub-header">Player Name</h2>
            <p><strong>First Name:</strong> <xsl:value-of select="first_name"/>,
                <strong>Last Name:</strong> <xsl:value-of select="last_name"/>,
                <strong>Email:</strong> <xsl:value-of select="email"/></p>
        </div>
    </xsl:template>

    <!-- Template pour afficher les scores du joueur -->
    <xsl:template match="player_scores">
        <div class="section">
            <h2 class="sub-header">Player Scores</h2>
            <p><strong>Total Pellets Eaten:</strong> <xsl:value-of select="tot_pellets_eaten"/>,
                <strong>Total Games Played:</strong> <xsl:value-of select="tot_games_played"/>,
                <strong>Total Games Won:</strong> <xsl:value-of select="tot_games_won"/>,
                <strong>Total Games Lost:</strong> <xsl:value-of select="tot_games_lost"/></p>
        </div>
    </xsl:template>

</xsl:stylesheet>
