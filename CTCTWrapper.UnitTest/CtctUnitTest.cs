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

namespace CTCTWrapper.UnitTest
{
    /// <summary>
    /// Summary description for CtctUnitTest
    /// </summary>
    [TestClass]
    public class CtctUnitTest
    {
        private const string Contact1 = "{\"id\":1,\"status\":\"ACTIVE\",\"fax\":null,\"addresses\":[{\"line1\":\"\",\"line2\":\"\",\"line3\":\"\",\"city\":\"\",\"address_type\":\"PERSONAL\",\"state_code\":\"\",\"country_code\":\"\",\"postal_code\":\"\",\"sub_postal_code\":\"\"},{\"line1\":\"\",\"line2\":\"\",\"line3\":\"\",\"city\":\"\",\"address_type\":\"BUSINESS\",\"state_code\":\"\",\"country_code\":\"\",\"postal_code\":\"\",\"sub_postal_code\":\"\"}],\"notes\":[{\"id\":1,\"note\":\"this is a note1\",\"created_date\":\"2012-12-18T10:51:21.454Z\"}],\"confirmed\":false,\"lists\":[{\"id\":1,\"status\":\"ACTIVE\"}],\"source\":\"\",\"email_addresses\":[{\"status\":\"ACTIVE\",\"confirm_status\":\"NO_CONFIRMATION_REQUIRED\",\"opt_in_source\":\"ACTION_BY_OWNER\",\"opt_in_date\":\"2012-12-11T04:56:07.630Z\",\"opt_out_date\":\"1969-12-31T19:00:00.000Z\",\"email_address\":\"johndoe@constantcontact.com\"}],\"prefix_name\":\"\",\"first_name\":\"firstname\",\"middle_name\":\"\",\"last_name\":\"lastname\",\"job_title\":\"job1\",\"department_name\":\"\",\"company_name\":\"company1\",\"home_phone\":\"555-12345\",\"work_phone\":\"555-101010\",\"custom_fields\":[{\"name\":\"CustomField1\",\"value\":\"custom field1\"}],\"source_details\":\"\",\"action_by\":\"ACTION_BY_OWNER\"}";
        private const string Contact2 = "{\"id\":2,\"status\":\"ACTIVE\",\"fax\":\"555-0000\",\"addresses\":[{\"line1\":\"line1\",\"line2\":\"line2\",\"line3\":\"line3\",\"city\":\"city1\",\"address_type\":\"PERSONAL\",\"state_code\":\"AL\",\"country_code\":\"us\",\"postal_code\":\"12345\",\"sub_postal_code\":\"010\"},{\"line1\":\"\",\"line2\":\"\",\"line3\":\"\",\"city\":\"\",\"address_type\":\"BUSINESS\",\"state_code\":\"\",\"country_code\":\"\",\"postal_code\":\"\",\"sub_postal_code\":\"\"}],\"notes\":[],\"confirmed\":false,\"lists\":[{\"id\":1,\"status\":\"ACTIVE\"}],\"source\":\"API\",\"email_addresses\":[{\"status\":\"ACTIVE\",\"confirm_status\":\"NO_CONFIRMATION_REQUIRED\",\"opt_in_source\":\"ACTION_BY_OWNER\",\"opt_in_date\":\"2012-12-13T08:43:04.974Z\",\"opt_out_date\":\"1969-12-31T19:00:00.000Z\",\"email_address\":\"test1@yahoo.com\"}],\"prefix_name\":\"pre1\",\"first_name\":\"first1\",\"middle_name\":\"middle1\",\"last_name\":\"last1\",\"job_title\":\"job1\",\"department_name\":\"\",\"company_name\":\"company1\",\"custom_fields\":[{\"name\":\"CustomField1\",\"value\":\"custom field1\"}],\"source_details\":\"7a3ae237-cbd1-4aaf-8194-721abede7cdb\",\"source_is_url\":false,\"action_by\":\"ACTION_BY_OWNER\"}";
        private const string Contact3 = "{\"id\":3,\"status\":\"ACTIVE\",\"fax\":\"555-0000\",\"addresses\":[{\"line1\":\"line1\",\"line2\":\"line2\",\"line3\":\"line3\",\"city\":\"city2\",\"address_type\":\"PERSONAL\",\"state_code\":\"\",\"country_code\":\"us\",\"postal_code\":\"12345\",\"sub_postal_code\":\"020\"},{\"line1\":\"\",\"line2\":\"\",\"line3\":\"\",\"city\":\"\",\"address_type\":\"BUSINESS\",\"state_code\":\"\",\"country_code\":\"\",\"postal_code\":\"\",\"sub_postal_code\":\"\"}],\"notes\":[],\"confirmed\":false,\"lists\":[{\"id\":1,\"status\":\"ACTIVE\"}],\"source\":\"API\",\"email_addresses\":[{\"status\":\"ACTIVE\",\"confirm_status\":\"NO_CONFIRMATION_REQUIRED\",\"opt_in_source\":\"ACTION_BY_OWNER\",\"opt_in_date\":\"2012-12-18T10:49:58.279Z\",\"opt_out_date\":\"2012-12-18T06:14:25.920Z\",\"email_address\":\"test2@yahoo.com\"}],\"prefix_name\":\"pre2\",\"first_name\":\"first2\",\"middle_name\":\"middle2\",\"last_name\":\"last2\",\"job_title\":\"job2\",\"department_name\":\"\",\"company_name\":\"company2\",\"custom_fields\":[],\"source_details\":\"7a3ae237-cbd1-4aaf-8194-721abede7cdb\",\"source_is_url\":false,\"action_by\":\"ACTION_BY_OWNER\"}";

