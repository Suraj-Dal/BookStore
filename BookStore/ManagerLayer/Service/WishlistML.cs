using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class WishlistML : IWishlistML
    {
        private readonly IWishlistRL iwishlistRL;
        public WishlistML(IWishlistRL iwishlistRL)
        {
            this.iwishlistRL = iwishlistRL;
        }

        public bool AddToWishlist(int BookID, int UserID)
        {
            try
            {
                return iwishlistRL.AddToWishlist(BookID, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteWishlist(int WishlistID, int UserID)
        {
            try
            {
                return iwishlistRL.DeleteWishlist(WishlistID, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<WishlistModel> GetWishlist(int UserID)
        {
            try
            {
                return iwishlistRL.GetWishlist(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
