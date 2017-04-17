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

Die CBM Disk Image Tools sind eine Sammlung von mehreren kleinen Programmen, welche Abbildungen (Images) von Commodore Disketten verarbeiten können. Der Schwerpunkt der Toolsammlung liegt momentan auf dem Erstellen von Prüfsummen von einzelnen Dateien. Diese Prüfsummen sollen den Vergleich von einzelnen Dateien vereinfachen. Es sollen dabei zum einen Commodore DOS Dateien und zum anderen Geos Dateien verarbeitet werden.
Ein weiteres Ziel der Toolsammlung ist es, auch für vorhandene Dateien im CVT Format vergleichbare Prüfsummen zu erstellen.

### Bestandteile

#### CDIDir

CDIDir dient zur Anzeigen des Directorys eines Abbildes (Images) einer Commodore Diskette. Die Anzeige soll dabei der bekannten Ausgabe am C64 entsprechen. Für Geos Dateien werden noch weitere Informationen angezeigt. Für jede Datei wird auch eine Prüfsumme generiert und ausgegeben. Die Berechnung der Prüfsummen für Geos Dateien erfolgt durch die interne Umwandlung der Datei in das CleanCVT Format.

#### CDIExtract

CDIExtract dient zum Extrahieren von einzelnen Dateien aus einem Abbild (Image) einer Commodore Diskette. Geos Dateien werden dabei im CleanCVT Format extrahiert.

#### CVTChecksum

CVTChecksum dient zur Anzeige einer Prüfsumme von einer Datei im CVT Format. Die Berechnung der Prüfsumme erfolgt durch die interne Umwandlung der Datei in das CleanCVT Format.

#### CVT2CleanCVT

CVT2CleanCVT dient zur Konvertierung einer Datei im CVT Format in eine Datei im CleanCVT Format.

### Das CleanCVT Dateiformat

#### Was ist das CleanCVT Dateiformat?

