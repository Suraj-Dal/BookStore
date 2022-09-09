using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRL
    {
        public bool AddToWishlist(int BookID, int UserID);
        public bool DeleteWishlist(int WishlistID, int UserID);
        public IEnumerable<WishlistModel> GetWishlist(int UserID);
    }
}
