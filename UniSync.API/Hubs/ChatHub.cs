using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Crypto;
using System.Text.RegularExpressions;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Features.Users.Queries;
using UniSync.Identity.Models;
using UniSync.Identity;
using UniSync.Application.Features.Messages;

namespace UniSync.Api.Hubs
{
    public class ChatHub : Hub
    {
        public Task Send(string data)
        {
            return Clients.All.SendAsync("Send", data);
        }
    }

}