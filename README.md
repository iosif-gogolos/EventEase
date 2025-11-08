# EventEase: .NET / Blazor Project
## Kurzbeschreibung
EventEase ist eine .NET / Blazor-Anwendung zum Verwalten und Anzeigen von Events. Dieses README beschreibt, wie das Repository geklont und lokal gestartet wird.

## Voraussetzungen
- Git (zum Klonen des Repositories)
- .NET SDK 6.0 oder neuer (empfohlen: 6.x oder 7.x)
  - Prüfen mit: `dotnet --version`
- Optional:
  - Visual Studio 2022+ (Windows/Mac) oder Visual Studio Code mit C#-Erweiterung
  - Moderne Browser (Chrome, Edge, Firefox)

## Repository klonen
1. Repository klonen (ersetze `<REPO_URL>` durch die korrekte URL):
   ```bash
   git clone <REPO_URL>
   ```
2. In das Projektverzeichnis wechseln:
   ```bash
   cd EventEase
   ```

## Projekt lokal starten (Konsole)
1. Alle NuGet-Pakete wiederherstellen:
   ```bash
   dotnet restore
   ```
2. Projekt bauen:
   ```bash
   dotnet build
   ```
3. Projekt starten:
   - Wenn die Lösung/der Ordner genau ein Startup-Projekt enthält:
     ```bash
     dotnet run
     ```
   - Falls es mehrere Projekte (z. B. Client/Server) gibt, das entsprechende Host-Projekt angeben:
     ```bash
     dotnet run --project ./src/EventEase.Server/EventEase.Server.csproj
     ```
   - Empfohlene Alternative: im Visual Studio / VS Code das Startprojekt auswählen und "Run/Debug" verwenden.

4. Nach dem Start im Browser öffnen:
   - Typische URLs:
     - HTTPS: `https://localhost:5001`
     - HTTP: `http://localhost:5000`
   - Die tatsächlichen Ports werden beim Start in der Konsole angezeigt (oder in `launchSettings.json`).

## Projekt mit Visual Studio Code
1. VS Code öffnen:
   ```bash
   code .
   ```
2. Sicherstellen, dass die Erweiterung "C#" installiert ist.
3. Im Explorer das gewünschte Startup-Projekt wählen (falls nötig).
4. F5 drücken, um die Anwendung im Debug-Modus zu starten.

## Build für Produktion / Veröffentlichung
1. Build für Release:
   ```bash
   dotnet publish -c Release -o ./publish
   ```
2. Ergebnisse befinden sich dann im `./publish`-Ordner und können auf einen Webserver oder Host deployt werden.

## Umgebungsvariablen & Ports
- Um ein anderes Environment zu verwenden:
  ```bash
  # Linux / macOS
  export ASPNETCORE_ENVIRONMENT=Development

  # Windows (PowerShell)
  $env:ASPNETCORE_ENVIRONMENT="Development"
  ```
- Wenn Standardports belegt sind, kann man die URLs überschreiben:
  ```bash
  # Beispiel: andere Ports
  ASPNETCORE_URLS="https://localhost:5003;http://localhost:5002" dotnet run
  ```

## Troubleshooting
- `dotnet --version` zeigt die installierte SDK-Version. Falls Version zu alt: .NET SDK aktualisieren.
- Fehler beim `dotnet restore`: Netzwerk prüfen, ggf. NuGet-Feeds konfigurieren.
- Portkonflikt: andere Anwendung beenden oder `ASPNETCORE_URLS` verwenden (siehe oben).
- Browser zeigt Fehler/weißes Blatt: Konsole prüfen, Logs in der Terminalausgabe beachten.

## Weitere Hinweise
- Falls die Lösung Datenbanken, API-Keys oder zusätzliche Konfiguration benötigt, befinden sich Hinweise typischerweise in `appsettings.json` oder in projektspezifischen README-Dateien; diese Werte müssen lokal angepasst werden.
- Für UI-Änderungen in Blazor: Dateien unter `Pages/` und `Components/` editieren; Anwendung neu starten/neu laden.

## Kontakt / Support
Bei Fragen zur lokalen Einrichtung oder Problemen bitte Issues im Repository anlegen oder den Projektverantwortlichen kontaktieren.
