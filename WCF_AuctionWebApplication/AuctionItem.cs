using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCF_AuctionWebApplication
{
    [DataContract]
    public class AuctionItem
    {
        [DataMember]
        public int ItemNumber { get; private set; }

        [DataMember]
        public string ItemDescription { get; private set; }

        [DataMember]
        public int RatingPrice { get; private set; }

        [DataMember]
        public int BidPrice { get; private set; }

        [DataMember]
        public string BidCustomName { get; private set; }

        [DataMember]
        public string BidCustomPhone { get; private set; }

        [DataMember]
        public DateTime BidTimestamp { get; private set; }


        private object _bidLock = new object();

        public AuctionItem(int itemNumber, string itemDescription, int ratingPrice)
        {
            this.ItemNumber = itemNumber;
            this.ItemDescription = itemDescription;
            this.RatingPrice = ratingPrice;
        }

        public string ProvideBid(int bidPrice, string bidCustomName, string bidCustomPhone)
        {
            lock (_bidLock)
            {
                if (this.BidPrice < bidPrice)
                {
                    this.BidPrice = bidPrice;
                    this.BidCustomName = bidCustomName;
                    this.BidCustomPhone = bidCustomPhone;
                    this.BidTimestamp = DateTime.Now;
                    return "OK";
                }
                else
                {
                    return "Bid to low";
                }
            }
        }
    }
}