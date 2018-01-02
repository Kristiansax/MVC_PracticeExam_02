using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF_AuctionWebApplication
{
    public class AuctionItemRepository
    {
        private List<AuctionItem> _itemList = new List<AuctionItem>();
        private object _itemListLock = new object();
        public AuctionItemRepository()
        {
            _itemList.Add(new AuctionItem(1011, "Defect Comodore 64", 50));
            _itemList.Add(new AuctionItem(1023, "Pink office chair", 500));
            _itemList.Add(new AuctionItem(1035, "Gey PH lamp", 1500));
            _itemList.Add(new AuctionItem(1047, "Funny christmas calendar", 37));
            _itemList.Add(new AuctionItem(1059, "Not used java book", 200));
        }

        public List<AuctionItem> GetAllItems()
        {
            lock (_itemListLock)
            {
                return new List<AuctionItem>(_itemList);  // copy of list with references made while locked
            }
        }

        public AuctionItem GetItem(int itemNumber)
        {
            lock (_itemListLock)
            {
                foreach (AuctionItem v in _itemList)   // find while locked for changing list
                {
                    if (v.ItemNumber == itemNumber)
                        return v;
                }
            }
            return null;
        }
        public AuctionItem CreateAuctionItem(int itemNumber, string itemDecription, int ratingPrice)
        {
            lock (_itemListLock)
            {
                int index = 0;
                while (index < _itemList.Count && _itemList[index].ItemNumber != itemNumber)   // find while locked
                {
                    ++index;
                }
                if (index < _itemList.Count)
                    return null;                    // already exist !!! return value for this
                else
                {
                    AuctionItem newVare = new AuctionItem(itemNumber, itemDecription, ratingPrice);
                    _itemList.Add(newVare);
                    return newVare;
                }

            }
        }
    }
}