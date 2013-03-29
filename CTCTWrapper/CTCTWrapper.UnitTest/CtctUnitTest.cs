using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CTCT;
using CTCT.Util;
using CTCT.Components.Contacts;
using System.Net;
using System.IO;
using System.Web;
using Moq;
using Moq.Protected;
using CTCT.Auth;
using CTCT.Services;
using CTCT.Components.Activities;
using CTCT.Components.EmailCampaigns;
using CTCT.Components.Tracking;
using CTCT.Components;
using CTCT.Exceptions;

namespace CTCTWrapper.UnitTest
{
    /// <summary>
    /// Summary description for CtctUnitTest
    /// </summary>
    [TestClass]
    public class CtctUnitTest
    {
        #region Private Constants

        private const string CUSTOMER_EMAIL = "verified_email_address@...";

        #endregion Private Constants

        public CtctUnitTest()
        {
            
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
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
            ConstantContact cc = new ConstantContact();
            var list = cc.GetVerifiedEmailAddress();

            Assert.IsNotNull(list);
            Assert.AreNotEqual(0, list.Count);
        }

        #endregion Account API

        #region Contacts API

        [TestMethod]
        public void LiveAddContactTest()
        {
            ConstantContact cc = new ConstantContact();

            Contact contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress() { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList() { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);
        }

        [TestMethod]
        public void LiveGetContactTest()
        {
            ConstantContact cc = new ConstantContact();

            Contact contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress() { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList() { Id = "1", Status = Status.Active });

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
            ConstantContact cc = new ConstantContact();

            Contact contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress() { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList() { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
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
            ConstantContact cc = new ConstantContact();

            Contact contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress() { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList() { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);

            var result = cc.DeleteContact(nc);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LiveGetAllContacts()
        {
            ConstantContact cc = new ConstantContact();

            var result = cc.GetContacts();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreNotEqual(0, result.Results.Count);
        }

        [TestMethod]
        public void LiveGetContactByEmail()
        {
            ConstantContact cc = new ConstantContact();

            Contact contact = new Contact();
            contact.EmailAddresses.Add(new EmailAddress() { EmailAddr = String.Format("{0}@email.com", Guid.NewGuid()), ConfirmStatus = ConfirmStatus.NoConfirmationRequired, Status = Status.Active });
            contact.Lists.Add(new ContactList() { Id = "1", Status = Status.Active });

            Contact nc = cc.AddContact(contact, false);
            Assert.IsNotNull(nc);
            Assert.IsNotNull(nc.Id);

            var result = cc.GetContacts(nc.EmailAddresses[0].EmailAddr, 1);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(1, result.Results.Count);
        }

        #endregion Contacts API

        #region Contact List Collection API

        [TestMethod]
        public void LiveAddContactListTest()
        {
            ConstantContact cc = new ConstantContact();

            ContactList contactList = new ContactList() { 
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
            ConstantContact cc = new ConstantContact();

            IList<ContactList> lists = cc.GetLists();
            Assert.IsNotNull(lists);
            Assert.AreNotEqual(0, lists.Count);

            ResultSet<Contact> contacts = cc.GetContactsFromList(lists[0].Id);
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);

            contacts = cc.GetContactsFromList(lists[0].Id, 3);
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Meta);
            Assert.IsNotNull(contacts.Meta.Pagination);
            Assert.IsNotNull(contacts.Meta.Pagination.Next);
            Assert.IsNotNull(contacts.Results);
            Assert.AreEqual(3, contacts.Results.Count);

            contacts = cc.GetContactsFromList(contacts.Meta.Pagination);
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Meta);
            Assert.IsNotNull(contacts.Meta.Pagination);
            Assert.IsNotNull(contacts.Meta.Pagination.Next);
            Assert.IsNotNull(contacts.Results);
            Assert.AreEqual(3, contacts.Results.Count);
        }

        [TestMethod]
        public void LiveUpdateContactListTest()
        {
            ConstantContact cc = new ConstantContact();

            ContactList contactList = new ContactList()
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
            ConstantContact cc = new ConstantContact();

            ContactList contactList = new ContactList()
            {
                Name = string.Format("List {0}", Guid.NewGuid()),
                Status = Status.Active
            };

            var result = cc.AddList(contactList);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name.Equals(contactList.Name));

            result.Name = string.Format("List - {0}", Guid.NewGuid());

            var deleted = cc.DeleteList(result.Id.ToString());
            Assert.IsTrue(deleted);
        }

        #endregion Contact List Collection API

        #region Email Campaign API

        [TestMethod]
        public void LiveAddEmailCampaignTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);
        }

