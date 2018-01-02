using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace WCF_AuctionWebApplication
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AuctionsService : IAuctionsService
    {

        private static AuctionItemRepository _itemRepository = new AuctionItemRepository();
        private AuctionItemRepository ItemRepository
        {
            get
            {
                return _itemRepository;   // alternate of this static you can use HttpContext.Current.Application object to store 
            }
        }

        public string CreateAuctionItem(int itemNumber, string idemDescription, int ratingPrice)
        {
            AuctionItem v = ItemRepository.CreateAuctionItem(itemNumber, idemDescription, ratingPrice);
            if (v == null)
                return "OK";
            else
                return "NOT Created";
        }

        public List<AuctionItem> GetAllAuctionItems()
        {
            return ItemRepository.GetAllItems();
        }

        public AuctionItem GetAuctionItem(int itemNumber)
        {
            return ItemRepository.GetItem(itemNumber);
        }

        public string ProvideBid(int itemNumber, int bidPrice, string bidCustomName, string bidCustomPhone)
        {
            AuctionItem v = ItemRepository.GetItem(itemNumber);
            if (v == null)
                return "Item does not exist";
            return v.ProvideBid(bidPrice, bidCustomName, bidCustomPhone);
        }

    }
}
