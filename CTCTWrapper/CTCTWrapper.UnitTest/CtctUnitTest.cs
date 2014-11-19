using System;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTCT;
using CTCT.Components.Contacts;
using System.Net;
using System.Linq;
using CTCT.Components.Activities;
using CTCT.Components.EmailCampaigns;
using CTCT.Components.Tracking;
using CTCT.Components;
using CTCT.Exceptions;
using System.Text;
using CTCT.Components.MyLibrary;
using CTCT.Util;

namespace CTCTWrapper.UnitTest
{
    /// <summary>
    /// Summary description for CtctUnitTest
    /// </summary>
    [TestClass]
    public class CtctUnitTest
    {
        #region Private Constants

		private const string CustomerEmail = "verified_email_address@...";
		private const string ApiKey = "apiKey";
		private const string AccessToken = "accessToken";

        #endregion Private Constants

        public CtctUnitTest()
        {
            
        }

        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        public class WebClientCookieAware : WebClient
        {
            CookieContainer cc = new CookieContainer();

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                var webRequest = request as HttpWebRequest;
                if (webRequest != null)
                {
                    webRequest.CookieContainer = cc;
                }
                return request;
            }
        }
        
        #region Live tests

        #region Account API

        [TestMethod]
        public void LiveVerifiedEmailAddressTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);
            var list = cc.GetVerifiedEmailAddress();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(0, list.Count);
        }

        #endregion Account API

        #region Contacts API

        [TestMethod]
        public void LiveAddContactTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList { Id = "1", Status = Status.Active });

            var nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);
        }

        [TestMethod]
        public void LiveGetContactTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);

            var retrievedContact = cc.GetContact(nc.Id);

            Assert.IsNotNull(retrievedContact);
            Assert.AreEqual(retrievedContact.Id, nc.Id);
        }

        [TestMethod]
        public void LiveUpdateContactTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList { Id = "1", Status = Status.Active });

            var nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);

            nc.CompanyName = "some company";

            var retrievedContact = cc.UpdateContact(nc, false);

            Assert.IsNotNull(retrievedContact);
            Assert.AreEqual(retrievedContact.Id, nc.Id);
            Assert.AreEqual(retrievedContact.CompanyName, nc.CompanyName);
        }

        [TestMethod]
        public void LiveDeleteContactTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);

            var result = cc.DeleteContact(nc);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LiveGetAllContacts()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var result = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreNotEqual(0, result.Results.Count);
        }

        [TestMethod]
        public void LiveGetContactByEmail()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);

            var result = cc.GetContacts(nc.EmailAddresses[0].EmailAddr, 1, DateTime.Now.AddMonths(-1), null);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(1, result.Results.Count);
        }

        #endregion Contacts API

        #region Contact List Collection API

        [TestMethod]
        public void LiveAddContactListTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ContactList contactList = new ContactList { 
                Name = string.Format("List {0}", Guid.NewGuid()),
                Status = Status.Active
            };

            var result = cc.AddList(contactList);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals(contactList.Name));
        }

        [TestMethod]
        public void LiveGetContactFromListTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            IList<ContactList> lists = cc.GetLists(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(lists);
            Assert.AreNotEqual(0, lists.Count);

            ResultSet<Contact> contacts = cc.GetContactsFromList(lists[0].Id, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);

            contacts = cc.GetContactsFromList(lists[0].Id, 3, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Meta);
            Assert.IsNotNull(contacts.Meta.Pagination);
            Assert.IsNotNull(contacts.Results);
            //Assert.AreEqual(3, contacts.Results.Count);
        }

        [TestMethod]
        public void LiveUpdateContactListTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contactList = new ContactList
                {
                Name = string.Format("List {0}", Guid.NewGuid()),
                Status = Status.Active
            };

            var result = cc.AddList(contactList);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals(contactList.Name));

            result.Name = string.Format("List - {0}", Guid.NewGuid());

            var updatedList = cc.UpdateList(result);
            Assert.IsNotNull(updatedList);
            Assert.AreEqual(result.Id, updatedList.Id);
            Assert.AreEqual(result.Name, updatedList.Name);
        }

        [TestMethod]
        public void LiveDeleteContactListTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var contactList = new ContactList
            {
                Name = string.Format("List {0}", Guid.NewGuid()),
                Status = Status.Active
            };

            var result = cc.AddList(contactList);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals(contactList.Name));

            result.Name = string.Format("List - {0}", Guid.NewGuid());

            var deleted = cc.DeleteList(result.Id.ToString(CultureInfo.InvariantCulture));
            Assert.IsTrue(deleted);
        }

        #endregion Contact List Collection API

        #region Email Campaign API

        [TestMethod]
        public void LiveAddEmailCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);
        }

        [TestMethod]
        public void LiveGetEmailCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            EmailCampaign ec = cc.GetCampaign(camp.Id);
            Assert.IsNotNull(ec);
            Assert.AreEqual(camp.Id, ec.Id);
        }

        [TestMethod]
        public void LiveUpdateEmailCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    PostalCode = "02101",
                    State = "MA",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            camp.GreetingString = "Hi";
            var camp1 = cc.UpdateCampaign(camp);
            Assert.IsNotNull(camp1);
            Assert.AreEqual(camp.Id, camp1.Id);
            Assert.AreEqual(camp.GreetingString, camp1.GreetingString);
        }

        [TestMethod]
        public void LiveDeleteEmailCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            var result = cc.DeleteCampaign(camp.Id);
            Assert.IsTrue(result);
        }

        #endregion Email Campaign API

        #region Email Campaign Schedule API

        [TestMethod]
        public void LiveScheduleCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            EmailCampaign camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);
        }

        [TestMethod]
        public void LiveGetScheduledCampaignsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            var schs = cc.GetSchedules(camp.Id);
            Assert.IsNotNull(schs);
            Assert.AreNotEqual(0, schs.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveUpdateFailedForScheduledCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            camp.GreetingName = GreetingName.FIRST_AND_LAST_NAME;

            var updatedCampaign = cc.UpdateCampaign(camp);
            Assert.IsNull(updatedCampaign);
        }

        [TestMethod]
        public void LiveDeleteScheduleForCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            var result = cc.DeleteSchedule(camp.Id, schedule.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveScheduleAnAlreadyScheduledCampaignTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            DateTime schDateAgain = DateTime.Now.AddMonths(2);
            Schedule scheduleAgain = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDateAgain });
            Assert.IsNull(scheduleAgain);
        }

        #endregion Email Campaign Schedule API

        #region Email Campaign Tracking API

        [TestMethod]
        public void LiveCampaignTrackingGetSummaryTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            TrackingSummary summary = cc.GetCampaignTrackingSummary(camp.Id);

            Assert.IsNotNull(summary);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetNotSentForTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

           var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            ResultSet<BounceActivity> result = cc.GetCampaignTrackingBounces(camp.Id, null, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetSendNowForTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            var schDate = DateTime.Now;
            var schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<BounceActivity> result = cc.GetCampaignTrackingBounces(camp.Id, null, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        // NOTE: the ExpectedException annotation is present because the tests actually fail when the campaign 
        //  isn't in sent state. This essentially means these tests are almost useless :(
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetAllClicksTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = CreateTestCampaign(cc);

            ResultSet<ClickActivity> result = cc.GetCampaignTrackingClicks(camp.Id, null, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetClicksTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = CreateTestCampaign(cc);

            ResultSet<ClickActivity> result = cc.GetCampaignTrackingClicks(camp.Id, "1", null, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(result);
        }

        private EmailCampaign CreateTestCampaign(ConstantContact cc)
        {
            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddDays(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            return camp;
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetForwardsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

           var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<ForwardActivity> result = cc.GetCampaignTrackingForwards(camp.Id, null, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetOpensTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

           var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<OpenActivity> result = cc.GetCampaignTrackingOpens(camp.Id, null, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetSendsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

           var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<SendActivity> result = cc.GetCampaignTrackingSends(camp.Id, null, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetOptOutsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual("", schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<OptOutActivity> result = cc.GetCampaignTrackingOptOuts(camp.Id, null, DateTime.Now.AddMonths(-1));

            Assert.IsNotNull(result);
        }

        #endregion Email Campaign Tracking API

        #region Email Campaign Test Send API

        [TestMethod]
        public void LiveEmailCampaignTestSendTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var camp = new EmailCampaign
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CustomerEmail,
                ReplyToEmail = CustomerEmail,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter
                {
                    OrganizationName = "my organization",
                    AddressLine1 = "123 Mapple Street",
                    AddressLine2 = "Suite 1",
                    AddressLine3 = "",
                    City = "Boston",
                    State = "MA",
                    PostalCode = "02101",
                    Country = "US",
                    IncludeForwardEmail = true,
                    ForwardEmailLinkText = "forward link",
                    IncludeSubscribeLink = true,
                    SubscribeLinkText = "subscribe link"
                }
                ,
                Lists = new List<SentContactList> { new SentContactList { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            var test = new TestSend { 
                Format = EmailFormat.HTML_AND_TEXT.ToString(),
                PersonalMessage = "This is a test send of the email campaign message.",
                EmailAddresses = new List<string> { CustomerEmail }
            };

            var testSend = cc.SendTest(camp.Id, test);

            Assert.IsNotNull(testSend);
            Assert.AreEqual(test.Format, testSend.Format);
        }

        #endregion Email Campaign Test Send API

        #region Contact Tracking API

		[TestMethod]
		public void LiveContactTrackingActivitiesTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

			ResultSet<ContactActivity> result = cc.GetContactTrackingActivities(contacts.Results[0].Id, 10, null);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Results);
		}

		[TestMethod]
		public void LiveContactTrackingEmailCampaignActivitiesTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

			ResultSet<TrackingSummary> result = cc.GetContactTrackingEmailCampaignActivities(contacts.Results[0].Id);
			Assert.IsNotNull(result);
		}

        [TestMethod]
        public void LiveContactTrackingSummaryTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            TrackingSummary summary = cc.GetContactTrackingSummary(contacts.Results[0].Id);
            Assert.IsNotNull(summary);
            //Assert.AreNotEqual(0, summary.Forwards);
            //Assert.AreNotEqual(0, summary.Opens);
            //Assert.AreNotEqual(0, summary.Sends);

            ResultSet<EmailCampaign> camps = cc.GetCampaigns(DateTime.Now.AddMonths(-1));
            summary = cc.GetCampaignTrackingSummary(camps.Results[1].Id);
            //Assert.AreNotEqual(0, summary.Forwards);
            //Assert.AreNotEqual(0, summary.Opens);
            //Assert.AreNotEqual(0, summary.Sends);
            //Assert.AreNotEqual(0, summary.Bounces);
        }

        [TestMethod]
        public void LiveContactTrackingClicksTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);
            ResultSet<ClickActivity> ca = cc.GetContactTrackingClicks(contacts.Results[0].Id, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(ca);
        }

        [TestMethod]
        public void LiveContactTrackingBouncesTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);
            ResultSet<BounceActivity> ba = cc.GetContactTrackingBounces(contacts.Results[0].Id);
            Assert.IsNotNull(ba);
        }

        [TestMethod]
        public void LiveContactTrackingForwardsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<ForwardActivity> fa = cc.GetContactTrackingForwards(contacts.Results[0].Id, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(fa);
        }

        [TestMethod]
        public void LiveContactTrackingOpensTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<OpenActivity> a = cc.GetContactTrackingOpens(contacts.Results[0].Id, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void LiveContactTrackingSendsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<SendActivity> a = cc.GetContactTrackingSends(contacts.Results[0].Id, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void LiveContactTrackingUnsubscribesTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            ResultSet<Contact> contacts = cc.GetContacts(DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<OptOutActivity> a = cc.GetContactTrackingOptOuts(contacts.Results[0].Id, DateTime.Now.AddMonths(-1));
            Assert.IsNotNull(a);
        } 

        #endregion Contact Tracking API

        #region Bulk Activities API

        [TestMethod]
        public void LiveActivityAddContactTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);
            var add = new AddContacts(
                new List<AddContactsImportData>{
                    new AddContactsImportData{
                        EmailAddresses = new List<string>{ String.Format("{0}@example.com", Guid.NewGuid()) }
                    }
                },
                new List<string> { "1" },
                null
                );
            var act = cc.CreateAddContactsActivity(add);
            Assert.IsNotNull(act);
        }

		[TestMethod]
		public void LiveActivityAddContactsMultipartTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			var filename = "add_contacts.txt";
			var content = Encoding.UTF8.GetBytes(String.Format("{0}@example.com", Guid.NewGuid()));
			var lists = new List<string>() { "1" };

			Activity activity = cc.AddContactsMultipartActivity(filename, content, lists);

			Assert.IsNotNull(activity);
			Assert.IsNotNull(activity.Id);
			Assert.AreEqual(activity.ContactCount, 1);
			Assert.AreEqual(activity.Type, "ADD_CONTACTS");			
		}

		[TestMethod]
		public void LiveActivityRemoveContactsMultipartTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			var filename = "remove_contacts.csv";
			var fileContent = String.Format("{0}@example.com", Guid.NewGuid());
			var content = Encoding.UTF8.GetBytes(fileContent);
			var lists = new List<string>() { "1" };

			 var add = new AddContacts(
                new List<AddContactsImportData>{
                    new AddContactsImportData{
                        EmailAddresses = new List<string> { fileContent }
                    }
                },
                lists,
                null
                );

            Activity act = cc.CreateAddContactsActivity(add);
			Activity activity = cc.RemoveContactsMultipartActivity(filename, content, lists);

			Assert.IsNotNull(activity);
			Assert.IsNotNull(activity.Id);
			Assert.AreEqual(activity.ContactCount, 1);
			Assert.AreEqual(activity.Type, "REMOVE_CONTACTS_FROM_LISTS");		
		}

        [TestMethod]
        public void LiveActivityRemoveContactTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);
            var emailAddresses = new List<string>{ String.Format("{0}@example.com", Guid.NewGuid()) };
            var lists = new List<string> { "1" };

            var add = new AddContacts(
                new List<AddContactsImportData>{
                    new AddContactsImportData{
                        EmailAddresses = emailAddresses
                    }
                },
                lists,
                null
                );
            Activity act = cc.CreateAddContactsActivity(add);
            Assert.IsNotNull(act);

            Activity activity = cc.AddRemoveContactsFromListsActivity(emailAddresses, lists);

            Assert.IsNotNull(activity);
            Assert.AreEqual(activity.ContactCount, emailAddresses.Count);
        }

        [TestMethod]
        public void LiveActivityClearListTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);
            var emailAddresses = new List<string> { String.Format("{0}@example.com", Guid.NewGuid()) };
            var lists = new List<string> { "1" };

            var add = new AddContacts(
                new List<AddContactsImportData>{
                    new AddContactsImportData{
                        EmailAddresses = emailAddresses
                    }
                },
                lists,
                null
                );
            Activity act = cc.CreateAddContactsActivity(add);
            Assert.IsNotNull(act);

            Activity activity = cc.AddClearListsActivity(lists);

            Assert.IsNotNull(activity);
            Assert.AreEqual(activity.Type, "CLEAR_CONTACTS_FROM_LISTS");
        }

        [TestMethod]
        public void LiveActivityExportContactsTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);
            var lists = new List<string>() { "1" };

            ExportContacts export = new ExportContacts();
            export.Lists = lists;

            Activity activity = cc.AddExportContactsActivity(export);

            Assert.IsNotNull(activity);
            Assert.IsNotNull(activity.Id);
            Assert.AreEqual(activity.Type, "EXPORT_CONTACTS");
        }

        [TestMethod]
        public void LiveActivityGetSummaryReportTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);
            var add = new AddContacts(
                new List<AddContactsImportData>{
                    new AddContactsImportData{
                        EmailAddresses = new List<string>{ String.Format("{0}@example.com", Guid.NewGuid()) }
                    }
                },
                new List<string> { "1" },
                null
                );
            Activity act = cc.CreateAddContactsActivity(add);
            Assert.IsNotNull(act);

            IList<Activity> list = cc.GetActivities();
            Activity a = cc.GetActivity(list[0].Id);
            Assert.IsNotNull(a);
        }

        #endregion Bulk Activities API

		#region MyLibrary API

		[TestMethod]
		public void GetLibraryInfoTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			var result = cc.GetLibraryInfo();
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.UsageSummary);
		}

		[TestMethod]
		public void LiveGetAllFoldersTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

            var folders = cc.GetLibraryFolders();
            Assert.IsNotNull(folders);
            Assert.IsNotNull(folders.Results);
            Assert.AreNotEqual(0, folders.Results.Count);
		}

		[TestMethod]
        public void LiveAddFolderTest()
        {
            var cc = new ConstantContact(ApiKey, AccessToken);

            var folder = new MyLibraryFolder();
			folder.Id = Guid.NewGuid().ToString();
			folder.Name = Guid.NewGuid().ToString();
			folder.CreatedDate = Extensions.ToISO8601String(DateTime.Now);
            folder.ModifiedDate = Extensions.ToISO8601String(DateTime.Now);

			var newFolder = cc.AddLibraryFolder(folder);
            Assert.IsNotNull(newFolder);
            Assert.IsNotNull(newFolder.Id);
        }

		[TestMethod]
		public void LiveUpdateFolderTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

            var folder = new MyLibraryFolder();
			folder.Id = Guid.NewGuid().ToString();
			folder.Name = Guid.NewGuid().ToString();
			folder.CreatedDate = Extensions.ToISO8601String(DateTime.Now);
            folder.ModifiedDate = Extensions.ToISO8601String(DateTime.Now);

            var newFolder = cc.AddLibraryFolder(folder);
            Assert.IsNotNull(newFolder);
            Assert.IsNotNull(newFolder.Id);

			newFolder.Name = Guid.NewGuid().ToString();
            var updatedFolder = cc.UpdateLibraryFolder(newFolder);

            Assert.IsNotNull(updatedFolder);
            Assert.AreEqual(updatedFolder.Id, newFolder.Id);
            Assert.AreEqual(updatedFolder.Name, newFolder.Name);
		}

		[TestMethod]
		public void LiveGetFolderTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

            var folder = new MyLibraryFolder();
			folder.Id = Guid.NewGuid().ToString();
			folder.Name = Guid.NewGuid().ToString();
			folder.CreatedDate = Extensions.ToISO8601String(DateTime.Now);
            folder.ModifiedDate = Extensions.ToISO8601String(DateTime.Now);

			var newFolder = cc.AddLibraryFolder(folder);
			var getFolder = cc.GetLibraryFolder(newFolder.Id);

            Assert.IsNotNull(getFolder);
			Assert.AreEqual(getFolder.Id, newFolder.Id);
			Assert.AreEqual(getFolder.Name, newFolder.Name);
		}

		[TestMethod]
		public void LiveDeleteFolderTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

            var folder = new MyLibraryFolder();
			folder.Id = Guid.NewGuid().ToString();
			folder.Name = Guid.NewGuid().ToString();
			folder.CreatedDate = Extensions.ToISO8601String(DateTime.Now);
            folder.ModifiedDate = Extensions.ToISO8601String(DateTime.Now);

			var newFolder = cc.AddLibraryFolder(folder);
			bool result = cc.DeleteLibraryFolder(newFolder.Id);
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void LiveGetTrashTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			var files = cc.GetLibraryTrashFiles();
			Assert.IsNotNull(files);
			Assert.IsNotNull(files.Results);
            //Assert.AreNotEqual(0, files.Results.Count);
		}

		[TestMethod]
		public void LiveDeleteTrashFilesTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

			var result = cc.DeleteLibraryTrashFiles();
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void LiveGetFilesTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

            var files = cc.GetLibraryFiles();
            Assert.IsNotNull(files);
            Assert.IsNotNull(files.Results);
            Assert.AreNotEqual(0, files.Results.Count);
		}

		[TestMethod]
		public void LiveGetFilesByFolderTest()
		{
			var cc = new ConstantContact(ApiKey, AccessToken);

            var folders = cc.GetLibraryFolders();
            Assert.IsNotNull(folders);
            Assert.IsNotNull(folders.Results);
            Assert.AreNotEqual(0, folders.Results.Count);

			var files = cc.GetLibraryFilesByFolder(folders.Results[0].Id);
			Assert.IsNotNull(files);
            Assert.IsNotNull(files.Results);
		}

		#endregion

        #endregion

        #region Authentication

        [TestMethod]
        public void LiveAuthenticationTest()
        {
            string state = "ok";
            var accessToken = OAuth.AuthenticateFromWinProgram(ref state);            
        }

        #endregion
    }
}
