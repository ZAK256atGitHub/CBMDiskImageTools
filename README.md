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

```
┌►tsets       ┌►
│             │  
└─njsnsandsa  │ 
kdmkfmsdklfls ┌► 
```

```html
<table class=MsoTableGrid border=1 cellspacing=0 cellpadding=0 width=729
 style='width:546.4pt;border-collapse:collapse;border:none'>
 <tr>
  <td width=28 valign=top style='width:21.05pt;border:solid windowtext 1.0pt;
  border-bottom:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=104 valign=top style='width:78.15pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:none;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:none;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=238 colspan=2 valign=top style='width:178.5pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:none;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Geos</span></p>
  </td>
  <td width=158 colspan=4 valign=top style='width:118.55pt;border-top:solid windowtext 1.0pt;
  border-left:none;border-bottom:none;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>Commodore
  DOS</span></p>
  </td>
 </tr>
 <tr>
  <td width=28 valign=top style='width:21.05pt;border:solid windowtext 1.0pt;
  border-top:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>GEOS file
  structure</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border:solid windowtext 1.0pt;
  border-left:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New";color:red;
  background:yellow'>SEQ/PRG/USR???</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>VLIR</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border:solid windowtext 1.0pt;
  border-left:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New";color:red;
  background:yellow'>SEQ/PRG/USR???</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Sequential</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border:solid windowtext 1.0pt;
  border-left:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>SEQ</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border:solid windowtext 1.0pt;
  border-left:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>PRG</span></p>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border:solid windowtext 1.0pt;
  border-left:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>USR</span></p>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border:solid windowtext 1.0pt;
  border-left:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>REL</span></p>
  <p class=MsoNoSpacing><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=28 rowspan=8 valign=top style='width:21.05pt;border:solid windowtext 1.0pt;
  border-top:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal align=center style='margin-top:0cm;margin-right:5.65pt;
  margin-bottom:0cm;margin-left:5.65pt;margin-bottom:.0001pt;text-align:center;
  line-height:normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Directory
  Eintrag</span></p>
  </td>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Bit 0-3:File
  typ</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Bit 5:Used
  only</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>  during
  SAVE-@ replacement</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Bit 6:Locked
  flag (Set produces &quot;&gt;&quot; locked files)</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Bit 7:Closed flag
  (Not  set  produces  &quot;*&quot;, or &quot;splat&quot; files)</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Filetype</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New";color:red;
  background:yellow'>SEQ/PRG/USR???</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Track/sector
  location of first sector of file</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>SEQ </span><span
  style='font-size:7.0pt;font-family:Wingdings'>à</span><span style='font-size:
  7.0pt;font-family:"Courier New"'> Starting track/sector</span></p>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>VLIR </span><span
  style='font-size:7.0pt;font-family:Wingdings'>à</span><span style='font-size:
  7.0pt;font-family:"Courier New"'> Record Block</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>filename</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Filename</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Track/Sector
  location of first side-sector block (REL file only)</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Track/sector
  location of info block</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>REL file
  record length (REL file only, max. value 254)</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>GEOS file
  structure</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Unused</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>GEOS filetype</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Unused</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Year/Month/Day/Hour/Minute</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>File size</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>Filesize</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=28 valign=top style='width:21.05pt;border:solid windowtext 1.0pt;
  border-top:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=28 valign=top style='width:21.05pt;border:solid windowtext 1.0pt;
  border-top:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
 <tr>
  <td width=28 valign=top style='width:21.05pt;border:solid windowtext 1.0pt;
  border-top:none;background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=104 valign=top style='width:78.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=200 valign=top style='width:150.15pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  background:#BDD6EE;padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.1pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=119 valign=top style='width:89.4pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:29.9pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=41 valign=top style='width:30.5pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=40 valign=top style='width:30.1pt;border-top:none;border-left:none;
  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
  <td width=37 valign=top style='width:28.05pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0cm 5.4pt 0cm 5.4pt'>
  <p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:
  normal'><span style='font-size:7.0pt;font-family:"Courier New"'>&nbsp;</span></p>
  </td>
 </tr>
</table>
```

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

# Testdokumentation

In der Solution ist ein Testprojekt enthalten, welches mehere Testfunktionen zum Testen der Kernfunktionen beinhaltet.

# Installationsdokumentation