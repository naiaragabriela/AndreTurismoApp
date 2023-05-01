﻿using System.Net.Sockets;
using AndreTurismoApp.ExternalService;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ExternalTicketService _ticketService;

        public TicketController(ExternalTicketService service)
        {
            _ticketService = service;
        }

        [HttpPost(Name = "InsertTicket")]
        public async Task<ActionResult> Add(Ticket ticket)
        {
            var statusCode = (int)await _ticketService.PostTicket(ticket);

            return StatusCode(statusCode);
        }

        [HttpGet(Name = "GetAllTicket")]
        public async Task<List<Ticket>> GetAll()
        {
            var response = await _ticketService.GetTicket();
            return response;
        }

        [HttpPut(Name = "UpdateTicket")]
        public async Task<ActionResult> Update(Ticket ticket)
        {
            var statusCode = (int)await _ticketService.PutTicket(ticket);

            return StatusCode(statusCode);
        }

        [HttpDelete(Name = "DeleteTicket")]
        public async Task<ActionResult> Delete(int id)
        {
            var statusCode = (int)await _ticketService.DeleteTicket(id);

            return StatusCode(statusCode);
        }
    }
}