        [TestMethod]
        public void LiveGetEmailCampaignTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
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
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
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
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
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
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);
        }

        [TestMethod]
        public void LiveGetScheduledCampaignsTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            var schs = cc.GetSchedules(camp.Id);
            Assert.IsNotNull(schs);
            Assert.AreNotEqual(0, schs.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveUpdateFailedForScheduledCampaignTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            camp.GreetingName = GreetingName.FIRST_AND_LAST_NAME;

            var updatedCampaign = cc.UpdateCampaign(camp);
            Assert.IsNull(updatedCampaign);
        }

        [TestMethod]
        public void LiveDeleteScheduleForCampaignTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            var result = cc.DeleteSchedule(camp.Id, schedule.Id.ToString());

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveScheduleAnAlreadyScheduledCampaignTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddMonths(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            DateTime schDateAgain = DateTime.Now.AddMonths(2);
            Schedule scheduleAgain = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDateAgain });
            Assert.IsNull(scheduleAgain);
        }

        #endregion Email Campaign Schedule API

        #region Email Campaign Tracking API

        [TestMethod]
        public void LiveCampaignTrackingGetSummaryTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
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
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            ResultSet<BounceActivity> result = cc.GetCampaignTrackingBounces(camp.Id, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetSendNowForTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<BounceActivity> result = cc.GetCampaignTrackingBounces(camp.Id, null);

            Assert.IsNull(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetClicksTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now.AddDays(1);
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<ClickActivity> result = cc.GetCampaignTrackingClicks(camp.Id, "1", null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetForwardsTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<ForwardActivity> result = cc.GetCampaignTrackingForwards(camp.Id, null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetOpensTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<OpenActivity> result = cc.GetCampaignTrackingOpens(camp.Id, null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetSendsTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<SendActivity> result = cc.GetCampaignTrackingSends(camp.Id, null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CtctException))]
        public void LiveCampaignTrackingGetOptOutsTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            DateTime schDate = DateTime.Now;
            Schedule schedule = cc.AddSchedule(camp.Id, new Schedule() { ScheduledDate = schDate });
            Assert.IsNotNull(schedule);
            Assert.AreNotEqual(0, schedule.Id);
            Assert.IsNotNull(schedule.ScheduledDate);

            ResultSet<OptOutActivity> result = cc.GetCampaignTrackingOptOuts(camp.Id, null);

            Assert.IsNotNull(result);
        }

        #endregion Email Campaign Tracking API

        #region Email Campaign Test Send API

        [TestMethod]
        public void LiveEmailCampaignTestSendTest()
        {
            ConstantContact cc = new ConstantContact();

            EmailCampaign camp = new EmailCampaign()
            {
                EmailContent = "<html><body>EMAIL CONTENT.</body></html>",
                Subject = "campaign subject",
                FromName = "my company",
                FromEmail = CUSTOMER_EMAIL,
                ReplyToEmail = CUSTOMER_EMAIL,
                Name = "campaign_" + DateTime.Now.ToString("yyMMddHHmmss"),
                TextContent = "email campaign text content",
                GreetingString = "Dear ",
                //TemplateType = TemplateType.CUSTOM,
                Status = CampaignStatus.DRAFT,
                EmailContentFormat = CampaignEmailFormat.HTML,
                StyleSheet = "",
                MessageFooter = new MessageFooter()
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
                Lists = new List<SentContactList>() { new SentContactList() { Id = "1" } }
            };
            camp = cc.AddCampaign(camp);
            Assert.IsNotNull(camp);
            Assert.IsNotNull(camp.Id);

            TestSend test = new TestSend() { 
                Format = EmailFormat.HTML_AND_TEXT.ToString(),
                PersonalMessage = "This is a test send of the email campaign message.",
                EmailAddresses = new List<string> { CUSTOMER_EMAIL }
            };

            var testSend = cc.SendTest(camp.Id, test);

            Assert.IsNotNull(testSend);
            Assert.AreEqual(test.Format, testSend.Format);
        }

        #endregion Email Campaign Test Send API

        #region Contact Tracking API

        [TestMethod]
        public void LiveContactTrackingSummaryTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            TrackingSummary summary = cc.GetContactTrackingSummary(contacts.Results[0].Id);
            Assert.IsNotNull(summary);
            //Assert.AreNotEqual(0, summary.Forwards);
            //Assert.AreNotEqual(0, summary.Opens);
            //Assert.AreNotEqual(0, summary.Sends);

            IList<EmailCampaign> camps = cc.GetCampaigns();
            summary = cc.GetCampaignTrackingSummary(camps[1].Id);
            //Assert.AreNotEqual(0, summary.Forwards);
            //Assert.AreNotEqual(0, summary.Opens);
            //Assert.AreNotEqual(0, summary.Sends);
            //Assert.AreNotEqual(0, summary.Bounces);
        }

        [TestMethod]
        public void LiveContactTrackingClicksTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);
            ResultSet<ClickActivity> ca = cc.GetContactTrackingClicks(contacts.Results[0].Id);
            Assert.IsNotNull(ca);
        }

        [TestMethod]
        public void LiveContactTrackingBouncesTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);
            ResultSet<BounceActivity> ba = cc.GetContactTrackingBounces(contacts.Results[0].Id);
            Assert.IsNotNull(ba);
        }

        [TestMethod]
        public void LiveContactTrackingForwardsTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<ForwardActivity> fa = cc.GetContactTrackingForwards(contacts.Results[0].Id);
            Assert.IsNotNull(fa);
        }

        [TestMethod]
        public void LiveContactTrackingOpensTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<OpenActivity> a = cc.GetContactTrackingOpens(contacts.Results[0].Id);
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void LiveContactTrackingSendsTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<SendActivity> a = cc.GetContactTrackingSends(contacts.Results[0].Id);
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void LiveContactTrackingUnsubscribesTest()
        {
            ConstantContact cc = new ConstantContact();

            ResultSet<Contact> contacts = cc.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.IsNotNull(contacts.Results);
            Assert.IsTrue(contacts.Results.Count > 0);

            ResultSet<OptOutActivity> a = cc.GetContactTrackingOptOuts(contacts.Results[0].Id);
            Assert.IsNotNull(a);
        } 

        #endregion Contact Tracking API

        #region Bulk Activities API

        [TestMethod]
        public void LiveActivityAddContactTest()
        {
            ConstantContact cc = new ConstantContact();
            AddContacts add = new AddContacts(
                new List<AddContactsImportData>(){
                    new AddContactsImportData(){
                        EmailAddresses = new List<string>(){ String.Format("{0}@example.com", Guid.NewGuid()) }
                    }
                },
                new List<string>() { "1" },
                null
                );
            Activity act = cc.CreateAddContactsActivity(add);
            Assert.IsNotNull(act);
        }

        [TestMethod]
        public void LiveActivityRemoveContactTest()
        {
            ConstantContact cc = new ConstantContact();
            var emailAddresses = new List<string>(){ String.Format("{0}@example.com", Guid.NewGuid()) };
            var lists = new List<string>() { "2" };

            AddContacts add = new AddContacts(
                new List<AddContactsImportData>(){
                    new AddContactsImportData(){
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
            ConstantContact cc = new ConstantContact();
            var emailAddresses = new List<string>() { String.Format("{0}@example.com", Guid.NewGuid()) };
            var lists = new List<string>() { "2" };

            AddContacts add = new AddContacts(
                new List<AddContactsImportData>(){
                    new AddContactsImportData(){
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
            ConstantContact cc = new ConstantContact();
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
            ConstantContact cc = new ConstantContact();
            AddContacts add = new AddContacts(
                new List<AddContactsImportData>(){
                    new AddContactsImportData(){
                        EmailAddresses = new List<string>(){ String.Format("{0}@example.com", Guid.NewGuid()) }
                    }
                },
                new List<string>() { "1" },
                null
                );
            Activity act = cc.CreateAddContactsActivity(add);
            Assert.IsNotNull(act);

            IList<Activity> list = cc.GetActivities();
            foreach (Activity activity in list)
            {
                Activity a = cc.GetActivity(activity.Id);
                Assert.IsNotNull(a);
            }
        }

        #endregion Bulk Activities API

        #endregion

    }
}
