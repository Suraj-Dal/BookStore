using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Service
{
    public class FeedbackML : IFeedbackML
    {
        private readonly IFeedbackRL ifeedbackRL;
        public FeedbackML(IFeedbackRL ifeedbackRL)
        {
            this.ifeedbackRL = ifeedbackRL;
        }

        public bool AddFeedback(FeedbackModel feedbackModel, int UserID)
        {
            try
            {
                return ifeedbackRL.AddFeedback(feedbackModel, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<GetFeedbackModel> GetFeedback(int UserID)
        {
            try
            {
                return ifeedbackRL.GetFeedback(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