Das CleanCVT Format entspricht dem CVT Format (siehe <https://ist.uwaterloo.ca/~schepers/formats/CVT.TXT>).
D.h. Alle Programme, welche Dateien im CVT Format verarbeiten können, sollten auch mit CleanCVT Dateien arbeiten können.
Bei CleanCVT Dateien werden alle Datenstellen, welche keine relevanten Informationen beinhalten, mit dem Wert $00 überschrieben und sind somit sozusagen gereinigt.

#### Welche Stellen werden in CleanCVT mit dem Wert $00 überschrieben?

Eine normal CVT Datei, welche z.B. vom Geos Programm CONVERT 2.5 oder vom MS-Dos Progrmm Star Commander erzeugt wird, enthält an einigen Datenstellen Informationen, welche zur Wiederherstellung der eigentlichen Geos Datei nicht benötigt werden. Genau diese Stellen werden beim CleanCVT Format mit dem Wert $00 überschrieben, um die Dateien dadurch vergleichbar zu machen. 

**Liste der Datenstellen welche den Wert $00 bekommen:**
* **Position 2 und 3** wird mit dem Wert $00 belegt, da sich dort die Spur und der Sektor des ersten Datenblocks (bzw. des VLIR Blocks) befinden. Eine CVT Datei beginnt mit 30 Byte, welche genau dem Directory Eintrag der gespeicherten Geos Datei entsprechen. Da die Spur und der Sektor des ersten Datenblocks beim Wiederherstellen auf einer Diskette ändern können, sollen diese Informationen nicht in die Berechnung der Prüfsumme einfließen.
* **Position 20 und 21** wird mit dem Wert $00 belegt, da sich dort die Spur und der Sektor des Info Blocks der Geos Datei  befinden. Auch der Geos Info Block kann auf verschiedenen Disketten auf unterschiedlichen Spuren bzw. Sektoren gespeichert sein.  Deshalb sollen auch diese Informationen nicht in die Berechnung der Prüfsumme einfließen.
* **Position 59 bis 254** wird mit dem Wert $00 belegt, da dieser Bereich in einem CVT eigentlich nicht benutzt wird. In diesem Bereich werden von manchen Programmen, welche CVT Dateien erzeugen, dennoch Informationen abgelegt. Hier findet man zum Beispiel Angaben zum Autor des Programms oder Ähnliches. Auch diese Informationen sollen nicht in die Berechnung der Prüfsumme einfließen.
* **Der Bereich des VLIR Record Blocks nach den Datensatzangaben** wird mit dem Wert $00 belegt. Der VLIR Record Block einer CVT Datei enthält Verweise zu den einzelnen Datensäten der CVT Datei. Der letzte Verweis ist an dem Werten $00 $00 zu erkennen. Nach diesen 2 Byte (bis zum Ende des VLIR Record Blocks) könnten sich noch Daten verbergen, welche für die Wiederherstellung der Geos Datei nicht benötigt werden. Auch diese Informationen sollen nicht in die Berechnung der Prüfsumme einfließen. Hinweis eine CVT Datei markiert gelöschte Datensäte, wie Geos auch,  mit den Werten $00 $FF. Auch gelöschte Datensätze müssen übrigens Wiederhergestellt werden, deshalb werden diese auch in  einer CVT Datei gespeichert.
* **Der Bereich nach den Daten jedes Letzen Datenblocks**  wird mit dem Wert $00 belegt. Bei VLIR Dateien werden immer nur ganze Datenblöcke eines Datensatzes gespeichert. D.h. auch die Letzten Datenblöcke eines Datensatzes sind 254 Byte lang, auch wenn diese weniger Nutzdaten enthalten.  Hier werden wohl die ganzen Blöcke der Quelldiskette in die CVT Datei übernommen. Auch diese Informationen sollen nicht in die Berechnung der Prüfsumme einfließen. Hinweis, der aller letzte Datensatz einer  VLIR Datei ist davon nicht betroffen, da von diesem nur die Nutzdaten in einer CVT Datei gespeichert werden.

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

### Regeln für die Groß-/Kleinschreibung von Konstanten

Eine Ausnahme bilden *Konstanten*, welche komplett mit Großbuchstaben zu bezeichnen sind. Einzelne Worte werden dabei mit einem Unterstrich getrennt. Dies nennt man auch CONSTANT\_CASE oder UPPER\_CASE. Beispiel: D64\_IMAGE\_FILE\_EXTENSION

## Visual Studio Solution

Der Aufbau der Visual Studio Solution orientierte sich an der Beschreibung von codingfreaks (siehe http://www.codingfreaks.de/2014/08/25/eine-solution-bauen/).

**Kurz Zusammengefasst:**
* Ordneraufteilung:
  * \_Shared: Ein Ordner, der immer ganz oben hängt (daher das "\_") und alles Mögliche aufnimmt, nur keine Projekte (z.B. Textdateien usw.). 
  * Logic: Nicht weiter verwunderlich kommen hier meist rein, die zentrale Logik bereitstellen. 
  * Test: Alles, was irgendwie nach Testing riecht, kommt hier rein. 
  * Ui: Natürlich benötigen wir meist irgendwas Visuelles am Anwender. Hier versammeln sich Web-, XAML und z.B. Consolen-Projekte. 
  * Setup
* Verwendung eines SharedAssemblyInfo.cs für alle Projekte
* Bezeichnung der Namespaces {SolutionName}.{SolutionFolder}.{ProjectName ohne SolutionFolder-Teil}.{ProjectFolder1}.{ProjectFolderx}
  * namespace ZAK256.CBMDiskImageTools.Logic.Core
  * namespace ZAK256.CBMDiskImageTools.Ui.CDIDir
  * namespace ZAK256.CBMDiskImageTools.Ui.CDIExtract
  * namespace ZAK256.CBMDiskImageTools.Ui.CDITools
  * namespace ZAK256.CBMDiskImageTools.Ui.CVT2CleanCVT
  * namespace ZAK256.CBMDiskImageTools.Ui.CVTChecksum

## Directory Einträge der verschiedenen Dateiarten

|Pos      | Commodore DOS (SEQ, PRG, USR)          | Commodore DOS (REL)                    | GEOS (SEQ)                             | GEOS (VLIR)                            |
|:--------|:---------------------------------------|:---------------------------------------|:---------------------------------------|:---------------------------------------|
|0 Bit0-3 | Dateiart                               | Dateiart                               | Dateiart                               | Dateiart                               |
|0 Bit4   | unbenutzt                              | unbenutzt                              | unbenutzt                              | unbenutzt                              |
|0 Bit5   | Ersetzungs- Kennung                    | Ersetzungs- Kennung                    | Ersetzungs- Kennung                    | Ersetzungs- Kennung                    |
|0 Bit6   | Schreibschutz- Kennung                 | Schreibschutz- Kennung                 | Schreibschutz- Kennung                 | Schreibschutz- Kennung                 |
|0 Bit7   | Offen- Kennung                         | Offen- Kennung                         | Offen- Kennung                         | Offen- Kennung                         |
|1        | Spur des ersten Datenblocks            | Spur des ersten Datenblocks            | Spur des ersten Datenblocks            | Spur des RECORD Blocks                 |
|2        | Sektor des ersten Datenblocks          | Sektor des ersten Datenblocks          | Sektor des ersten Datenblocks          | Sektor des RECORD Blocks               |
|3-18     | Dateiname (aufgefüllt mit SHIFT-SPACE) | Dateiname (aufgefüllt mit SHIFT-SPACE) | Dateiname (aufgefüllt mit SHIFT-SPACE) | Dateiname (aufgefüllt mit SHIFT-SPACE) |
|19       | unbenutzt                              | Spur des ersten Side-Sector-Blocks     | Spur des INFO Blocks                   | Spur des INFO Blocks                   |
|20       | unbenutzt                              | Sektor des ersten Side-Sector-Blocks   | Sektor des INFO Blocks                 | Sektor des INFO Blocks                 |
|21       | unbenutzt egal                         | Datensatzlänge                         | GEOS Dateistruktur                     | GEOS Dateistruktur                     |
|22       | unbenutzt immer=0                      | unbenutzt egal                         | GEOS Dateiart                          | GEOS Dateiart                          |
|23       | unbenutzt                              | unbenutzt                              | Jahr                                   | Jahr                                   |
|24       | unbenutzt                              | unbenutzt                              | Monat                                  | Monat                                  |
|25       | unbenutzt                              | unbenutzt                              | Tag                                    | Tag                                    |
|26       | unbenutzt                              | unbenutzt                              | Stunde                                 | Stunde                                 |
|27       | unbenutzt                              | unbenutzt                              | Minute                                 | Minute                                 |
|28       | Anzahl der verwendeten Blöcke (Low)    | Anzahl der verwendeten Blöcke (Low)    | Anzahl der verwendeten Blöcke (Low)    | Anzahl der verwendeten Blöcke (Low)    |
|29       | Anzahl der verwendeten Blöcke (High)   | Anzahl der verwendeten Blöcke (High)   | Anzahl der verwendeten Blöcke (High)   | Anzahl der verwendeten Blöcke (High)   |


## Wann ist eine Datei eine GEOS DATEI?

Eine Datei ist eine GEOS Datei, wenn
* die Dateiart gleich 1, 2 oder 3 ist
* und die GEOS Dateiart größer 0 ist
* und GEOS Dateistruktur gleich 0 oder 1 ist

Jede GEOS Datei benötigt auch einen gültigen Info Block. GEOS versucht schon beim Lesen einer Diskette auf dem DESK TOP den Info Block zu lesen. Ist keine korrekte Kombination aus Spur und Sektor angeben, wird eine Fehlermeldung ausgeben und der Vorgang abgebrochen. Kann der angeben Block gelesen werden, so werden aus diesem Block die Icon Information auf dem DESK TOP angezeigt. Dies ist auch der Fall, wenn es sich um keinen gültigen Info Block handelt. Aber spätestens beim Start einer Applikation muss ein gültiger Info Block vorhanden sein, da dieser Informationen zum Start einer Applikation beinhaltet. Eine Prüfung ob ein Info Block gültig ist kann nur durch GEOS selbst erfolgen. Eine Prüfung auf eine korrekte Kombination aus Spur und Sektor kann auch ohne von GEOS durchgeführt werden.

## Welche Informationen werden von einer Commodore DOS Datei mit der Dateiart SEQ, PRG, USR exportiert bzw. fließen in die Prüfsummenberechnung ein?

Von Commodore DOS Datei mit der Dateiart SEQ, PRG, USR werden nur die reinen Daten exportiert bzw. fließen in die Prüfsummenberechnung ein. Das bedeutet, dass **nur alle Datenblöcke** betrachtet werden. Es werden keine weiteren Informationen berücksichtigt. Das bedeutet, dass Informationen aus Directory Eintrag dem wie z.B. der Dateiname **nicht** in die Prüfsummenberechnung einfließen.

## Welche Informationen werden von einer Commodore DOS Datei mit der Dateiart REL exportiert bzw. fließen in die Prüfsummenberechnung ein?

Commodore DOS Dateien mir der Dateiart REL werden momentan noch nicht unterstützt!

## Wie werden die Prüfsummen von GEOS Dateien erstellt?

Um vergleichbare Prüfsummen für Geos Dateien zu erstellen, werden die Prüfsummen auf Basis von CleanCVT Dateien erstellt. D.h. die jeweilige Geos Datei wird intern in das CleanCVT Format umgewandelt und aus diesen Daten die Prüfsumme errechnet. Es ist dabei wichtig dass das CleanCVT Format zum Einsatz kommt, da sonst keine vergleichbare Prüfsumme entstehen würde. Unsaubere CVT Dateien von unterschiedlichen Disketten sind nicht vergleichbar, da diese Diskettenrelevante Informationen tragen (z.B. Startspuren und Startsektoren), welche nichts mit der eigentlichen Datei zu tun haben. 

## Welche Informationen werden von einer GEOS Datei mit der GEOS Dateistruktur SEQ exportiert bzw. fließen in die Prüfsummenberechnung ein?

* Directory Eintrag
  * Dateiart                              
  * Ersetzungs- Kennung                   
  * Schreibschutz- Kennung                
  * Offen- Kennung                        
  * Dateiname (aufgefüllt mit SHIFT-SPACE)
  * GEOS Dateistruktur                    
  * GEOS Dateiart                         
  * Jahr                                  
  * Monat                                 
  * Tag                                   
  * Stunde                                
  * Minute                                
  * Anzahl der verwendeten Blöcke (Low)   
  * Anzahl der verwendeten Blöcke (High)  
* Info Block (wird 1 zu 1 übernommen)
* Daten aller Datenblöcke (eine GEOS SEQ Datei besitzt nur genau eine Sektorenkette mit Datenblöcken) 

## Welche Informationen werden von einer GEOS Datei mit der GEOS Dateistruktur VLIR exportiert bzw. fließen in die Prüfsummenberechnung ein?

* Directory Eintrag
  * Dateiart                              
  * Ersetzungs- Kennung                   
  * Schreibschutz- Kennung                
  * Offen- Kennung                        
  * Dateiname (aufgefüllt mit SHIFT-SPACE)
  * GEOS Dateistruktur                    
  * GEOS Dateiart                         
  * Jahr                                  
  * Monat                                 
  * Tag                                   
  * Stunde                                
  * Minute                                
  * Anzahl der verwendeten Blöcke (Low)   
  * Anzahl der verwendeten Blöcke (High)  
* Info Block (wird 1 zu 1 übernommen)
* Record Block (entspricht nicht 1 zu 1 dem Record Block der GEOS Datei)
* Daten von Datensatz 1
* . . .
* Daten von Datensatz 127

# Welcher Algorithmus wird für das Erstellen der Prüfsumme verwendet und warum?

Die Prüfsummen werden mit Algorithmus **Message-Digest Algorithm 5** (kurz. **MD5**) berechnet. Dieser wird verwandt, da der Algorithmus Bestandteil des .NET Frameworks ist. Leider gibt es im .NET Framework keine CRC32 Implementierung, welche ansonsten zum Einsatz gekommen wäre. Eine eigene CRC32 Klasse bzw. Methode sollte absichtlich nicht in das Projekt aufgenommen werden, da diese Fehleranfällig sein könnte. Eine SHA-1 Prüfsumme wiederum ist einfach zu lang. 

# Testdokumentation

In der Solution ist ein Testprojekt enthalten, welches mehrere Testfunktionen zum Testen der Kernfunktionen beinhaltet. Alle verwendet Testdateien wie z.B. D64 Images, CVT Dateien usw. sind als *Resources* im Testprojekt eingebettet.

## Convert 2.5 erstellen

Das Programm CONVERT.BAS (http://cbmfiles.com/geos/geosfiles/CONVERT.BAS) wurde auf einer leeren von Vice (Version 3.0) erstellten Diskette ausgeführt. Es ist wichtig, dass die Diskette absolut leer ist.
Die Diskette wurde anschließend noch in das Geos Format konvertiert.

## Test

Für viele Tests werden die D64 Images aus dem Archiv GEOS64.ZIP von der Internetseite cbmfiles.com (http://cbmfiles.com/geos/geosfiles/GEOS64.ZIP) als Grundlage verwendet. Dieses Archiv enthält die folgenden 4 D64 Images.
*APPS64.D64  (MD5: 8EB414AB37B23A1D1D348D456896A1B0)
*GEOS64.D64  (MD5: F004B907634A30C21D4DF39E362C0789)
*SPELL64.D64  (MD5: 05425BE1824E99534CC30E60EDFF49C7)
*WRUTIL64.D64  (MD5: 3B52C7A91F794C0E5599AF804A515878)

```
┌─────────────────┬───────────────────────────┬───────────────────────────┬────────────────────────────────┐
│  GEOS SEQ/VLIR  │    CVT (dirty) Dateien    │     CVT (dirty) Dateien   │     Clean CVT Dateien          │
│  in D64 Images  │       in D64 Images       │     als PC Dateien        │     als PC Dateien             │
├─────────────────┴───────────────────────────┴───────────────────────────┴────────────────────────────────┤
│    O────────────────────────────────CDIExtract─────────────────────────────────────►O                    │
│    O────────────────────────────Star Commander 0.83────────────────────────────────►O                    │
│    O─────────────────pcGeos 0.3–GGET.EXE──────────────────►O ┐                                           │
│                            ┌ O─────────CDIExtract─────────►O │                                           │
│    O─────convert 2.5──────►┤ O────Star Commander 0.83─────►O ├────CVT2CleanCVT─────►O                    │
│                            └ O───u.v.a.m. z.B. 64Copy─────►O ┘                                           │
│                                                                                                          │
│    ║                                                       ║                        ║jegliche Programme  │
│    ║CDIDir                                                 ║CVTChecksum             ║zum Erzeugen von MD5│
│    ║                                                       ║                        ║Prüfsummen          │
│    ▼                                                       ▼                        ▼                    │
│   MD5 ◄∙─∙─∙─∙─∙─∙─∙─∙─∙─vergleichbar─∙─∙─∙─∙─∙─∙─∙─∙─∙─► MD5 ◄─∙─vergleichbar─∙─► MD5                   │
└──────────────────────────────────────────────────────────────────────────────────────────────────────────┘
```

#### Star Commander 0.83

Der **Star Commander** ist in der Lage, Geos Dateien aus D64 Images als CVT Dateien direkt zu extrahieren. Die dabei entstehenden CVT Dateien entsprechen wohl sogar dem CleanCVT Format.  Alle Dateien der 4 D64 Images *APPS64.D64*, *GEOS64.D64*, *SPELL64.D64* und *WRUTIL64.D64* wurden mit dem Star Commander 0.83 in CVT Dateien umgewandelt. Der Star Commander 0.83 konnte dabei auch die Dateien "GEOS" "GEOBOOT" "RBOOT" aus dem D64 Image GEOS64.D64 umwandeln, bei denen das Programm *Convert* Probleme hatte.

```
GEOS SEQ/VLIR                                          Clean CVT Dateien
in D64 Images                                          als PC Dateien
---------------------------                            --------------------
APPS64.D64                                           
0 "Applications    " ML 2A                           
120  "DESK TOP"         USR  --Star Commander 0.83-->  DESK TOP.cvt
141  "GEOWRITE"         USR  --Star Commander 0.83-->  GEOPAINT.cvt
152  "GEOPAINT"         USR  --Star Commander 0.83-->  GEOWRITE.cvt
41   "photo manager"    USR  --Star Commander 0.83-->  PHOTO MANAGER.CVT
15   "calculator"       USR  --Star Commander 0.83-->  CALCULATOR.CVT
19   "note pad"         USR  --Star Commander 0.83-->  NOTE PAD.CVT
26   "California"       USR  --Star Commander 0.83-->  California.cvt
23   "Cory"             USR  --Star Commander 0.83-->  Cory.cvt
13   "Dwinelle"         USR  --Star Commander 0.83-->  Dwinelle.cvt
34   "Roma"             USR  --Star Commander 0.83-->  Roma.cvt
40   "University"       USR  --Star Commander 0.83-->  University.cvt
7    "Commodore"        USR  --Star Commander 0.83-->  Commodore.cvt
9    "ReadMe"           USR  --Star Commander 0.83-->  ReadMe.cvt
23 BLOCKS FREE.                                      
   
GEOS SEQ/VLIR                                          Clean CVT Dateien
in D64 Images                                          als PC Dateien
---------------------------                            --------------------
GEOS64.D64                                           
0 "System          " 00 2A                           
2    "GEOS"             PRG  --Star Commander 0.83-->  GEOS.cvt
86   "GEOBOOT"          PRG  --Star Commander 0.83-->  GEOBOOT.cvt
78   "CONFIGURE"        USR  --Star Commander 0.83-->  CONFIGURE.cvt
120  "DESK TOP"         USR  --Star Commander 0.83-->  DESK TOP.cvt
3    "JOYSTICK"         USR  --Star Commander 0.83-->  JOYSTICK.cvt
5    "MPS-803"          USR  --Star Commander 0.83-->  MPS-803.cvt
22   "preference mgr"   USR  --Star Commander 0.83-->  PREFERENCE MGR.CVT
22   "pad color mgr"    USR  --Star Commander 0.83-->  PAD COLOR MGR.CVT
13   "alarm clock"      USR  --Star Commander 0.83-->  ALARM CLOCK.CVT
18   "PAINT DRIVERS"    USR  --Star Commander 0.83-->  PAINT DRIVERS.cvt
2    "RBOOT"            PRG  --Star Commander 0.83-->  RBOOT.cvt
4    "Star NL-10(com)"  USR  --Star Commander 0.83-->  Star NL-10(com).cvt
3    "ASCII Only"       USR  --Star Commander 0.83-->  ASCII Only.cvt
3    "COMM 1351"        USR  --Star Commander 0.83-->  COMM 1351.cvt
3    "COMM 1351(a)"     USR  --Star Commander 0.83-->  COMM 1351(a).cvt
20   "CONVERT"          USR  --Star Commander 0.83-->  CONVERT.cvt
259 BLOCKS FREE.                                     

GEOS SEQ/VLIR                                          Clean CVT Dateien
in D64 Images                                          als PC Dateien
---------------------------                            --------------------
SPELL64.D64                                          
0 "geoSpell        " 00 2A                           
120  "DESK TOP"         USR  --Star Commander 0.83-->  DESK TOP.cvt
111  "GEOSPELL"         USR  --Star Commander 0.83-->  GEOSPELL.cvt
387  "GeoDictionary"    USR  --Star Commander 0.83-->  GeoDictionary.cvt
45 BLOCKS FREE.                                      

GEOS SEQ/VLIR                                          Clean CVT Dateien
in D64 Images                                          als PC Dateien
---------------------------                            --------------------
WRUTIL64.D64                                         
0 "Write Utilities " 00 2A                           
120  "DESK TOP"         USR  --Star Commander 0.83-->  DESK TOP.cvt
67   "TEXT GRABBER"     USR  --Star Commander 0.83-->  TEXT GRABBER.cvt
60   "GEOLASER"         USR  --Star Commander 0.83-->  GEOLASER.cvt
67   "GEOMERGE"         USR  --Star Commander 0.83-->  GEOMERGE.cvt
38   "text manager"     USR  --Star Commander 0.83-->  TEXT MANAGER.CVT
4    "EasyScript Form"  USR  --Star Commander 0.83-->  EasyScript Form.cvt
3    "PaperClip Form"   USR  --Star Commander 0.83-->  PaperClip Form.cvt
2    "SpeedScript Form" USR  --Star Commander 0.83-->  SpeedScript Form.cvt
3    "WordWriter Form"  USR  --Star Commander 0.83-->  WordWriter Form.cvt
2    "Generic I Form"   USR  --Star Commander 0.83-->  Generic I Form.cvt
2    "Generic II Form"  USR  --Star Commander 0.83-->  Generic II Form.cvt
2    "Generic III Form" USR  --Star Commander 0.83-->  Generic III Form.cvt
44   "LW_Roma"          USR  --Star Commander 0.83-->  LW_Roma.cvt
44   "LW_Cal"           USR  --Star Commander 0.83-->  LW_Cal.cvt
46   "LW_Greek"         USR  --Star Commander 0.83-->  LW_Greek.cvt
49   "LW_Barrows"       USR  --Star Commander 0.83-->  LW_Barrows.cvt
110 BLOCKS FREE.     
```

#### pcGeos 0.3 – GGET.EXE

Das Programm **GGET.EXE** aus der Programmsammlung **pcGeos 0.3** ist, wie der *Star Commander*, in der Lage Geos Dateien aus D64 Images als CVT Dateien direkt zu extrahieren. Diese CVT Dateien werden leider *unsauber* (dirty) erstellt. Die Programme der Programmsammlung **pcGeos 0.3** haben auch noch einen weiteren Nachteil, diese können nur mit 8.3 Dateinamen im DOS Bereich arbeiten. Diese Einschränkung macht sich schon bei der Extraktion der Dateien der 4 D64 Images *APPS64.D64*, *GEOS64.D64*, *SPELL64.D64* und *WRUTIL64.D64* bemerkbar. Einige Dateien ergaben dabei die gleichen 8.3 Dateinamen, so dass diese manuell umbenannt werden mussten.

```
GEOS SEQ/VLIR                                 CVT (dirty) Dateien
in D64 Images                                 als PC Dateien
---------------------------                   --------------------
APPS64.D64                                           
0 "Applications    " ML 2A                           
120  "DESK TOP"         USR  --pcGeos 0.3-->  DESK_TOP.CVT
141  "GEOWRITE"         USR  --pcGeos 0.3-->  GEOWRITE.CVT
152  "GEOPAINT"         USR  --pcGeos 0.3-->  GEOPAINT.CVT
41   "photo manager"    USR  --pcGeos 0.3-->  PHOTO_MA.CVT
15   "calculator"       USR  --pcGeos 0.3-->  CALCULAT.CVT
19   "note pad"         USR  --pcGeos 0.3-->  NOTE_PAD.CVT
26   "California"       USR  --pcGeos 0.3-->  CALIFORN.CVT
23   "Cory"             USR  --pcGeos 0.3-->  CORY.CVT
13   "Dwinelle"         USR  --pcGeos 0.3-->  DWINELLE.CVT
34   "Roma"             USR  --pcGeos 0.3-->  ROMA.CVT
40   "University"       USR  --pcGeos 0.3-->  UNIVERSI.CVT
7    "Commodore"        USR  --pcGeos 0.3-->  COMMODOR.CVT
9    "ReadMe"           USR  --pcGeos 0.3-->  README.CVT
23 BLOCKS FREE.                                      
    
GEOS SEQ/VLIR                                 CVT (dirty) Dateien
in D64 Images                                 als PC Dateien
---------------------------                   --------------------
GEOS64.D64                                           
0 "System          " 00 2A                           
2    "GEOS"             PRG  --pcGeos 0.3-->  GEOS.CVT
86   "GEOBOOT"          PRG  --pcGeos 0.3-->  GEOBOOT.CVT
78   "CONFIGURE"        USR  --pcGeos 0.3-->  CONFIGUR.CVT
120  "DESK TOP"         USR  --pcGeos 0.3-->  DESK_TOP.CVT
3    "JOYSTICK"         USR  --pcGeos 0.3-->  JOYSTICK.CVT
5    "MPS-803"          USR  --pcGeos 0.3-->  MPS-803.CVT
22   "preference mgr"   USR  --pcGeos 0.3-->  PREFEREN.CVT
22   "pad color mgr"    USR  --pcGeos 0.3-->  PAD_COLO.CVT
13   "alarm clock"      USR  --pcGeos 0.3-->  ALARM_CL.CVT
18   "PAINT DRIVERS"    USR  --pcGeos 0.3-->  PAINT_DR.CVT
2    "RBOOT"            PRG  --pcGeos 0.3-->  RBOOT.CVT
4    "Star NL-10(com)"  USR  --pcGeos 0.3-->  STAR_NL-.CVT
3    "ASCII Only"       USR  --pcGeos 0.3-->  ASCII_ON.CVT
3    "COMM 1351"        USR  --pcGeos 0.3-->  COMM_135.CVT
3    "COMM 1351(a)"     USR  --pcGeos 0.3-->  COMM_13A.CVT umbenannt
20   "CONVERT"          USR  --pcGeos 0.3-->  CONVERT.CVT
259 BLOCKS FREE.                                     
 
GEOS SEQ/VLIR                                 CVT (dirty) Dateien
in D64 Images                                 als PC Dateien
---------------------------                   --------------------
SPELL64.D64                                          
0 "geoSpell        " 00 2A                           
120  "DESK TOP"         USR  --pcGeos 0.3-->  DESK_TOP.CVT
111  "GEOSPELL"         USR  --pcGeos 0.3-->  GEOSPELL.CVT
387  "GeoDictionary"    USR  --pcGeos 0.3-->  GEODICTI.CVT
45 BLOCKS FREE.                                      

GEOS SEQ/VLIR                                 CVT (dirty) Dateien
in D64 Images                                 als PC Dateien
---------------------------                   --------------------
WRUTIL64.D64                                         
0 "Write Utilities " 00 2A                           
120  "DESK TOP"         USR  --pcGeos 0.3-->  DESK_TOP.CVT
67   "TEXT GRABBER"     USR  --pcGeos 0.3-->  TEXT_GRA.CVT
60   "GEOLASER"         USR  --pcGeos 0.3-->  GEOLASER.CVT
67   "GEOMERGE"         USR  --pcGeos 0.3-->  GEOMERGE.CVT
38   "text manager"     USR  --pcGeos 0.3-->  TEXT_MAN.CVT
4    "EasyScript Form"  USR  --pcGeos 0.3-->  EASYSCRI.CVT
3    "PaperClip Form"   USR  --pcGeos 0.3-->  PAPERCLI.CVT
2    "SpeedScript Form" USR  --pcGeos 0.3-->  SPEEDSCR.CVT
3    "WordWriter Form"  USR  --pcGeos 0.3-->  WORDWRIT.CVT
2    "Generic I Form"   USR  --pcGeos 0.3-->  GENERIC1.CVT umbenannt
2    "Generic II Form"  USR  --pcGeos 0.3-->  GENERIC2.CVT umbenannt
2    "Generic III Form" USR  --pcGeos 0.3-->  GENERIC3.CVT umbenannt
44   "LW_Roma"          USR  --pcGeos 0.3-->  LW_ROMA.CVT
44   "LW_Cal"           USR  --pcGeos 0.3-->  LW_CAL.CVT
46   "LW_Greek"         USR  --pcGeos 0.3-->  LW_GREEK.CVT
49   "LW_Barrows"       USR  --pcGeos 0.3-->  LW_BARRO.CVT
110 BLOCKS FREE. 
```

#### convert 2.5 (und Star Commander 0.83)

Das wohl bekannteste Programm, welches CVT Dateien erzeugen kann, ist **Convert**, welches direkt unter Geos läuft. Dieses Programm konvertiert Geos SEQ Dateien als auch Geos VLIR in das CVT Format. Die erzeugten CVT Dateien ersetzten dabei die originalen Geos Dateien und befinden sich also immer noch in der D64 Image Datei. Um die Dateien aus den D64 Images zu extrahieren, kann eine Vielzahl von Programmen zum Einsatz kommen. Hier wurde der Star Commander in der Version 0.83 verwendet. Es ist aber z.B. auch 64Copy verwendbar. Die durch **Convert** erzeugten CVT Dateien haben den Nachteil, dass diese etwas *unsauber* (dirty) erstellt werden. CVT Dateien besitzen Datenstellen die für die Wiederherstellung einer Geos Datei, nicht benötigt werden. Diese Datenstellen sind in einem CVT, welches mittels **Convert** erstellt wurde, leider mit zufälligen Informationen gefüllt.
Alle Dateien der 4 D64 Images *APPS64.D64*, *GEOS64.D64*, *SPELL64.D64* und *WRUTIL64.D64* wurden also zuerst mit Convert 2.5 in das CVT Format umgewandelt und dann mit dem Star Commander 0.83 aus den Images extrahiert. Die Dateien "GEOS" "GEOBOOT" "RBOOT" aus dem D64 Image GEOS64.D64 konnten dabei, durch **Convert 2.5** nicht konvertiert werden. Dies liegt wahrscheinlich daran, dass diese 3 Dateien den falschen GEOS Dateiart besitzen. Dieser Fehler ist wohl entstanden, als der Kopierschutz entfernt wurde.

```
GEOS SEQ/VLIR                   CVT (dirty) Dateien                                  CVT (dirty) Dateien
in D64 Images                   in D64 Images                                        als PC Dateien
---------------------------     ---------------------------                          --------------------
APPS64.D64    --convert 2.5-->  APPS64_Convert2_5.D64                                
0 "Applications    " ML 2A      0 "Applications    " ML 2A
120  "DESK TOP"         USR --> 121  "DESK TOP"         PRG --Star Commander 0.83--> DESK TOP.prg
141  "GEOWRITE"         USR --> 142  "GEOWRITE"         PRG --Star Commander 0.83--> GEOWRITE.prg
152  "GEOPAINT"         USR --> 153  "GEOPAINT"         PRG --Star Commander 0.83--> GEOPAINT.prg
41   "photo manager"    USR --> 42   "PHOTO MANAGER"    PRG --Star Commander 0.83--> PHOTO MANAGER.prg   
15   "calculator"       USR --> 16   "CALCULATOR"       PRG --Star Commander 0.83--> CALCULATOR.prg
19   "note pad"         USR --> 20   "NOTE PAD"         PRG --Star Commander 0.83--> NOTE PAD.prg
26   "California"       USR --> 27   "CALIFORNIA"       PRG --Star Commander 0.83--> CALIFORNIA.prg
23   "Cory"             USR --> 24   "CORY"             PRG --Star Commander 0.83--> CORY.prg
13   "Dwinelle"         USR --> 14   "DWINELLE"         PRG --Star Commander 0.83--> DWINELLE.prg
34   "Roma"             USR --> 35   "ROMA"             PRG --Star Commander 0.83--> ROMA.prg
40   "University"       USR --> 41   "UNIVERSITY"       PRG --Star Commander 0.83--> UNIVERSITY.prg
7    "Commodore"        USR --> 8    "COMMODORE"        PRG --Star Commander 0.83--> COMMODORE.prg
9    "ReadMe"           USR --> 10   "README"           PRG --Star Commander 0.83--> README.prg
23 BLOCKS FREE.                 10 BLOCKS FREE.

GEOS SEQ/VLIR                   CVT (dirty) Dateien                                  CVT (dirty) Dateien
in D64 Images                   in D64 Images                                        als PC Dateien
---------------------------     ---------------------------                          -------------------- 
GEOS64.D64    --convert 2.5-->  GEOS64_Convert2_5.D64
0 "System          " 00 2A      0 "System          " 00 2A
2    "GEOS"             PRG Err 2    "GEOS"             PRG 
86   "GEOBOOT"          PRG Err 86   "GEOBOOT"          PRG
78   "CONFIGURE"        USR --> 79   "CONFIGURE"        PRG --Star Commander 0.83--> CONFIGURE.prg
120  "DESK TOP"         USR --> 121  "DESK TOP"         PRG --Star Commander 0.83--> DESK TOP.prg
3    "JOYSTICK"         USR --> 4    "JOYSTICK"         PRG --Star Commander 0.83--> JOYSTICK.prg
5    "MPS-803"          USR --> 6    "MPS-803"          PRG --Star Commander 0.83--> MPS-803.prg
22   "preference mgr"   USR --> 23   "PREFERENCE MGR"   PRG --Star Commander 0.83--> PREFERENCE MGR.prg
22   "pad color mgr"    USR --> 23   "PAD COLOR MGR"    PRG --Star Commander 0.83--> PAD COLOR MGR.prg
13   "alarm clock"      USR --> 14   "ALARM CLOCK"      PRG --Star Commander 0.83--> ALARM CLOCK.prg
18   "PAINT DRIVERS"    USR --> 19   "PAINT DRIVERS"    PRG --Star Commander 0.83--> PAINT DRIVERS.prg
2    "RBOOT"            PRG Err 2    "RBOOT"            PRG 
4    "Star NL-10(com)"  USR --> 5    "STAR NL-10(COM)"  PRG --Star Commander 0.83--> STAR NL-10(COM).prg
3    "ASCII Only"       USR --> 4    "ASCII ONLY"       PRG --Star Commander 0.83--> ASCII ONLY.prg
3    "COMM 1351"        USR --> 4    "COMM 1351"        PRG --Star Commander 0.83--> COMM 1351.prg
3    "COMM 1351(a)"     USR --> 4    "COMM 1351(A)"     PRG --Star Commander 0.83--> COMM 1351(A).prg
20   "CONVERT"          USR --> 21   "CONVERT"          PRG --Star Commander 0.83--> CONVERT.prg
259 BLOCKS FREE.                246 BLOCKS FREE.

GEOS SEQ/VLIR                   CVT (dirty) Dateien                                  CVT (dirty) Dateien
in D64 Images                   in D64 Images                                        als PC Dateien
---------------------------     ---------------------------                          --------------------
SPELL64.D64   --convert 2.5-->  SPELL64_Convert2_5.D64
0 "geoSpell        " 00 2A      0 "geoSpell        " 00 2A
120  "DESK TOP"         USR --> 121  "DESK TOP"         PRG --Star Commander 0.83--> DESK TOP.prg
111  "GEOSPELL"         USR --> 112  "GEOSPELL"         PRG --Star Commander 0.83--> GEOSPELL.prg
387  "GeoDictionary"    USR --> 388  "GEODICTIONARY"    PRG --Star Commander 0.83--> GEODICTIONARY.prg
45 BLOCKS FREE.                 42 BLOCKS FREE.

GEOS SEQ/VLIR                   CVT (dirty) Dateien                                  CVT (dirty) Dateien
in D64 Images                   in D64 Images                                        als PC Dateien
---------------------------     ---------------------------                          --------------------
WRUTIL64.D64  --convert 2.5-->  WRUTIL64_Convert2_5.D64
0 "Write Utilities " 00 2A      0 "Write Utilities " 00 2A
120  "DESK TOP"         USR --> 121  "DESK TOP"         PRG --Star Commander 0.83--> DESK TOP.prg
67   "TEXT GRABBER"     USR --> 68   "TEXT GRABBER"     PRG --Star Commander 0.83--> TEXT GRABBER.prg
60   "GEOLASER"         USR --> 61   "GEOLASER"         PRG --Star Commander 0.83--> GEOLASER.prg
67   "GEOMERGE"         USR --> 68   "GEOMERGE"         PRG --Star Commander 0.83--> GEOMERGE.prg
38   "text manager"     USR --> 39   "TEXT MANAGER"     PRG --Star Commander 0.83--> TEXT MANAGER.prg
4    "EasyScript Form"  USR --> 5    "EASYSCRIPT FORM"  PRG --Star Commander 0.83--> EASYSCRIPT FORM.prg
3    "PaperClip Form"   USR --> 4    "PAPERCLIP FORM"   PRG --Star Commander 0.83--> PAPERCLIP FORM.prg
2    "SpeedScript Form" USR --> 3    "SPEEDSCRIPT FORM" PRG --Star Commander 0.83--> SPEEDSCRIPT FORM.prg
3    "WordWriter Form"  USR --> 4    "WORDWRITER FORM"  PRG --Star Commander 0.83--> WORDWRITER FORM.prg
2    "Generic I Form"   USR --> 3    "GENERIC I FORM"   PRG --Star Commander 0.83--> GENERIC I FORM.prg
2    "Generic II Form"  USR --> 3    "GENERIC II FORM"  PRG --Star Commander 0.83--> GENERIC II FORM.prg
2    "Generic III Form" USR --> 3    "GENERIC III FORM" PRG --Star Commander 0.83--> GENERIC III FORM.prg
44   "LW_Roma"          USR --> 45   "LW_ROMA"          PRG --Star Commander 0.83--> LW_ROMA.prg
44   "LW_Cal"           USR --> 45   "LW_CAL"           PRG --Star Commander 0.83--> LW_CAL.prg
46   "LW_Greek"         USR --> 47   "LW_GREEK"         PRG --Star Commander 0.83--> LW_GREEK.prg
49   "LW_Barrows"       USR --> 50   "LW_BARROWS"       PRG --Star Commander 0.83--> LW_BARROWS.prg
110 BLOCKS FREE.                94 BLOCKS FREE.
```

#### CVT von der Internetseite cbmfiles.com

Auf der Internetseite cbmfiles.com finden sich eine Vielzahl von Geos Dateien im CVT Format. Einige von diesen müssten auch den Dateien auf den 4 D64 Images *APPS64.D64*, *GEOS64.D64*, *SPELL64.D64* und *WRUTIL64.D64* genau entsprechen. Die folgende Tabelle stellt die Dateinamen der 4 D64 Images den entsprechenden CVT Dateinamen gegenüber.

|D64 Image   |Nr.|Dateiname         |cbmfiles.com Dateiname                            |
|:-----------|:-:|:-----------------|:-------------------------------------------------|
|APPS64.D64  |   |                  |                                                  |
|            |1  |"DESK TOP"        |APPS64DT.CVT                                      |
|            |2  |"GEOWRITE"        |http://cbmfiles.com/geos/geosfiles/GW64.CVT       |
|            |3  |"GEOPAINT"        |http://cbmfiles.com/geos/geosfiles/GPT64.CVT      |
|            |4  |"photo manager"   |http://cbmfiles.com/geos/geosfiles/PHMGR64.CVT    |
|            |5  |"calculator"      |http://cbmfiles.com/geos/geosfiles/CALC64.CVT     |
|            |6  |"note pad"        |http://cbmfiles.com/geos/geosfiles/NOTE64.CVT     |
|            |7  |"California"      |http://cbmfiles.com/geos/geosfiles/CALIF.CVT      |
|            |8  |"Cory"            |http://cbmfiles.com/geos/geosfiles/CORY.CVT       |
|            |9  |"Dwinelle"        |http://cbmfiles.com/geos/geosfiles/DWIN.CVT       |
|            |10 |"Roma"            |http://cbmfiles.com/geos/geosfiles/ROMA.CVT       |
|            |11 |"University"      |http://cbmfiles.com/geos/geosfiles/UNIV.CVT       |
|            |12 |"Commodore"       |http://cbmfiles.com/geos/geosfiles/COMMFONT.CVT   |
|            |13 |"ReadMe"          |README.CVT                                        |
|GEOS64.D64  |   |                  |                                                  |
|            |14 |"GEOS"            |GEOS64.CVT                                        |
|            |15 |"GEOBOOT"         |BOOT64.CVT                                        |
|            |16 |"CONFIGURE"       |CONF64.CVT                                        |
|            |17 |"DESK TOP"        |GEOS64DT.CVT                                      |
|            |18 |"JOYSTICK"        |http://cbmfiles.com/geos/geosfiles/JOYSTICK.CVT   |
|            |19 |"MPS-803"         |http://cbmfiles.com/geos/geosfiles/MPS803.CVT     |
|            |20 |"preference mgr"  |http://cbmfiles.com/geos/geosfiles/PRMGR64.CVT    |
|            |21 |"pad color mgr"   |http://cbmfiles.com/geos/geosfiles/PDMGR64.CVT    |
|            |22 |"alarm clock"     |http://cbmfiles.com/geos/geosfiles/ALARM64.CVT    |
|            |23 |"PAINT DRIVERS"   |http://cbmfiles.com/geos/geosfiles/PNTDRVRS.CVT   |
|            |24 |"RBOOT"           |http://cbmfiles.com/geos/geosfiles/RBOOT.CVT      |
|            |25 |"Star NL-10(com)" |http://cbmfiles.com/geos/geosfiles/SNL10COM.CVT   |
|            |26 |"ASCII Only"      |http://cbmfiles.com/geos/geosfiles/ASC.CVT        |
|            |27 |"COMM 1351"       |http://cbmfiles.com/geos/geosfiles/COMM1351.CVT   |
|            |28 |"COMM 1351(a)"    |http://cbmfiles.com/geos/geosfiles/COM1351A.CVT   |
|            |29 |"CONVERT"         |CONVERT.CVT                                       |
|SPELL64.D64 |   |                  |                                                  |
|            |30 |"DESK TOP"        |SPEL64DT.CVT                                      |
|            |31 |"GEOSPELL"        |http://cbmfiles.com/geos/geosfiles/SPELL64.CVT    |
|            |32 |"GeoDictionary"   |http://cbmfiles.com/geos/geosfiles/DICT.CVT       |
|WRUTIL64.D64|   |                  |                                                  |
|            |33 |"DESK TOP"        |WRUT64DT.CVT                                      |
|            |34 |"TEXT GRABBER"    |http://cbmfiles.com/geos/geosfiles/TG64.CVT       |
|            |35 |"GEOLASER"        |http://cbmfiles.com/geos/geosfiles/GEOLASER.CVT   |
|            |36 |"GEOMERGE"        |http://cbmfiles.com/geos/geosfiles/GM64.CVT       |
|            |37 |"text manager"    |http://cbmfiles.com/geos/geosfiles/TXMGR64.CVT    |
|            |38 |"EasyScript Form" |http://cbmfiles.com/geos/geosfiles/TGESF64.CVT    |
|            |39 |"PaperClip Form"  |http://cbmfiles.com/geos/geosfiles/TGPCF64.CVT    |
|            |40 |"SpeedScript Form"|http://cbmfiles.com/geos/geosfiles/TGSSF64.CVT    |
|            |41 |"WordWriter Form" |http://cbmfiles.com/geos/geosfiles/TGWWF64.CVT    |
|            |42 |"Generic I Form"  |http://cbmfiles.com/geos/geosfiles/TGG1F64.CVT    |
|            |43 |"Generic II Form" |http://cbmfiles.com/geos/geosfiles/TGG2F64.CVT    |
|            |44 |"Generic III Form"|http://cbmfiles.com/geos/geosfiles/TGG3F64.CVT    |
|            |45 |"LW_Roma"         |http://cbmfiles.com/geos/geosfiles/LWROMA.CVT     |
|            |46 |"LW_Cal"          |http://cbmfiles.com/geos/geosfiles/LWCAL.CVT      |
|            |47 |"LW_Greek"        |http://cbmfiles.com/geos/geosfiles/LWGREEK.CVT    |
|            |48 |"LW_Barrows"      |http://cbmfiles.com/geos/geosfiles/LWBARR.CVT     |

```
┌────────────────┬──────────────────────┬─────────────────────┬─────────────────────────┐
│ GEOS SEQ/VLIR  │  CVT (dirty) Dateien │ CVT (dirty) Dateien │    Clean CVT Dateien    │
│ in D64 Images  │     in D64 Images    │   als PC Dateien    │     als PC Dateien      │
├────────────────┴──────────────────────┴─────────────────────┴─────────────────────────┤
│╔══════════════╗                                                                       │
│║4 D64 Images  ║                                                                       │
│║aus GEOS64.ZIP║                                                                       │
│╚═══╤═══╦══════╝                     Test                                              │
│    │   ╚═════════════════════════CDIExtract═══════════════════════════════►O          │
│    │                                                                       ▲          │
│    │                                                                       │          │
│    │                                                                 vergleichbar     │
│    │                                                                       │          │
│    │                                                                       ▼          │
│    │                                                                ┌──────────────┐  │
│    │                                                                │Alle Dateien  │  │
│    ├────────────────Star Commander 0.83────────────────────────────►│der 4 Images  │  │
│    │                                                                │als CleanCVT  │  │
│    │                                                                └──────────────┘  │
│    │                                                                       ▲          │
│    │                                                                       │          │
│    │                                                                 vergleichbar     │
│    │                                      ┌──────────────┐                 │          │
│    │                pcGeos 0.3            │Alle Dateien  │      Test       ▼          │
│    ├─────────────────GGET.EXE────────────►│der 4 Images  │══CVT2CleanCVT══►O          │
│    │                                      │als CVT       │                 ▲          │
│    │                                      └──────────────┘                 │          │
│    │                                             ▲                         │          │
│    │                                             │                         │          │
│    │                                     nicht vergleichbar          vergleichbar     │
│    │                                             │                         │          │
│    │                                             ▼                         │          │
│    │                                      ┌──────────────┐                 │          │
│    │                            Star      │Alle Dateien  │      Test       ▼          │
│    └──convert 2.5─────────►───Commander──►│der 4 Images  │══CVT2CleanCVT══►O          │
│                                 0.83      │als CVT       │                 ▲          │
│                                           └──────────────┘                 │          │
│                                                  ▲                         │          │
│                                                  │                         │          │
│                                             vergleichbar             vergleichbar     │
│                                                  │                         │          │
│                                                  ▼                         │          │
│                                           ╔══════════════╗                 │          │
│                                           ║viele Dateien ║      Test       ▼          │
│                                           ║der 4 Images  ║══CVT2CleanCVT══►O          │
│                                           ║als CVT       ║                            │
│                                           ╚══════════════╝                            │
│                                                                                       │
└───────────────────────────────────────────────────────────────────────────────────────┘
Legende:
  ╔═╗
  ║ ║ Dateien stammen von der Internetseite cbmfiles.com
  ╚═╝
  ┌─┐
  │ │ Dateien befinden sich in den Resources des Test Projektes
  └─┘
   O  Dateien werden zur Laufzeit des Tests erzeugt
  ══► Berechnung erfolgt zur Laufzeit der Test-Metoden
  ──► Berechnung ist vor dem Test durchgeführt worden, die entstanden Dateien
      befinden sich in den Resources des Test Projektes
   ▲
   │  vergleichbar / nicht vergleichbar
   ▼
```

# Installationsdokumentation

# ToDo Liste

* G64 Unterstützung
* D71 Unterstützung
* D81 Unterstützung
* .MD5 Dateien erstellen
* .MD5 Dateien überprüfen
* CleanCVT für CVT Dateien mit SEQ Dateien (Umwandlung von SEQ in PRG)
