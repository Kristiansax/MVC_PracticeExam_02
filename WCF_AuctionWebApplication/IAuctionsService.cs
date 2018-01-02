using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_AuctionWebApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuctionsService" in both code and config file together.
    [ServiceContract]
    public interface IAuctionsService
    {
        [OperationContract]
        List<AuctionItem> GetAllAuctionItems();

        [OperationContract]
        AuctionItem GetAuctionItem(int itemNumber);

        [OperationContract]
        string CreateAuctionItem(int itemNumber, string idemDescription, int ratingPrice);

        [OperationContract]
        string ProvideBid(int itemNumber, int bidPrice, string bidCustomName, string bidCustomPhone);
    }
}
