using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionWebApplication.Models;

namespace AuctionWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            AuctionReference.AuctionsServiceClient client = new AuctionReference.AuctionsServiceClient();

            return View(await client.GetAllAuctionItemsAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            AuctionReference.AuctionsServiceClient client = new AuctionReference.AuctionsServiceClient();
            var item = await client.GetAuctionItemAsync(id);

            if (item != null)
            {
                return View(item);
            }
            else
            {
                Response.Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(AuctionReference.AuctionItem item)
        {
            AuctionReference.AuctionsServiceClient client = new AuctionReference.AuctionsServiceClient();
            var result = await client.ProvideBidAsync(item.ItemNumber, item.BidPrice, item.BidCustomName, item.BidCustomPhone);
            ViewBag.Response = result;
            return View();
        }

        public async Task<IActionResult> About(string name)
        {
            AuctionReference.AuctionsServiceClient client = new AuctionReference.AuctionsServiceClient();
            var result = await client.GetAllAuctionItemsAsync();
            var result2 = result.Where(x => x.BidCustomName == name);

            
            return View("Index", result2);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
