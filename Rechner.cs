using System;
using System.Linq;

public class Rechner
{
    DateTime entry_Date;
    DateTime exit_Date;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("===== KündigunsFristenRechner =====");
            Menu();
        }
    }

    void Menu()
    {
        //Einstiegsdatum erfassen
        entry_Date = EingabeDatum(true);

        //Kündigungsdatum erfassen
        exit_Date = EingabeDatum(false);

        //Datumswerte Überprüfen 
        if (DateTime.Compare(entry_Date, exit_Date) == 1)
        {
            Console.WriteLine("Kündigungsdatum liegt nicht in der Zukunft!");
            return;
        }

        //Eingaben formatiert Ausgeben
        Console.Write("\n["); //klammer auf
        FarbigeAusgabe("i", ConsoleColor.Blue, false); // farbiger text
        Console.Write("]\tEinstiegsdatum:\t"); //klammer Zu
        FarbigeAusgabe($"{entry_Date:MMMM} ", ConsoleColor.DarkYellow, false);
        FarbigeAusgabe($"{entry_Date:yyyy} {Environment.NewLine}", ConsoleColor.DarkMagenta, false);


        Console.Write("["); //klammer auf
        FarbigeAusgabe("i", ConsoleColor.Blue, false); // farbiger text
        Console.Write("]\tAusstiegsdatum:\t"); //klammer Zu
        FarbigeAusgabe($"{exit_Date:MMMM} ", ConsoleColor.Yellow, false);
        FarbigeAusgabe($"{exit_Date:yyyy} {Environment.NewLine}", ConsoleColor.Magenta, false);

        //Kündigungsfrist Berechnen
        Console.Write("[");
        FarbigeAusgabe("i", ConsoleColor.Blue, false);
        Console.WriteLine("]\tBerechne Kündigungsfrist ...");
        int frist = Berechnen(entry_Date, exit_Date);

        Console.Write("\nDer Angetellte hat ");
        FarbigeAusgabe((exit_Date.Year - entry_Date.Year).ToString(), ConsoleColor.Magenta, false);
        Console.WriteLine(" Jahre gearbeitet!");
        Console.Write("Die Kündigungsfrist Beträgt ");
        FarbigeAusgabe(frist.ToString(), ConsoleColor.DarkYellow, false);
        Console.WriteLine(" Monat(e)!");
        Console.WriteLine("===== Rechner ENDE =====\n\n");
    }


    /// <summary>
    /// Fordert den Benzutzer zur eingabe eines Datums auf
    /// </summary>
    /// <param name="typ">TRUE: Einstiegsdatum, FALSE: Kündigungsdatum</param>
    /// <returns></returns>
    DateTime EingabeDatum(bool typ)
    {

        //Datumseinheiten zur Eingabe
        int month = 0;
        int year = 0;
        string input;
        bool valid = false;

        Range monthRange = new Range(0, 12);
        Range YearRange = new Range(0, 9999);


        while (!valid)
        {
            //Konsolentext
            string consoleText = "";
            if (typ == true)
                consoleText += "Einstiegsdatum";
            else
                consoleText += "Gewünschtes Küdigungsdatum";

            consoleText += " eingeben ( mm yyyy ): ";
            Console.Write(consoleText);

            //Eingabe lesen
            Console.ForegroundColor = ConsoleColor.Blue;
            input = Console.ReadLine();
            Console.ResetColor();

            //Datum auf Fehler überprüfen
            try
            {
                //Monat
                try
                {
                    month = int.Parse(input.Substring(0, 2));
                    if (!(month >= monthRange.Start.Value && month <= monthRange.End.Value)) throw new Exception();
                }
                catch (System.Exception)
                {
                    throw new Exception("Monat hat Falsches format");
                }

                //Jahr
                try
                {
                    year = int.Parse(input.Substring(3));
                    if (!(year >= YearRange.Start.Value && year <= YearRange.End.Value)) throw new Exception();
                }
                catch (System.Exception)
                {
                    throw new Exception("Jahr hat Falsches format");
                }
                valid = true;
            }
            catch (Exception ex)
            {
                FarbigeAusgabe($"Du machst etwas Falsch: {ex.Message} ", ConsoleColor.Red);
            }
        }
        return new DateTime(year, month, 1);
    }


    /// <summary>
    /// Gibt einen Text Farbig in der Konsole aus
    /// </summary>
    /// <param name="line">True: Console.Writeline FALSE: Console.Write</param>
    /// <param name="text">Der Text der Ausgegeben wird</param>
    /// <param name="color">Die Farbe der Ausgabe in der Konsole</param>
    void FarbigeAusgabe(string text, ConsoleColor color, bool line = true)
    {
        if (line)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }
        else
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }
        Console.ResetColor();

    }


    /// <summary>
    /// Berechnet die Kündigungsfrist einens Angestellten
    /// </summary>
    /// <param name="_entryDate"></param>
    /// <param name="_exitDate"></param>
    /// <returns>Kündigungsfrist in Monaten</returns>
    int Berechnen(DateTime _entryDate, DateTime _exitDate)
    {
        static bool inRange(int integer, int min, int max)
        {
            if (integer >= min && integer < max) return true;
            else return false;
        }

        int jahre = _exitDate.Year - _entryDate.Year;
        int frist = 0;
        if (inRange(jahre, 0, 5)) // zwei Zahre 
        {
            frist = 1;
        }
        else
        if (inRange(jahre, 5, 8)) // 5 Zahre 
        {
            frist = 2;
        }
        else
        if (inRange(jahre, 8, 10)) // 8 Zahre 
        {
            frist = 3;
        }
        else
        if (inRange(jahre, 10, 12)) // 8 Zahre 
        {
            frist = 4;
        }
        else
        if (inRange(jahre, 12, 15)) // 12 Zahre 
        {
            frist = 5;
        }
        else
        if (inRange(jahre, 15, 20)) // 15 Zahre 
        {
            frist = 6;
        }
        else
        if (jahre >= 20)
        {
            frist = 7;
        }
        return frist;
    }

}