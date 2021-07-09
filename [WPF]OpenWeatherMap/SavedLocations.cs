﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;

namespace _WPF_OpenWeatherMap
{
    public class SavedLocations
    {
        static string vrstica;
        static string vrsticaPreveri;
        static bool mestoObstaja = true;

        #region SaveCity metoda
        //Metoda, ki pridobi kot parameter ime mesta in ga shrani v beležko
        //PRED SHRANJEVANJEM MORA TUDI METODA PREGLEDATI ALI JE MESTO ŽE SHRANJENO BILO

        public static void SaveCity(string cityName)
        {
            //List, v katerem so shranjena vsa mesta
            List<string> listMest = new List<string>(); 

            //Preverimo, če obstaja datoteka shranjeni.txt
            if (File.Exists("shranjeni.txt"))
            {
                #region Pregledamo celo datoteko za določeno mesto
                //Če obstaja bomo prebrali vse zapise v datoteki
                using (StreamReader beri = new StreamReader("shranjeni.txt"))
                {
                    //Gremo skozi celo datoteko, in vsako vrstico posebej shranimo v spremenljivko 'vrstica'
                    while ((vrstica = beri.ReadLine()) != null)
                    {
                        //Če se nek zapis ujema z trenutnim mestom pomeni da ga imamo že shranjenega
                        if (vrstica.Equals(cityName))
                        {
                            mestoObstaja = true; //Natavimo bool na TRUE - kar pomeni da mesto obstaja
                            break; 
                        }
                        //Mesto še ni shranjeno, lahko ga zapišemo notri
                        else
                        {
                            mestoObstaja = false; //Mesto NE obstaja 
                        }
                    }
                    //StreamReader je potrebno zapreti preden lahko pišemo v datoteko!!!
                    beri.Close();
                }
                #endregion
                #region Če smo našli mesto ali nismo našli mesto
                //Če je mesto že shranjeno v datoteki - KLIK NA GUMB IZBRIŠI 
                //PODATEK SE MORA IZBRISATI IZ BELEŽKE 
                if (mestoObstaja)
                {
                    //Metoda kot parameter dobi mesto, katerega mora izbrisati iz beležke (oziroma iz shranjenih mest)
                    VnesiVBelezko(vrstica);

                    MessageBox.Show("Podatek je bil izbrisan!");
                }
                //ČE JE FALSE
                else
                {
                    StreamWriter pisi = File.AppendText("shranjeni.txt");
                    pisi.WriteLine(cityName);
                    pisi.Close();
                    MessageBox.Show("Mesto je bilo dodano!");
                }

                #endregion
            }
            //Če NE obstaja ta datoteka
            else
            {
                UstvariDatoteko(cityName);
                MessageBox.Show("Datoteka je bila ustvarjena!");
            }
        }
        #endregion
        #region Ustvari datoteko metoda
        /// <summary>
        /// Metoda s katero se ustvari datoteka 'shranjeni.txt' v kateri so vstavljene shranjene lokacije 
        /// </summary>
        public static void UstvariDatoteko(string mesto)
        {
            //Poskusimo ustvariti datoteko in notri vpisati izbrano mesto
            try
            {
                StreamWriter pisi = File.AppendText("shranjeni.txt");
                pisi.WriteLine(mesto);
                pisi.Close();
            }
            catch
            {
                MessageBox.Show("Prišlo je do napake pri ustvarjanju datoteke!", "Napaka!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion
        #region Metoda za spreminjanje napisa na gumbu "Shranjeni/Izbrisi"
        /// <summary>
        /// PREVERITI MORAMO ČE MESTO ŽE OBSTAJA - ČE OBSTAJA V DATOTEKI SE NAPIS NA GUMBU SPREMENI
        /// NAPIS SE TUDI MORA SPREMENITI NAZAJ KO IMAMO MESTO, KI ŠE NI DODANO
        /// 
        /// METODA SE MORA IZVESTI OB KLIKU NA GUMB SEARCH.
        /// </summary>
        /// <param name="mesto"></param>

        public static string SpremeniNapis(string mesto)
        {
            //List, v katerega bomo shranili vsa mesta
            List<string> shranjenaMesta = new List<string>();

            //StreamReader
            using (StreamReader beri = new StreamReader("shranjeni.txt"))
            {

                while ((vrsticaPreveri = beri.ReadLine()) != null)
                {
                    //Če se vnos NE ujema z shranjenimi mesti - pomeni da mesto nismo shranili ŠE
                    if(!vrsticaPreveri.Equals(mesto))
                    {
                        shranjenaMesta.Add(vrsticaPreveri);
                    }
                    else
                    {
                        return "Izbrisi";
                    }

                }

            }

            return "Shrani";
        }
        #endregion
        #region Vnos vseh priljubljenih v beležko
        static void VnesiVBelezko(string izbrisanoMesto)
        {
            //Deklaracija Lista, v kateremu bodo vsa mesta ki niso enaka izbrisanemu
            string novaVrstica;
            List<string> noviPodatki = new List<string>();

            //Sortiranje skozi celo datoteko
            using (StreamReader beri = new StreamReader("shranjeni.txt"))
            {
                while((novaVrstica = beri.ReadLine()) != null)
                {
                    if(!novaVrstica.Equals(izbrisanoMesto))
                    {
                        noviPodatki.dd(novaVrstica);
                    }
                }
            }

            //Pisanje Lista v obstoječo datoteko 
            StreamWriter pisi = File.AppendText("shranjeni.txt");
            for(int i = 0; i < noviPodatki.Count; i++)
            {
                pisi.WriteLine(noviPodatki[i]);
            }
            pisi.Close();

        }
        #endregion
    }
}
