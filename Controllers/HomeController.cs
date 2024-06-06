using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AvoralTraders.Models;
using System.Net;
using System.Net.Sockets;

namespace AvoralTraders.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
        => View();

    public IActionResult App()
    {
        ViewBag.HostName = Dns.GetHostName();
        ViewBag.NetworkAddr = GetNetworkAddresses();
        ViewBag.EnvironmentName = GetEnvironmentName();
        return View();
    }

    private static string GetEnvironmentName()
        => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";

    private static string[] GetNetworkAddresses()
    {
        string hostname = Dns.GetHostName();

        IPHostEntry hostEntry = 
            Dns.GetHostEntry(hostname, AddressFamily.InterNetwork);

        return hostEntry.AddressList.Select(x => $"{x}").ToArray();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
