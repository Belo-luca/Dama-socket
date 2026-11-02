using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama
{
    public partial class Form1 : Form
    {
        string PBianca = "pedinaBianca.png";               //Memorizza il nome del file immagine per la pedina bianca.
        string PBiancaCorona = "pedinaBiancaCorona.png";   //Memorizza il nome del file immagine per la pedina bianca con la corona.
        string PNera = "pedinaNera.png";                   //Memorizza il nome del file immagine per la pedina nera.
        string PNeraCorona = "pedinaNeraCorona.png";       //Memorizza il nome del file immagine per la pedina nera con la corona.

        string PCoordVecchio;   //Coordinata vecchia della pedina selezionata
        string PCoordNuovo;     //Coordinata nuova della pedina selezionata

        PictureBox[] caselle;   //dichiaro l'array caselle

        public Form1()
        {
            InitializeComponent();

            //array di tutte le picturebox
            caselle = new PictureBox[]
            {
                A1, A2, A3, A4, A5, A6, A7, A8, //fila A
                B1, B2, B3, B4, B5, B6, B7, B8, //fila B
                C1, C2, C3, C4, C5, C6, C7, C8, //fila C
                D1, D2, D3, D4, D5, D6, D7, D8, //fila D
                E1, E2, E3, E4, E5, E6, E7, E8, //fila E
                F1, F2, F3, F4, F5, F6, F7, F8, //fila F
                G1, G2, G3, G4, G5, G6, G7, G8, //fila G
                H1, H2, H3, H4, H5, H6, H7, H8  //fila H
            };

            //per ogni PictureBox nell'array delle caselle
            foreach (PictureBox pb in caselle)
            {
                pb.Click += PictureBox_Click;     // Associa l'evento Click della PictureBox al metodo PictureBox_Click
            }

            PictureBox[] rigaA = { A1, A2, A3, A4, A5, A6, A7, A8 };     //array della riga A
            PictureBox[] rigaB = { B1, B2, B3, B4, B5, B6, B7, B8 };    //array della riga B
                                                                        //            PictureBox[] rigaC = { C1, C2, C3, C4, C5, C6, C7, C8 };    //array della riga C (può non servire?)
                                                                        //            PictureBox[] rigaD = { D1, D2, D3, D4, D5, D6, D7, D8 };    //array della riga D (può non servire?)
                                                                        //            PictureBox[] rigaE = { E1, E2, E3, E4, E5, E6, E7, E8 };    //array della riga E (può non servire?)
                                                                        //            PictureBox[] rigaF = { F1, F2, F3, F4, F5, F6, F7, F8 };    //array della riga F (può non servire?)
            PictureBox[] rigaG = { G1, G2, G3, G4, G5, G6, G7, G8 };    //array della riga G
            PictureBox[] rigaH = { H1, H2, H3, H4, H5, H6, H7, H8 };    //array della riga H

            //mette l'immagine della pedina bianca nella riga A
            foreach (PictureBox pb in rigaA)
            {
                pb.Image = Image.FromFile(PBianca);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //mette l'immagine della pedina bianca nella riga B
            foreach (PictureBox pb in rigaB)
            {
                pb.Image = Image.FromFile(PBianca);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //mette l'immagine della pedina nera nella riga G
            foreach (PictureBox pb in rigaG)
            {
                pb.Image = Image.FromFile(PNera);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }

            //mette l'immagine della pedina nera nella riga H
            foreach (PictureBox pb in rigaH)
            {
                pb.Image = Image.FromFile(PNera);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender; //salva nella variabile pb la casella che è stata cliccata

            //se una PictureBox ha una immagine
            if (pb.Image != null)
            {
                //se una PictureBox ha lo sfondo verde
                if (pb.BackColor == Color.Green)
                {
                    pb.BackColor = Color.Transparent;   //mette lo sfondo trasparente
                    PCoordVecchio = null;               //mette null la variabile PCoordVecchio
                }
                else
                {
                    //se la variabile PCoordVecchio è null
                    if (PCoordVecchio == null)
                    {
                        pb.BackColor = Color.Green; //mette lo sfondo verde
                        PCoordVecchio = pb.Name;    //prende la coordinata della pedina selezionata e lo mette nella variabile PCoordVecchio
                    }
                    else
                    {
                        //per ogni picturebox in caselle
                        foreach (PictureBox pbt in caselle)
                        {
                            pbt.BackColor = Color.Transparent;  //mette tutte le picturebox con lo sfondo trasparente
                            pb.BackColor = Color.Green;         //mette la picturebox selezionata con lo sfondo verde
                        }
                    }
                }
            }
            //se una picturebox non ha l'immagine
            else
            {
                //se la variabile PCoordVecchio contiene qualcosa
                if (PCoordVecchio != null)
                {
                    PictureBox pictureBoxTrovata = null;    //variabile della picturebox che ci serve

                    //per ogni picturebox in caselle
                    foreach (PictureBox pbc in caselle)
                    {
                        //se il nome della picturebox presa in questo momento è uguale alla variabile PCoordVecchio
                        if (pbc.Name == PCoordVecchio)
                        {
                            pictureBoxTrovata = pbc;        //variabile della picturebox che ci serve è uguale alla picturebox presa in questo momento
                            break;                          //interrompiamo il ciclo del foreach
                        }
                    }

                    //se la variabile della picturebox che ci serve non è vuota
                    if (pictureBoxTrovata != null)
                    {
                        pb.Image = pictureBoxTrovata.Image;             //l'immagine della picturebox selezionata sarà uguale alla picturebox selezionata precedentemente
                        pb.SizeMode = pictureBoxTrovata.SizeMode;       //l'ingrandimento della picturebox selezionata sarà uguale alla picturebox selezionata precedentemente

                        pictureBoxTrovata.Image = null;                 //la picturebox selezionata precedentemente non avrà nessuna immagine

                        //per ogni picturebox in caselle
                        foreach (PictureBox pbt in caselle)
                        {
                            pbt.BackColor = Color.Transparent;          //mette tutte le picturebox con lo sfondo trasparente
                        }

                        PCoordVecchio = null;                           //mette null la variabile PCoordVechhio
                    }
                }
            }
        }

        private void A1_Click(object sender, EventArgs e) { }

        private void pictureBox19_Click(object sender, EventArgs e) { }
        private void pictureBox22_Click(object sender, EventArgs e) { }
    }
}
