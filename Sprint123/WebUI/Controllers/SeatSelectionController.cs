using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SeatSelectionController : Controller
    {
        private IShowSeatRepository showSeatRepository;
        private IRoomRepository roomRepository;
        private ITempTicketRepository tempTicketRepository;

        public SeatSelectionController(IShowSeatRepository showSeatRepository, IRoomRepository roomRepository, ITempTicketRepository tempTicketRepository)
        {
            this.showSeatRepository = showSeatRepository;
            this.roomRepository = roomRepository;
            this.tempTicketRepository = tempTicketRepository;
        }

        [HttpGet]
        public ActionResult SelectSeats()
        {
            List<Ticket> ticketList = (List<Ticket>)TempData["Tickets"];
            Show show = ticketList.FirstOrDefault().Show;
            Room room = roomRepository.GetRoom(show.RoomID);
            IEnumerable<ShowSeat> showSeats = showSeatRepository.GetShowSeats(show.ShowID);
            int totalTickets = ticketList.Count();

            for (int i = 1; i <= room.RowCount; i++)
            {
                List<ShowSeat> currentRow = new List<ShowSeat>();
                int count = 0;
                // Trying to find somehwere where you can all sit next to eachother
                foreach (var j in showSeats)
                {
                    if (j.RowNumber == i && j.IsTaken == false && j.IsReserved == false)
                    {
                        currentRow.Add(j);
                        count++;
                    }
                }
                if (count >= totalTickets)
                {
                    List<TempTicket> tempTickets = tempTicketRepository.GetTempTicketsReservation(ticketList.FirstOrDefault().ReservationID).ToList();
                    for (int k = 0; k < totalTickets; k++)
                    {
                        ticketList[k].RowNumber = currentRow[k].RowNumber;
                        ticketList[k].SeatNumber = currentRow[k].SeatNumber;
                        ticketList[k].SeatID = currentRow[k].SeatID;
                        tempTickets[k].RowNumber = currentRow[k].RowNumber;
                        tempTickets[k].SeatNumber = currentRow[k].SeatNumber;
                        tempTickets[k].SeatID = currentRow[k].SeatID;
                        showSeats.Where(x => x.SeatID == currentRow[k].SeatID).FirstOrDefault().IsReserved = true;
                    }
                    showSeatRepository.UpdateShowSeats(showSeats.ToList());
                    tempTicketRepository.UpdateTempTickets(tempTickets);
                    break;
                } else if (i == room.RowCount)
                // If there aren't enough seats left where you can sit next to eachother
                {
                    List<TempTicket> tempTickets = tempTicketRepository.GetTempTicketsReservation(ticketList.FirstOrDefault().ReservationID).ToList();
                    currentRow.Clear();
                    count = 0;
                    foreach (var j in showSeats)
                    {
                        if (j.IsTaken == false && j.IsReserved == false)
                        {
                            currentRow.Add(j);
                            count++;
                            if (count == totalTickets)
                            {
                                break;
                            }
                        }
                    }

                    for (int k = 0; k < totalTickets; k++)
                    {
                        ticketList[k].RowNumber = currentRow[k].RowNumber;
                        ticketList[k].SeatNumber = currentRow[k].SeatNumber;
                        ticketList[k].SeatID = currentRow[k].SeatID;
                        tempTickets[k].RowNumber = currentRow[k].RowNumber;
                        tempTickets[k].SeatNumber = currentRow[k].SeatNumber;
                        tempTickets[k].SeatID = currentRow[k].SeatID;
                        showSeats.Where(x => x.SeatID == currentRow[k].SeatID).FirstOrDefault().IsReserved = true;
                    }
                    showSeatRepository.UpdateShowSeats(showSeats.ToList());
                    tempTicketRepository.UpdateTempTickets(tempTickets);
                }
                currentRow.Clear();
            }

            SeatLayout seatLayout = new SeatLayout();
            seatLayout.showSeats = showSeats;
            seatLayout.rowCount = room.RowCount;
            TempData["TicketList"] = ticketList;
            return View("SelectSeats", seatLayout);
        }
        
        [HttpGet]
        public ActionResult ChooseSeats()
        {
            List<Ticket> ticketList = (List<Ticket>)TempData["Tickets"];
            return RedirectToAction("Pay", "Pin", ticketList);
        }
    }
}