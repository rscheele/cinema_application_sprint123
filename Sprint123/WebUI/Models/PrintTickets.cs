﻿using Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class PrintTickets
    {
        private Document doc;
        private PdfWriter writer;
        private MemoryStream ms;
        private Font normalFont;
        private Font largeFont;
        private Font smallFont;
        private string culture;

        public PrintTickets(List<Ticket> tickets)
        {
            normalFont = FontFactory.GetFont("Segoe UI", 8.0f, BaseColor.BLACK);
            smallFont = FontFactory.GetFont("Segoe UI", 6.0f, BaseColor.BLACK);
            largeFont = FontFactory.GetFont("Segoe UI", 10.0f, BaseColor.BLACK);

            doc = new Document(PageSize.A6);
            ms = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, ms);
            Write(tickets);
        }

        public bool Write(List<Ticket> tickets)
        {
            writer.Open();
            doc.Open();
            string location = "Breda";

            bool first = true;
            foreach (Ticket t in tickets)
            {
                if (first == false)
                {
                    doc.NewPage();
                }
                first = false;
                // Location
                addText("Locatie: " + location, largeFont);

                AddEmptyLine();
                AddEmptyLine();

                // ticket info
                addText("TicketID: " + t.TicketID);
                addText("TicketType: " + t.TicketType);
                addText("Reserveringsnummer: " + t.ReservationID);

                string sub;
                if (t.Show.Movie.LanguageSub == null)
                {
                    sub = "Geen";
                }
                else
                {
                    sub = t.Show.Movie.LanguageSub;
                }

                addText("Film: " + t.Show.Movie.Name);
                addText("Taal: " + t.Show.Movie.Language);
                addText("Ondertiteling: " + sub);
                addText("Leeftijd: " + t.Show.Movie.Age);
                addText("Duur: " + t.Show.Movie.Length + " minuten");
                addText("3D: " + (t.Show.Movie.Is3D ? "Ja" : "Nee"));
                addText("Prijs: €" + t.Price.ToString());


                /// maandag 1 Februari 2017
                addText("Datum: " + t.Show.BeginTime.ToString("dddd d MMMM yyyy"), smallFont);
                // 23:45
                addText("Begintijd: " + t.Show.BeginTime.ToString("HH:mm"), smallFont);
                addText("Eindtijd: " + t.Show.EndTime.ToString("HH:mm"), smallFont);
                addText("Locatie: " + location, smallFont);
                addText("Zaal: " + t.Show.RoomID, smallFont);
                AddEmptyLine();
                addText("Rijnummer  : " + t.RowNumber);
                addText("Stoelnummer: " + t.SeatNumber);
                AddEmptyLine();
                addText("Met popcorn arrangement: " + (t.Popcorn ? "Ja" : "Nee"));

                //addText("Stoelnummer: " + t.Seat.SeatNumber, smallFont);
                //addText("Rijnummer: " + t.Seat.RowY, smallFont);
            }

            doc.Close();
            writer.Close();
            return true;
        }

        public MemoryStream getMemoryStream()
        {
            return ms;
        }

        public void addText(String txt, Font font)
        {
            doc.Add(new Paragraph(txt, font));
        }

        public void addText(String txt)
        {
            doc.Add(new Paragraph(txt, normalFont));
        }

        public void AddEmptyLine()
        {
            addText(" ");
        }

        public ActionResult SendPdf()
        {
            MemoryStream stream = getMemoryStream();
            FileStreamResult result = new FileStreamResult(new MemoryStream(stream.GetBuffer()), "pdf/application");
            result.FileDownloadName = "image.pdf";
            return result;
        }
    }
}