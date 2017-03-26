# CBM Disk Image Tools

The CBM Disk Image Tools are a collection of several small programs that can process images of Commodore diskettes. The focus of the tool collection is currently to create checksums of individual files.

## Contributing

Please refer to each project's style guidelines and guidelines for submitting patches and additions. In general, we follow the "fork-and-pull" Git workflow.

 1. **Fork** the repo on GitHub
 2. **Clone** the project to your own machine
 3. **Commit** changes to your own branch
 4. **Push** your work back up to your fork
 5. Submit a **Pull request** so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## License

CBM Disk Image Tools is licensed under the [MIT license](LICENSE).

# Benutzerdokumentation
## CBM Disk Image Tools

Die CBM Disk Image Tools sind eine Sammlung von mehreren kleinen Programmen, welche Abbildungen (Images) von Commodore Disketten verarbeiten können. Der Schwerpunkt der Toolsammlung liegt momentan auf dem Erstellen von Prüfsummen von einzelnen Dateien. Diese Prüfsummen sollen den Vergleich von einzelnen Dateien vereinfachen. Es sollen dabei zum einen Commodore DOS Dateien und zum anderen GEOS Dateien verarbeitet werden.
Ein weiteres Ziel der Toolsammlung ist es, auch für vorhandene Dateien im CVT Format vergleichbare Prüfsummen zu erstellen.

### Bestandteile

### CDIDir

CDIDir dient zur Anzeigen des Directorys eines  Abbildes (Images) einer Commodore Diskette. Die Anzeige soll dabei der bekannten Ausgabe am C64 entsprechen. Für Geos Dateien werden noch weitere Informationen angezeigt. Für jede Datei wird auch eine Prüfsumme generiert und ausgegeben.

### CDIExtract

### CVTChecksum

### CVT2CleanCVT

# Entwicklungsdokumentation

## Konventionen für die Groß-/Kleinschreibung

### Regeln für die Groß-/Kleinschreibung von Bezeichnern

Es werden die normalen Regeln von Microsoft verwendet (siehe <https://msdn.microsoft.com/de-de/library/ms229043(v=vs.100).aspx>).

|Bezeichner                           |Case    |Beispiel        |
|:------------------------------------|:-------|:---------------|
|class                                |Pascal  |AppDomain       |
|Enumerationstyp                      |Pascal  |ErrorLevel      |
|Enumerationswerte                    |Pascal  |FatalError      |
|event                                |Pascal  |ValueChanged    |
|Ausnahmeklasse                       |Pascal  |WebException    |
|Schreibgeschütztes statisches Feld   |Pascal  |RedValue        |
|interface                            |Pascal  |IDisposable     |
|Methode                              |Pascal  |ToString        |
|namespace                            |Pascal  |System.Drawing  |
|Parameter                            |Camel   |typeName        |
|Property                             |Pascal  |BackColor       |

Eine Ausname bilden Konstanten, welche 

# Testdokumentation

# Installationsdokumentation