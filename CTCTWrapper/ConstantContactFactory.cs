using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Services;

namespace CTCT
{
    /// <summary>
    /// ConstantContactFactory class used to create needed services
    /// </summary>
    public class ConstantContactFactory
    {
        private IUserServiceContext userServiceContext = null;

        /// <summary>
        /// ConstantContactFactory constructor
        /// </summary>
        /// <param name="userServiceContext">User service context</param>
        public ConstantContactFactory(IUserServiceContext userServiceContext)
        {
            this.userServiceContext = userServiceContext;
        }

        /// <summary>
        /// Create an account service 
        /// </summary>
        /// <returns>IAccountService</returns>
        public IAccountService CreateAccountService()
        {
            return new AccountService(this.userServiceContext);
        }

        /// <summary>
        /// Create an activity service
        /// </summary>
        /// <returns>IActivityService</returns>
        public IActivityService CreateActivityService()
        {
            return new ActivityService(userServiceContext);
        }

        /// <summary>
        /// Create a campaign schedule service
        /// </summary>
        /// <returns>ICampaignScheduleService</returns>
        public ICampaignScheduleService CreateCampaignScheduleService()
        {
            return new CampaignScheduleService(userServiceContext);
        }

        /// <summary>
        /// Create a campaign tracking service
        /// </summary>
        /// <returns>ICampaignTrackingService</returns>
        public ICampaignTrackingService CreateCampaignTrackingService()
        {
            return new CampaignTrackingService(userServiceContext);
        }

        /// <summary>
        /// Create a contact service
        /// </summary>
        /// <returns>IContactService</returns>
        public IContactService CreateContactService()
        {
            return new ContactService(userServiceContext);
        }

        /// <summary>
        /// Create a contact tracking service
        /// </summary>
        /// <returns>IContactTrackingService</returns>
        public IContactTrackingService CreateContactTrackingService()
        {
            return new ContactTrackingService(userServiceContext);
        }

        /// <summary>
        /// Create an email campign service
        /// </summary>
        /// <returns>IEmailCampaignService</returns>
        public IEmailCampaignService CreateEmailCampaignService()
        {
            return new EmailCampaignService(userServiceContext);
        }

        /// <summary>
        /// Create an event spot service
        /// </summary>
        /// <returns>IEventSpotService</returns>
        public IEventSpotService CreateEventSpotService()
        {
            return new EventSpotService(userServiceContext);
        }

        /// <summary>
        /// Create a list service
        /// </summary>
        /// <returns>IListService</returns>
        public IListService CreateListService()
        {
            return new ListService(userServiceContext);
        }

        /// <summary>
        /// Create a my library service
        /// </summary>
        /// <returns>IMyLibraryService</returns>
        public IMyLibraryService CreateMyLibraryService()
        {
            return new MyLibraryService(userServiceContext);
        }

        /// <summary>
        /// Create a bulk status service
        /// </summary>
        /// <returns>IBulkStatusService</returns>
        public IBulkStatusService CreateBulkStatusService()
        {
            return new BulkStatusService(userServiceContext);
        }
    }
}
