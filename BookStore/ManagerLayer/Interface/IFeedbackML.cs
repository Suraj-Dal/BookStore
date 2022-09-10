using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IFeedbackML
    {
        public bool AddFeedback(FeedbackModel feedbackModel, int UserID);
        public IEnumerable<GetFeedbackModel> GetFeedback(int UserID);
    }
}
