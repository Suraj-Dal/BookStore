using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public bool AddFeedback(FeedbackModel feedbackModel, int UserID);
        public IEnumerable<GetFeedbackModel> GetFeedback(int UserID);
    }
}