        private const string List1 = "{\"id\":1,\"status\":\"ACTIVE\"}";
        private const string List2 = "{\"id\":2,\"status\":\"ACTIVE\"}";
        private const string List3 = "{\"id\":3,\"status\":\"ACTIVE\"}";

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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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

        #region Contact Service

        [TestMethod]
        public void GetTokenTest()
        {
            Mock<ICtctOAuth2> target = new Mock<ICtctOAuth2>();
            target.Setup(m => m.GetAccessToken()).Returns("access_token");
            
            string token = target.Object.GetAccessToken();
            Assert.AreEqual("access_token", token);
        }

        [TestMethod]
        public void GetContactsTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("[{0},{1},{2}]", Contact1, Contact2, Contact3) });
            
            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            IList<Contact> contacts = target.Object.GetContacts();
            Assert.IsNotNull(contacts);
            Assert.AreEqual(3, contacts.Count);
        }

        [TestMethod]
        public void GetContactTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", Contact1) });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            Contact contact = target.Object.GetContact(1);
            Assert.IsNotNull(contact);
            Assert.IsNotNull(contact.EmailAddresses);
            Assert.AreEqual(1, contact.EmailAddresses.Count);
            Assert.AreEqual("johndoe@constantcontact.com", contact.EmailAddresses[0].EmailAddr);
        }

        [TestMethod]
        public void GetContactByEmailTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("[{0}]", Contact1) });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            IList<Contact> contacts = target.Object.GetContactByEmail("johndoe@constantcontact.com");
            Assert.IsNotNull(contacts);
            Assert.AreEqual(1, contacts.Count);
            Assert.AreEqual("johndoe@constantcontact.com", contacts[0].EmailAddresses[0].EmailAddr);
        }

        [TestMethod]
        public void AddContactTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", Contact1) });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            Contact contact = target.Object.AddContact(new Contact());
            Assert.IsNotNull(contact);
            Assert.IsNotNull(contact.EmailAddresses);
            Assert.AreEqual(1, contact.EmailAddresses.Count);
            Assert.AreEqual("johndoe@constantcontact.com", contact.EmailAddresses[0].EmailAddr);
        }

        [TestMethod]
        public void DeleteContactTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", Contact1), StatusCode = System.Net.HttpStatusCode.NoContent });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            bool res = target.Object.DeleteContact(new Contact() { Id = 1 });
            Assert.IsTrue(res);

            res = target.Object.DeleteContact(1);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void DeleteContactFromListTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", Contact1), StatusCode = System.Net.HttpStatusCode.NoContent });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            bool res = target.Object.DeleteContactFromList(new Contact() { Id = 1 }, new ContactList() { Id = 1 });
            Assert.IsTrue(res);

            res = target.Object.DeleteContactFromList(1, 1);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void DeleteContactFromListsTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Delete(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", Contact1), StatusCode = System.Net.HttpStatusCode.NoContent });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            bool res = target.Object.DeleteContactFromLists(new Contact() { Id = 1 });
            Assert.IsTrue(res);

            res = target.Object.DeleteContactFromLists(1);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void UpdateContactTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Put(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", Contact1) });

            Mock<ContactService> cserv = new Mock<ContactService>();
            cserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IContactService>("ContactService").Returns(cserv.Object);

            Contact contact = target.Object.UpdateContact(new Contact());
            Assert.IsNotNull(contact);
            Assert.IsNotNull(contact.EmailAddresses);
            Assert.AreEqual(1, contact.EmailAddresses.Count);
            Assert.AreEqual("johndoe@constantcontact.com", contact.EmailAddresses[0].EmailAddr);
        }

        #endregion

        #region List Service

        [TestMethod]
        public void GetListsTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("[{0},{1},{2}]", List1, List2, List3) });

            Mock<ListService> lserv = new Mock<ListService>();
            lserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IListService>("ListService").Returns(lserv.Object);

            IList<ContactList> lists = target.Object.GetLists();
            Assert.IsNotNull(lists);
            Assert.AreEqual(3, lists.Count);
        }

        [TestMethod]
        public void AddListTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", List1) });

            Mock<ListService> lserv = new Mock<ListService>();
            lserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IListService>("ListService").Returns(lserv.Object);

            ContactList list = target.Object.AddList(new ContactList());
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Id);
            Assert.AreEqual(Status.Active, list.Status);
        }

        [TestMethod]
        public void GetListTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("{0}", List1) });

            Mock<ListService> lserv = new Mock<ListService>();
            lserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IListService>("ListService").Returns(lserv.Object);

            ContactList list = target.Object.GetList(1);
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Id);
            Assert.AreEqual(Status.Active, list.Status);
        }

        [TestMethod]
        public void GetContactsFromListTest()
        {
            Mock<IRestClient> rest = new Mock<IRestClient>();
            rest.Setup(m => m.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(new CUrlResponse() { Body = String.Format("[{0},{1},{2}]", Contact1, Contact2, Contact3) });

            Mock<ListService> lserv = new Mock<ListService>();
            lserv.SetupGet<IRestClient>(p => p.RestClient).Returns(rest.Object);

            Mock<ICtctOAuth2> oauth = new Mock<ICtctOAuth2>();
            oauth.Setup(m => m.GetAccessToken()).Returns("access_token");

            Mock<ConstantContact> target = new Mock<ConstantContact>();
            target.Protected().SetupGet<ICtctOAuth2>("OAuth").Returns(oauth.Object);
            target.Protected().SetupGet<IListService>("ListService").Returns(lserv.Object);

            IList<Contact> contacts = target.Object.GetContactsFromList(new ContactList() { Id = 1 });
            Assert.IsNotNull(contacts);
            Assert.AreEqual(3, contacts.Count);

            contacts = target.Object.GetContactsFromList(1);
            Assert.IsNotNull(contacts);
            Assert.AreEqual(3, contacts.Count);
        }

        #endregion
    }
}
